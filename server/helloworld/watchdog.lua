local skynet = require "skynet"
require "skynet.manager"
local netpack = require "netpack"

local CMD = {}
local SOCKET = {}
local agent = {}
local gate

function SOCKET.open(fd, addr)
	skynet.error("New client from : ".. addr)
	agent[fd] = skynet.newservice("agent")
	skynet.call(agent[fd], "lua", "start", gage, fd, proto)
end

local function close_agent( fd )
	local a = agent[fd]
	agent[fd] = nil
	if a then 
		skynet.call(gate, "lua", "kick", fd)
		skynet.send(a, "lua", "disconect")
	end
end

function SOCKET.close( fd )
	print("sokcet close", fd)
	close_agent(fd)
end

function SOCKET.error( fd, msg)
	print("socket error",fd, msg)
	close_agent(fd)
end

function SOCKET.date(fd, msg)
end

function CMD.start( conf )
	skynet.call(gate, "lua", "open", conf)
end

function CMD.close( fd )
	close_agent(fd)
end

skynet.start(function()
	print("----start watchdog----")
	skynet.dispacth("lua", function(session, source, cmd, subcmd, ...)
		print("---watchdog cmd ", cmd)
		if cmd == "socket" then
			print("---watchdog subcmd", subcmd)
			local f = SOCKET[subcmd]
			f(...)
			--socket api dont need returm
		else
			local f = assert(CMD[cmd])
			skynet.ret(skynet.pack(f(subcmd, ...)))
		end
	end)

	gate = skynet.newservice("gate")
end)