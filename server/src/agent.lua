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
	--print("package",package)
	--print("client_fd",client_fd)
	socket.write(client_fd, netpack.pack(package))
end

skynet.register_protocol {
	name = "client",
	id = skynet.PTYPE_CLIENT,
	unpack = function (msg, sz)
		return skynet.tostring(msg,sz)
	end,
	dispatch = function(_, _, msg)		
		data = msgpack.unpack(msg)
		module = msgId[math.floor(data.msgno / 100)]	
		opcode = data.msgno%10000
		local ok, result
		--print("msgno = ", data.msgno)
		--login
		if data.msgno == message.CSLOGIN then
			ok, result, playerId = pcall(skynet.call, "loginservice", "lua", "dispatch", opcode, data.msg)
			if ok then
				player_info.id = playerId
				send_response(result)
				agentInterface.player_info = player_info
				print("player_info",agentInterface.player_info)				
				return
			end
			return
		end

		if player_info == nil then
			print("please login first")
			return
		end

		if module then
			ok, result = pcall(skynet.call, module, "lua", "dispatch", opcode, data.msg, agentInterface)
			if ok then
				if result then
					send_response(result)
				end
			else
				print("role error", data.msgno,module)
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
	
