local protobuf = require "protobuf"
local message = require "message"
local skynet = require "skynet"
local socket = require "socket"
local netpack = require "netpack"
local msgpack = require "msgpack.core"

require "skynet.manager"

local CMD = {}

local player_table = {}

CMD.dispatch = function (opcode, msg, agent, ...)
	local id = agent.player_info.id;
	local client_fd = agent.client_fd
    if opcode == message.CSENTERSCENE%10000 then
		enterscene(id, agent)
	end
	if opcode == message.CSPLAYERMOVE%10000 then
		updatePos(id, msg)
	end
	if opcode == message.CSPLAYERSTARTMOVE%10000 then
		playerStartMove(id, client_fd)
	end
	if opcode == message.CSPLAYERENDMOVE%10000 then
		playerEndMove(id, msg, client_fd)
	end
	if opcode == message.CSPLAYERENDMOVEPOS%10000 then
		playerEndMovePos(id, msg)
	end
	if opcode == message.CSPLAYERUPDATEMOVEDIR%10000 then
		playerUpdateMoveDir(id, msg, client_fd)
	end
	if opcode == message.CSASYNCPLAYERPOS%10000 then
		playerAsyncPosAndDir(id, msg)
	end
end

CMD.disconect = function (agent)
	leaveScene(agent)
end

function enterscene(id, agent)
	player_table[id] = initplayerposition()
	player_table[id].agent = agent;
end

function leaveScene(id)
	player_table[id] = nil
end

function updatePos(id, data)

	local tb = {}
	tb.id = 2017
	local msg = protobuf.decode("Monster.Protocol.CsPlayerMove", data)
	if msg then

	local info = player_table[id]
	info.dirX = msg.dirX;
	info.dirY = msg.dirY;
	end
end

function playerStartMove(id, client_fd)
	local info = player_table[id]
	info.isMove = true

	local tb = {}
	tb.id = id;

	local msgbody = protobuf.encode("Monster.Protocol.ScPlayerStartMove", tb)
	local package = msgpack.pack(message.SCPLAYERSTARTMOVE, msgbody)
	send_response(client_fd, package)
end

function playerEndMove(id, data, client_fd)
	local msg = protobuf.decode("Monster.Protocol.CsPlayerEndMove", data)
	local info = player_table[id]
	info.isMove = false;
	info.dirX = 0
	info.dirY = 0

	local tb = {}
	tb.id = id;

	local msgbody = protobuf.encode("Monster.Protocol.ScPlayerEndMove", tb)
	local package = msgpack.pack(message.SCPLAYERENDMOVE, msgbody)
	send_response(client_fd, package)

end

function playerEndMovePos(id, data)
	local msg = protobuf.decode("Monster.Protocol.CsPlayerEndMovePos", data)
	local info = player_table[id]
	info.posX = msg.posX
	info.posY = msg.posY
	info.dirX = msg.dirX
	info.dirY = msg.dirY
end

function playerUpdateMoveDir(id, data, client_fd)
	local msg = protobuf.decode("Monster.Protocol.CsPlayerUpdateMoveDir", data)
	-- msg will decode failure sometimes and i don't know why
	if msg then
		local info = player_table[id]
		info.dirX = msg.dirX
		info.dirY = msg.dirY

		local scPlayerUpdateMoveDir = {}
		scPlayerUpdateMoveDir.dirX = info.dirX;
		scPlayerUpdateMoveDir.dirY = info.dirY;
		local msgbody = protobuf.encode("Monster.Protocol.ScPlayerUpdateMoveDir", scPlayerUpdateMoveDir)
		local package = msgpack.pack(message.SCPLAYERUPDATEMOVEDIR, msgbody)
		send_response(client_fd, package)
	end
end

function playerAsyncPosAndDir(id, data)
	local msg = protobuf.decode("Monster.Protocol.CsAsyncPlayerPos", data)
	if msg then
		local info = player_table[id]
		info.dirX = msg.posInfo.dirX;
		info.dirY = msg.posInfo.dirY;
		info.posX = msg.posInfo.posX;
		info.posY = msg.posInfo.posY;
	end
end

function playerInfoMsg()
	local scAllPlayerInfo = {infos = {}}
	local index = 1

	for k,v in pairs(player_table) do
		local playerPosInfo = {}
		playerPosInfo.posInfo = {}
		playerPosInfo.id = k;
		playerPosInfo.posInfo.posX = v.posX;
		playerPosInfo.posInfo.posY = v.posY;
		playerPosInfo.posInfo.dirX = v.dirX;
		playerPosInfo.posInfo.dirY = v.dirY;
		playerPosInfo.isMove = v.isMove;
		scAllPlayerInfo.infos[index] = playerPosInfo
		index = index + 1
	end

	local msgbody = protobuf.encode("Monster.Protocol.ScAllPlayerPosInfo", scAllPlayerInfo)

    return msgpack.pack(message.SCALLPLAYERPOSINFO, msgbody)
end

function initplayerposition()
	local info = {}
	info.posX = 0
	info.posY = 0
	info.dirX = 0
	info.dirY = 0
	info.isMove = false
	return info;
end

function send_response(client_fd, package)
	socket.write(client_fd, netpack.pack(package))
end

function broadcastpackage(package)
	for k,v in pairs(player_table) do
		send_response(v.agent.client_fd, package)
	end
end



function fixedUpdate()
	local i = 0
	while (true)
	do 
		skynet.sleep(100)	
		broadcastpackage(playerInfoMsg())
	end
end

skynet.start(function(...)
	skynet.dispatch("lua", function(_,_,cmd,...)
		f = CMD[cmd]
		skynet.ret(skynet.pack(f(...)))	
	end)

	protobuf = require "protobuf"
	protobuf.register_file "../proto/scene_message.pb"

	skynet.register "leitaiservice"
	
	skynet.fork(fixedUpdate)
end)