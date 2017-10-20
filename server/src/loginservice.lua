local skynet = require "skynet"
local message = require "message"
local msgpack = require "msgpack.core"
require "skynet.manager"


local CMD = {}
local protobuf = {}

CMD.dispatch = function(opcode, msg, fd)
	local data = protobuf.decode("Monster.Protocol.CsLogin", msg)
	local accountId = data.account
	
	local sql = "select * from table tb_account where id = '"..accountId.."' "
	local ok, result = pcall(skynet.call, "dbservice", "lua", "query", sql)	

	if ok then
		if result == 0 then
			createplayer(accountId)
		end
	end
	
	local tb = {}
	tb.accountid = accountId
	tb.result = 1
	--skynet.call("talkservice", "lua", "register", fd, accountId)
	local msgbody =  protobuf.encode("Monster.Protocol.ScLogin", tb)
	return msgpack.pack(message.SCLOGIN, msgbody)
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


	skynet.register "loginservice"

end)
