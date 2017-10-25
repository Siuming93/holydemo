local skynet = require "skynet"
require "skynet.manager"
local netpack = require "netpack"
local socket = require "socket"
local message = require "message"
local msgId = require "msgId"
msgpack = require "msgpack.core"

local host

local CMD = {}
local client_fd = {}
local player_info = {}

local agentInterface ={}

function send_response(package)
	print("package",package)
	print("client_fd",client_fd)
	socket.write(client_fd, netpack.pack(package))
end

skynet.register_protocol {
	name = "client",
	id = skynet.PTYPE_CLIENT,
	unpack = function (msg, sz)
		return skynet.tostring(msg,sz)
	end,
	dispatch = function(_, _, msg)
		print("------------client dispacth-----------")
		
		data = msgpack.unpack(msg)
		print("msgno", data.msgno)
		module = math.floor(data.msgno / 100)	
		opcode = data.msgno%100
		local ok, result
		--login
		if msgId[module] == "loginservice" then
			ok, result, playerId = pcall(skynet.call, "loginservice", "lua", "dispatch", opcode, data.msg)
			if ok then
				player_info.id = playerId
				send_response(result)
				print("playerid:",player_info.id)
				agentInterface.player_info = player_info
				return
			end
			return
		end

		if player_info == nil then
			print("please login first")
			return
		end

		if msgId[module] == "leitaiservice" then
			print("self",agentInterface)
			pcall(skynet.call, "leitaiservice", "lua", "dispatch", opcode, data.msg, agentInterface)
			return
		end

		if msgId[module] then
			ok, result = pcall(skynet.call, msgId[module], "lua", "dispatch", opcode, data.msg, agentInterface)
			if ok then
				send_response(result)
			else
				print("role error")
			end
		else
			print("server receive error msg")
		end
	end
}

function CMD.start(gate, fd, proto)
	client_fd = fd
	agentInterface.client_fd = fd;
	print("start")
	skynet.call(gate, "lua", "forward", fd)
end

function CMD.disconect()
	print("----a client disconnect")
	pcall(skynet.call, "leitaiservice", "lua", "disconect", agentInterface)
	skynet.exit()
end

skynet.start(function()
	skynet.dispatch("lua", function(_,_, cmd, ...)
		print("---agent cmd", cmd)
		local f = CMD[cmd]
		skynet.ret(skynet.pack(f(...)))
	end)
end)
	
