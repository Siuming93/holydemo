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

local function send_response(package)
	print("package",package)
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
		module = math.floor(data.msgno / 100)	
		opcode = data.msgno%100
		local ok, result
		if msgId[module] then
			ok, result = pcall(skynet.call, msgId[module], "lua", "dispatch", opcode, data.msg, client_fd)
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
	print("start")
	skynet.call(gate, "lua", "forward", fd)
end

function CMD.disconnect()
	print("----a client disconnect")
	skynet.call("talk")
	skynet.exit()
end

skynet.start(function()
	skynet.dispatch("lua", function(_,_, cmd, ...)
		print("---agent cmd", cmd)
		local f = CMD[cmd]
		skynet.ret(skynet.pack(f(...)))
	end)
end)
	
