local skynet = require "skynet"
local message = require "message"
local msgpack = require "msgpack.core"
require "skynet.manager"


local CMD = {}
local protobuf = {}

CMD.dispatch = function(opcode, msg)
	local data = protobuf.decode("Monster.Protocol.CMsgAccountLoginRequest", msg)
	local accountId = data.account
	print("-------------------------------------------------loginId:",accountId)
	
	local sql = "select * from table tb_account where id = '"..accountId.."' "
	local ok, result = pcall(skynet.call, "dbservice", "lua", "query", sql)	

	if ok then
		if #result == 0 then
			createplayer(accountId)
		end
	end
	local tb = {}
	tb.accountid = accountId

	local msgbody =  protobuf.encode("Monster.Protocol.CMsgAccountRegistResponse", tb)
	print(" resp ok :", message.CMSGACCOUNTLOGINRESPONSE)
	return msgpack.pack(message.CMSGACCOUNTLOGINRESPONSE, msgbody)
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

	protobuf = require "protobuf"
	protobuf.register_file "../proto/login_message.pb"

	local tb = {}
	tb.account = "55"
	protobuf.encode("Monster.Protocol.CMsgAccountLoginRequest",tb)
	
	local ltb = {}
	ltb.account = 1
	protobuf.encode("Monster.Protocol.CMsgAccountRegistResponse",{result = 12,accountid = 55})

	skynet.register "loginservice"

end)
