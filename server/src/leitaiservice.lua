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
    if opcode == message.CSENTERSCENE%100 then
		enterscene(id, agent)
	end
	if opcode == message.CSPLAYERMOVE%100 then
		updatePos(id, msg)
	end
	if opcode == message.CSPLAYERISMOVE%100 then
		updateIsMove(id, msg)
	end
	print "end"
end

CMD.disconect = function (agent)
	leaveScene(agent)
end

function enterscene(id, agent)
	print("enterscene, agent",agent)
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

	local has = protobuf.check("Monster.Protocol.CsPlayerMove;")


	local info = player_table[id]
	info.posX = msg.info.posX;
	info.posY = msg.info.posY;
	info.rotX = msg.info.rotX;
	info.rotY = msg.info.rotY;
end

function updateIsMove(id, data)
	local msg = protobuf.decode("Monster.Protocol.CsPlayerIsMove", data)
	local info = player_table[id]
	info.isMove = msg.isMove
end

function initplayerposition()
	local info = {}
	info.posX = 0
	info.posY = 0
	info.rotX = 0
	info.rotY = 0
	info.isMove = false
	return info;
end

function send_response(client_fd, package)
	print("package",package)
	print("client fd", client_fd)
	socket.write(client_fd, netpack.pack(package))
end

function broadcastpackage(package)
	for k,v in pairs(player_table) do
		send_response(v.agent.client_fd, package)
	end
end

function playerInfoMsg()
	local scAllPlayerInfo = {infos = {}}
	local index = 1

	
	for k,v in pairs(player_table) do
		local playerPosInfo = {}
		playerPosInfo.id = k;
		playerPosInfo.posX = v.posX;
		playerPosInfo.posY = v.posY;
		playerPosInfo.rotX = v.rotX;
		playerPosInfo.rotY = v.rotY;
		playerPosInfo.isMove = v.isMove;
		print("broadcast ismove", v.isMove)
		scAllPlayerInfo.infos[index] = playerPosInfo
	end

	local msgbody = protobuf.encode("Monster.Protocol.ScAllPlayerPosInfo", scAllPlayerInfo)
	local msg = protobuf.decode("Monster.Protocol.ScAllPlayerPosInfo", msgbody)
	if msg.infos[1] then
	print("msg decode", msg.infos[1].id)
	end
    return msgpack.pack(message.SCALLPLAYERPOSINFO, msgbody)
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