local skynet = require "skynet"
local message = require "message"
local msgpack = require "msgpack.core"
local socket = require "socket"

local CMD = {}
local protobuf = {}
local fds = {}

function CMD.start()
end

function CMD.register(fd, id)
	fds[fd] = id
end

function CMD.unregister(fd)
	fds.remove(fd)
end

function CMD.dispatch(opcode, msg, fd)
	data = protobuf.decode("Monster.Protocol.CsTalk", msg)
	local tb = {}
	tb.formId = data.id
	tb.content = data.content
	local msgbody =  protobuf.encode("Monster.Protocol.ScTalk", tb)
	broadcast(fds[fd], msgbody)
end

function broadcast(id, msgbody)
	local pack = msgpack.pack(message.SCTALK, msgbody)
	for fd,v in pairs(fds)
		socket.write(fd, pack)	
	end
end

skynet.start(function(...)
	skynet.dispatch("lua", function(_, _, cmd, ...)
		local f = CMD[cmd]
		skynet.ret(skynet.pack(f(...)))	
	end)
	
	protobuf = require "protobuf"
	protobuf.register_file "../proto/talk_message.pb"
end)
