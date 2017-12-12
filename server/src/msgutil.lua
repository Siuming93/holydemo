
--require "TBinaryProtocol"
--require "TMemoryBuffer"


function decode(msg, data)
    local transport = TMemoryBuffer:new{}
    local protocol = TBinaryProtocol:new {
        trans = transport
    }
    transport:resetBuffer(data)

    msg:read(protocol)
    return msg;
end

function encode(msg)
    local transport = TMemoryBuffer:new{}
    local protocol = TBinaryProtocol:new {
        trans = transport
    }
    msg.wirte(protocol)
    return transport.getBuffer()
end
