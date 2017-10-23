local protobuf = require "protobuf"
local message = require "message"
local skynet = require "skynet"
require "skynet.manager"

local CMD = {}

local player_table = {}

CMD.dispatch = function (opcode, msg, ...)
    local data = protobuf.decode("CsEnterScene", msg)

    local msgbody = protobuf.encode("ScEnterScene", tb)
    return message.SCHELLOWORLD, msgbody
end

function enterscene(id)
{
	player_table[id] = initplayerposition()
}

function leaveScene(id)
{
	player_table[id] = nil
}

function updatePos(id, newInfo)
{
	local info = player_table[id]
	info.posX = newInfo.posX;
	info.posY = newInfo.posY;
	info.rotX = newInfo.rotX;
	info.rotY = newInfo.rotY;
}

function initplayerposition()
{
	local info = {}
	info.posX = 0
	info.posY = 0
	info.rotX = 0
	info.rotY = 0
}

function 

skynet.start(function(...)
	skynet.dispatch("lua", function(_,_,cmd,...)
		f = CMD[cmd]
		skynet.ret(skynet.pack(f(...)))	
	end)

	protobuf = require "protobuf"
	protobuf.register_file "../proto/scene_message.pb"


	skynet.register "leitaiservice"

end)