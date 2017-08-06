local protobuf = require "protobuf"
local message = require "message"
local skynet = require "skynet"
require "skynet.manager"

local CMD = {}

CMD.dispatch = function (opcode, msg, ...)
    local data = protobuf.decode("CsHelloWorld", msg)
    local id = data.id
    local str = data.str
    print("id:", id)
    print("str:", str)

    local tb = {}
    tb.id = 3
    tb.str = "666"

    local msgbody = protobuf.encode("CsHelloWorld", tb)
    return message.SCHELLOWORLD, msgbody
end

skynet.start(function(...)
    skynet.dispatch("lua",function(_, _, cmd, ...)
        local f = assert(CMD[cmd])
		skynet.ret(skynet.pack(f(subcmd, ...)))
    end)
    local helloworld_data = io.open("../proto/helloworld_message.pb", "rb")
    local buffer = helloworld_data:read "*a"
    helloworld_data:close()
    protobuf.register(buffer)

    local tb = {}
    tb.str = "666"
    tb.id = 22
    local msgbody = protobuf.encode("Monster.Protocol.CsHelloWorld", tb)

   -- skynet.register "helloworldservice"
end)