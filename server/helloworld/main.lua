local skynet = require "skynet"

local max_client = 64;

skynet.start(function()
	print("----server start----")

	local watchdog = skynet.newservice("watchdog")
	skynet.call(watchdog, "lua", "start",{
		port = 8888,
		maxclient = max_client,
		nodelay = true,
		})
	print("watchdog listen on 8888")

	skynet.exit()	
end)
