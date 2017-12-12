local message = require "message"
local skynet = require "skynet"
local socket = require "socket"
local netpack = require "netpack"
local msgpack = require "msgpack.core"

require "skynet.manager"

local CMD = {}

local player_table = {}

CMD.dispatch = function (opcode, msg, agent, ...)
	print("scene opcode", opcode)
	local id = agent.player_info.id;
	local client_fd = agent.client_fd
    if opcode == message.CSENTERSCENE%10000 then
		enterscene(id, agent, client_fd)
	end
	if opcode == message.CSPLAYERSTARTMOVE%10000 then
		playerStartMove(id, msg)
	end
	if opcode == message.CSPLAYERENDMOVE%10000 then
		playerEndMove(id, msg, client_fd)
	end
	if opcode == message.CSPLAYERUPDATEMOVEDIR%10000 then
		playerUpdateMoveDir(id, msg, client_fd)
	end
end

CMD.disconect = function (agent)
	leaveScene(agent)
end

function enterscene(id, agent, client_fd)
	local info = initplayerposition(id)
	player_table[id] = info
	player_table[id].agent = agent;
	local selfTb = {}
	selfTb.id = id
	selfTb.result = true
	local selfMsg = protobuf.encode("Monster.Protocol.ScEnterScene", selfTb)
	local package = msgpack.pack(message.SCENTERSCENE, selfMsg)
	
	send_response(agent.client_fd, package)
	local broadTb = {}
	broadTb.id = id
	broadTb.posInfo = info.posInfo
	local broadMsg = protobuf.encode("Monster.Protocol.ScOtherRoleEnterScene", broadTb)
	local package = msgpack.pack(message.SCOTHERROLEENTERSCENE, broadMsg)
	sendAllOtherPosInfo(id, client_fd)
	broadcastpackage(broadMsg, id)
end

function leaveScene(id)
	player_table[id] = nil
end

function playerStartMove(id, data)
	local msg = protobuf.decode("Monster.Protocol.CsPlayerStartMove", data)
	if not msg then 
		print(" playerStartMove decode failure data:" ..data)
		return
	end
	local info = player_table[id]
	info.isMove = true
	info.posInfo.posX = msg.posInfo.posX
	--info.posInfo.posY = msg.posInfo.posY
	--info.posInfo.angle =  msg.posInfo.angle
	local tb = {}
	tb.id = id;
	--tb.time = skynet.time()
	tb.posInfo = info.posInfo

	local msgbody = protobuf.encode("Monster.Protocol.ScPlayerStartMove", tb)
	local package = msgpack.pack(message.SCPLAYERSTARTMOVE, msgbody)
	print("ScPlayerStartMove")
	broadcastpackage(package, id)
end

function playerEndMove(id, data, client_fd)
	local msg = protobuf.decode("Monster.Protocol.CsPlayerEndMove", data)
		if not msg then 
		print("playerEndMovedecode failure data len:".. string.len(data))
		return
	end
 	local info = player_table[id]
	info.isMove = false;
	--info.posInfo.posX = msg.posInfo.posX
	--info.posInfo.posY = msg.posInfo.posY
	--info.posInfo.angle =  msg.posInfo.angle

	local tb = {}
	tb.id = id;
	tb.posInfo = info.posInfo

	local msgbody = protobuf.encode("Monster.Protocol.ScPlayerEndMove", tb)
	local package = msgpack.pack(message.SCPLAYERENDMOVE, msgbody)
	broadcastpackage(package, id)
end

function playerUpdateMoveDir(id, data, client_fd)
	local msg = protobuf.decode("Monster.Protocol.CsPlayerUpdateMoveDir", data)
	if not msg then 
		print("decode failure data:" ..data)
		return
	end
	if msg then
		local info = player_table[id]
		info.posInfo = msg.posInfo

		local tb = {}
		tb.id = id;
		tb.time = msg.time;
		tb.posInfo = msg.posInfo
		local msgbody = protobuf.encode("Monster.Protocol.ScPlayerUpdateMoveDir", tb)
		local package = msgpack.pack(message.SCPLAYERUPDATEMOVEDIR, msgbody)
		broadcastpackage(package, id)
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

function sendAllOtherPosInfo(id, client_fd)
	local scAllPlayerInfo = {infos = {}}
	local index = 1

	for k,v in pairs(player_table) do
		if k ~= id then
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
	end

	local msgbody = protobuf.encode("Monster.Protocol.ScAllPlayerPosInfo", scAllPlayerInfo)
	local package = msgpack.pack(message.SCALLPLAYERPOSINFO, msgbody)
	send_response(client_fd, package)
end


function initplayerposition(id)
	local info = {}

	info.id = id
	info.posInfo = {}
	info.posInfo.posX = 0
	info.posInfo.posY = 0
	info.posInfo.angle = 0
	info.isMove = false

	return info;
end

function send_response(client_fd, package)
	socket.write(client_fd, netpack.pack(package))
end

function broadcastpackage(package, selfId)
	for k,v in pairs(player_table) do
		if selfId ~= k then
			send_response(v.agent.client_fd, package)
		end
	end
end


skynet.start(function(...)
	skynet.dispatch("lua", function(_,_,cmd,...)
		f = CMD[cmd]
		skynet.ret(skynet.pack(f(...)))	
	end)
end)