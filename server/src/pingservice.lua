local skynet = require "skynet"
local message = require "message"
local msgpack = require "msgpack.core"

require "msgUtil"
require "protocol_ttypes"
require "skynet.manager"


local CMD = {}

function CMD.dispatch(...)
    local tb = ScPong:new{}
    local msgbody = encode(tb)
	return msgpack.pack(message.SCPONG, msgbody)
end

skynet.start(function(...)
    skynet.dispatch("lua", function(_,_,cmd,...)
        f = CMD[cmd]
        skynet.ret(skynet.pack(f(...)))
    end)

    skynet.register "pingservice"
end)