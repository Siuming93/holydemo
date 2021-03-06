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
	data = msgpack.unpack(package)
	print(" resp ok :", data.msgno)
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
		print("originmsg = ",msg)
		print("msglen =",string.len(msg))
		print("msgno = ", data.msgno)
		print("msg = ", data.msg)
		local ok, result
		if msgId[module] then
			ok, msgcode, body = pcall(skynet.call, msgId[module], "lua", "dispatch", opcode, data.msg)
			if ok then
				--send_response(msgpack.pack(msgcode, body))
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
	skynet.exit()
end

skynet.start(function()
	skynet.dispatch("lua", function(_,_, cmd, ...)
		print("---agent cmd", cmd)
		local f = CMD[cmd]
		skynet.ret(skynet.pack(f(...)))
	end)
end)
	
