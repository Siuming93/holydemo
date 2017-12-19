local skynet = require "skynet"
local message = require "message"
local msgpack = require "msgpack.core"
local socket = require "socket"
local netpack = require "netpack"
local long = require "liblualongnumber"
require "msgUtil"
require "protocol_ttypes"
require "skynet.manager"
require "skynet.manager"

local CMD = {}

local player_table = {}

CMD.dispatch = function (opcode, msg, agent, ...)
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
	print("enterscene")
	local info = initplayerposition(id)
	player_table[id] = info
	player_table[id].agent = agent;
	local selfTb = ScEnterScene:new{};
	selfTb.id = id
	selfTb.result = true
	local selfMsg = encode(selfTb)
	local package = msgpack.pack(message.SCENTERSCENE, selfMsg)
	
	send_response(agent.client_fd, package)
	local broadTb = ScOtherRoleEnterScene:new{}
	broadTb.id = id
	broadTb.posInfo = getRolePosMsg(id)
	local broadMsg = encode(broadTb)

	local msg2 = decode(ScOtherRoleEnterScene:new{}, broadMsg)
	print("id",msg2.id)
	print("posInfo",msg2.posInfo)

	local package = msgpack.pack(message.SCOTHERROLEENTERSCENE, broadMsg)
	sendAllOtherPosInfo(id, client_fd)
	broadcastpackage(package, id)
end

function leaveScene(id)
	player_table[id] = nil
end

function playerStartMove(id, data)
	local msg = decode(CsPlayerStartMove:new{}, data)
	local info = player_table[id]
	info.isMove = true
	info.posInfo.posX = msg.posInfo.posX
	info.posInfo.posY = msg.posInfo.posY
	info.posInfo.angle =  msg.posInfo.angle
	local tb = ScPlayerStartMove:new{}
	tb.id = id
	tb.time = msg.time
	tb.posInfo = getRolePosMsg(id)
	tb.speed = msg.speed
	local msgbody = encode(tb)
	local package = msgpack.pack(message.SCPLAYERSTARTMOVE, msgbody)
	broadcastpackage(package, id)
end

function playerEndMove(id, data, client_fd)
	local msg = decode(CsPlayerEndMove:new{}, data)

 	local info = player_table[id]
	info.isMove = false;
	info.posInfo.posX = msg.posInfo.posX
	info.posInfo.posY = msg.posInfo.posY
	info.posInfo.angle =  msg.posInfo.angle

	local tb = ScPlayerEndMove:new{}
	tb.id = id;
	tb.posInfo = getRolePosMsg(id)

	local msgbody = encode(tb)
	local package = msgpack.pack(message.SCPLAYERENDMOVE, msgbody)
	broadcastpackage(package, id)
end

function playerUpdateMoveDir(id, data, client_fd)
	local msg = decode(CsPlayerUpdateMoveDir:new{}, data)

	local info = player_table[id]
	setRolePosMsg(id, msg.posInfo)

	local tb = ScPlayerUpdateMoveDir:new{}
	tb.id = id;
	tb.time = msg.time;
	tb.posInfo = msg.posInfo
	print("angle ",msg.posInfo.angle)
	
	local msgbody = encode(tb)
	local package = msgpack.pack(message.SCPLAYERUPDATEMOVEDIR, msgbody)
	broadcastpackage(package, id)
end

function getRolePosMsg(id)
	local info = player_table[id]
	local msg = PosInfo:new{}
	msg.posX = info.posInfo.posX
	msg.posY = info.posInfo.posY
	msg.angle = info.posInfo.angle
	return msg
end

function setRolePosMsg(id, posInfo)
	local info = player_table[id]
	info.posInfo.posX = posInfo.posX 
	info.posInfo.posY = posInfo.posY
	info.posInfo.angle = posInfo.angle
end

function playerAsyncPosAndDir(id, data)
	local msg = decode(CsPlayerAsyncPosAndDir:new{}, data)
	if msg then
		local info = player_table[id]
		info.dirX = msg.posInfo.dirX;
		info.dirY = msg.posInfo.dirY;
		info.posX = msg.posInfo.posX;
		info.posY = msg.posInfo.posY;
	end
end

function sendAllOtherPosInfo(id, client_fd)
	local scAllPlayerInfo = ScAllPlayerPosInfo:new{}
	scAllPlayerInfo.infos = {}
	local index = 1

	for k,v in pairs(player_table) do
		if k ~= id then
			local playerPosInfo = PlayerPosInfo:new{}
			playerPosInfo.posInfo = {}
			playerPosInfo.id = k;
			playerPosInfo.posInfo = getRolePosMsg(k)
			playerPosInfo.isMove = v.isMove;
			scAllPlayerInfo.infos[index] = playerPosInfo
		index = index + 1
		end
	end

	local msgbody = encode(scAllPlayerInfo)
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

	skynet.register "leitaiservice"
end)