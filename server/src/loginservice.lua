local skynet = require "skynet"
local message = require "message"
local msgpack = require "msgpack.core"
require "msgUtil"
require ('protocol_ttypes')
require "skynet.manager"




local CMD = {}
local protobuf = {}

CMD.dispatch = function(opcode, msg, fd)
	if opcode == message.CSLOGIN % 10000 then
		return OnCsLogin(msg, fd)
	end
	if opcode == message.CSASYNCTIME % 10000 then
		return OnAsyncTime(msg)
	end
end

function OnCsLogin (msg, fd)
	local data = decode(CsLogin:new{}, msg)
	local accountId = tostring(data.accountid)
	
	local sql = "select * from table tb_account where id = '"..accountId.."' "
	local ok, result = pcall(skynet.call, "dbservice", "lua", "query", sql)	

	if ok then
		if result == 0 then
			createplayer(accountId)
		end
	end

	local tb = ScLogin:new{}
	tb.accountid = accountId
	tb.result = 1

	local msgbody =  encode(tb)
	return msgpack.pack(message.SCLOGIN, msgbody), accountId
end

function OnAsyncTime(msg)
	local tb = ScAsyncTime:new{}
	tb.time = skynet.time() * 1000
	local msgbody = encode(tb)
	return msgpack.pack(message.SCASYNCTIME, msgbody)
end


function createplayer(accountId)
	local sql = "insert into tb_account( id = '"..accountId.."')"
	local ok, result = pcall(skynet.call, "dbservice", "lua", "query", sql)	
	if ok then
		print("create new player:"..accountId)
	else
		print("failed to create new player")
	end
end

skynet.start(function(...)
	skynet.dispatch("lua", function(_,_,cmd,...)
		f = CMD[cmd]
		skynet.ret(skynet.pack(f(...)))	
	end)

	skynet.register "loginservice"

end)
