--
-- Autogenerated by Thrift
--
-- DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
-- @generated
--


require 'Thrift'
require 'protocol_constants'

CsPing = __TObject:new{

}

function CsPing:read(iprot)
  iprot:readStructBegin()
  while true do
    local fname, ftype, fid = iprot:readFieldBegin()
    if ftype == TType.STOP then
      break
    else
      iprot:skip(ftype)
    end
    iprot:readFieldEnd()
  end
  iprot:readStructEnd()
end

function CsPing:write(oprot)
  oprot:writeStructBegin('CsPing')
  oprot:writeFieldStop()
  oprot:writeStructEnd()
end

ScPong = __TObject:new{

}

function ScPong:read(iprot)
  iprot:readStructBegin()
  while true do
    local fname, ftype, fid = iprot:readFieldBegin()
    if ftype == TType.STOP then
      break
    else
      iprot:skip(ftype)
    end
    iprot:readFieldEnd()
  end
  iprot:readStructEnd()
end

function ScPong:write(oprot)
  oprot:writeStructBegin('ScPong')
  oprot:writeFieldStop()
  oprot:writeStructEnd()
end

CsLogin = __TObject:new{
  accountid
}

function CsLogin:read(iprot)
  iprot:readStructBegin()
  while true do
    local fname, ftype, fid = iprot:readFieldBegin()
    if ftype == TType.STOP then
      break
    elseif fid == 1 then
      if ftype == TType.I64 then
        self.accountid = iprot:readI64()
      else
        iprot:skip(ftype)
      end
    else
      iprot:skip(ftype)
    end
    iprot:readFieldEnd()
  end
  iprot:readStructEnd()
end

function CsLogin:write(oprot)
  oprot:writeStructBegin('CsLogin')
  if self.accountid ~= nil then
    oprot:writeFieldBegin('accountid', TType.I64, 1)
    oprot:writeI64(self.accountid)
    oprot:writeFieldEnd()
  end
  oprot:writeFieldStop()
  oprot:writeStructEnd()
end

ScLogin = __TObject:new{
  result,
  accountid
}

function ScLogin:read(iprot)
  iprot:readStructBegin()
  while true do
    local fname, ftype, fid = iprot:readFieldBegin()
    if ftype == TType.STOP then
      break
    elseif fid == 1 then
      if ftype == TType.I32 then
        self.result = iprot:readI32()
      else
        iprot:skip(ftype)
      end
    elseif fid == 2 then
      if ftype == TType.I64 then
        self.accountid = iprot:readI64()
      else
        iprot:skip(ftype)
      end
    else
      iprot:skip(ftype)
    end
    iprot:readFieldEnd()
  end
  iprot:readStructEnd()
end

function ScLogin:write(oprot)
  oprot:writeStructBegin('ScLogin')
  if self.result ~= nil then
    oprot:writeFieldBegin('result', TType.I32, 1)
    oprot:writeI32(self.result)
    oprot:writeFieldEnd()
  end
  if self.accountid ~= nil then
    oprot:writeFieldBegin('accountid', TType.I64, 2)
    oprot:writeI64(self.accountid)
    oprot:writeFieldEnd()
  end
  oprot:writeFieldStop()
  oprot:writeStructEnd()
end

CsAsyncTime = __TObject:new{

}

function CsAsyncTime:read(iprot)
  iprot:readStructBegin()
  while true do
    local fname, ftype, fid = iprot:readFieldBegin()
    if ftype == TType.STOP then
      break
    else
      iprot:skip(ftype)
    end
    iprot:readFieldEnd()
  end
  iprot:readStructEnd()
end

function CsAsyncTime:write(oprot)
  oprot:writeStructBegin('CsAsyncTime')
  oprot:writeFieldStop()
  oprot:writeStructEnd()
end

ScAsyncTime = __TObject:new{
  time
}

function ScAsyncTime:read(iprot)
  iprot:readStructBegin()
  while true do
    local fname, ftype, fid = iprot:readFieldBegin()
    if ftype == TType.STOP then
      break
    elseif fid == 1 then
      if ftype == TType.DOUBLE then
        self.time = iprot:readDouble()
      else
        iprot:skip(ftype)
      end
    else
      iprot:skip(ftype)
    end
    iprot:readFieldEnd()
  end
  iprot:readStructEnd()
end

function ScAsyncTime:write(oprot)
  oprot:writeStructBegin('ScAsyncTime')
  if self.time ~= nil then
    oprot:writeFieldBegin('time', TType.DOUBLE, 1)
    oprot:writeDouble(self.time)
    oprot:writeFieldEnd()
  end
  oprot:writeFieldStop()
  oprot:writeStructEnd()
end

CsEnterScene = __TObject:new{
  name
}

function CsEnterScene:read(iprot)
  iprot:readStructBegin()
  while true do
    local fname, ftype, fid = iprot:readFieldBegin()
    if ftype == TType.STOP then
      break
    elseif fid == 1 then
      if ftype == TType.STRING then
        self.name = iprot:readString()
      else
        iprot:skip(ftype)
      end
    else
      iprot:skip(ftype)
    end
    iprot:readFieldEnd()
  end
  iprot:readStructEnd()
end

function CsEnterScene:write(oprot)
  oprot:writeStructBegin('CsEnterScene')
  if self.name ~= nil then
    oprot:writeFieldBegin('name', TType.STRING, 1)
    oprot:writeString(self.name)
    oprot:writeFieldEnd()
  end
  oprot:writeFieldStop()
  oprot:writeStructEnd()
end

ScEnterScene = __TObject:new{
  result
}

function ScEnterScene:read(iprot)
  iprot:readStructBegin()
  while true do
    local fname, ftype, fid = iprot:readFieldBegin()
    if ftype == TType.STOP then
      break
    elseif fid == 1 then
      if ftype == TType.BOOL then
        self.result = iprot:readBool()
      else
        iprot:skip(ftype)
      end
    else
      iprot:skip(ftype)
    end
    iprot:readFieldEnd()
  end
  iprot:readStructEnd()
end

function ScEnterScene:write(oprot)
  oprot:writeStructBegin('ScEnterScene')
  if self.result ~= nil then
    oprot:writeFieldBegin('result', TType.BOOL, 1)
    oprot:writeBool(self.result)
    oprot:writeFieldEnd()
  end
  oprot:writeFieldStop()
  oprot:writeStructEnd()
end

PosInfo = __TObject:new{
  posX,
  posY,
  angle
}

function PosInfo:read(iprot)
  iprot:readStructBegin()
  while true do
    local fname, ftype, fid = iprot:readFieldBegin()
    if ftype == TType.STOP then
      break
    elseif fid == 1 then
      if ftype == TType.DOUBLE then
        self.posX = iprot:readDouble()
      else
        iprot:skip(ftype)
      end
    elseif fid == 2 then
      if ftype == TType.DOUBLE then
        self.posY = iprot:readDouble()
      else
        iprot:skip(ftype)
      end
    elseif fid == 3 then
      if ftype == TType.I32 then
        self.angle = iprot:readI32()
      else
        iprot:skip(ftype)
      end
    else
      iprot:skip(ftype)
    end
    iprot:readFieldEnd()
  end
  iprot:readStructEnd()
end

function PosInfo:write(oprot)
  oprot:writeStructBegin('PosInfo')
  if self.posX ~= nil then
    oprot:writeFieldBegin('posX', TType.DOUBLE, 1)
    oprot:writeDouble(self.posX)
    oprot:writeFieldEnd()
  end
  if self.posY ~= nil then
    oprot:writeFieldBegin('posY', TType.DOUBLE, 2)
    oprot:writeDouble(self.posY)
    oprot:writeFieldEnd()
  end
  if self.angle ~= nil then
    oprot:writeFieldBegin('angle', TType.I32, 3)
    oprot:writeI32(self.angle)
    oprot:writeFieldEnd()
  end
  oprot:writeFieldStop()
  oprot:writeStructEnd()
end

ScOtherRoleEnterScene = __TObject:new{
  id,
  posInfo
}

function ScOtherRoleEnterScene:read(iprot)
  iprot:readStructBegin()
  while true do
    local fname, ftype, fid = iprot:readFieldBegin()
    if ftype == TType.STOP then
      break
    elseif fid == 1 then
      if ftype == TType.I64 then
        self.id = iprot:readI64()
      else
        iprot:skip(ftype)
      end
    elseif fid == 2 then
      if ftype == TType.STRUCT then
        self.posInfo = PosInfo:new{}
        self.posInfo:read(iprot)
      else
        iprot:skip(ftype)
      end
    else
      iprot:skip(ftype)
    end
    iprot:readFieldEnd()
  end
  iprot:readStructEnd()
end

function ScOtherRoleEnterScene:write(oprot)
  oprot:writeStructBegin('ScOtherRoleEnterScene')
  if self.id ~= nil then
    oprot:writeFieldBegin('id', TType.I64, 1)
    oprot:writeI64(self.id)
    oprot:writeFieldEnd()
  end
  if self.posInfo ~= nil then
    oprot:writeFieldBegin('posInfo', TType.STRUCT, 2)
    self.posInfo:write(oprot)
    oprot:writeFieldEnd()
  end
  oprot:writeFieldStop()
  oprot:writeStructEnd()
end

CsLeaveScene = __TObject:new{

}

function CsLeaveScene:read(iprot)
  iprot:readStructBegin()
  while true do
    local fname, ftype, fid = iprot:readFieldBegin()
    if ftype == TType.STOP then
      break
    else
      iprot:skip(ftype)
    end
    iprot:readFieldEnd()
  end
  iprot:readStructEnd()
end

function CsLeaveScene:write(oprot)
  oprot:writeStructBegin('CsLeaveScene')
  oprot:writeFieldStop()
  oprot:writeStructEnd()
end

ScLeaveScene = __TObject:new{
  id
}

function ScLeaveScene:read(iprot)
  iprot:readStructBegin()
  while true do
    local fname, ftype, fid = iprot:readFieldBegin()
    if ftype == TType.STOP then
      break
    elseif fid == 1 then
      if ftype == TType.I64 then
        self.id = iprot:readI64()
      else
        iprot:skip(ftype)
      end
    else
      iprot:skip(ftype)
    end
    iprot:readFieldEnd()
  end
  iprot:readStructEnd()
end

function ScLeaveScene:write(oprot)
  oprot:writeStructBegin('ScLeaveScene')
  if self.id ~= nil then
    oprot:writeFieldBegin('id', TType.I64, 1)
    oprot:writeI64(self.id)
    oprot:writeFieldEnd()
  end
  oprot:writeFieldStop()
  oprot:writeStructEnd()
end

PlayerPosInfo = __TObject:new{
  id,
  posInfo,
  isMove
}

function PlayerPosInfo:read(iprot)
  iprot:readStructBegin()
  while true do
    local fname, ftype, fid = iprot:readFieldBegin()
    if ftype == TType.STOP then
      break
    elseif fid == 1 then
      if ftype == TType.I64 then
        self.id = iprot:readI64()
      else
        iprot:skip(ftype)
      end
    elseif fid == 2 then
      if ftype == TType.STRUCT then
        self.posInfo = PosInfo:new{}
        self.posInfo:read(iprot)
      else
        iprot:skip(ftype)
      end
    elseif fid == 3 then
      if ftype == TType.BOOL then
        self.isMove = iprot:readBool()
      else
        iprot:skip(ftype)
      end
    else
      iprot:skip(ftype)
    end
    iprot:readFieldEnd()
  end
  iprot:readStructEnd()
end

function PlayerPosInfo:write(oprot)
  oprot:writeStructBegin('PlayerPosInfo')
  if self.id ~= nil then
    oprot:writeFieldBegin('id', TType.I64, 1)
    oprot:writeI64(self.id)
    oprot:writeFieldEnd()
  end
  if self.posInfo ~= nil then
    oprot:writeFieldBegin('posInfo', TType.STRUCT, 2)
    self.posInfo:write(oprot)
    oprot:writeFieldEnd()
  end
  if self.isMove ~= nil then
    oprot:writeFieldBegin('isMove', TType.BOOL, 3)
    oprot:writeBool(self.isMove)
    oprot:writeFieldEnd()
  end
  oprot:writeFieldStop()
  oprot:writeStructEnd()
end

ScAllPlayerPosInfo = __TObject:new{
  infos
}

function ScAllPlayerPosInfo:read(iprot)
  iprot:readStructBegin()
  while true do
    local fname, ftype, fid = iprot:readFieldBegin()
    if ftype == TType.STOP then
      break
    elseif fid == 1 then
      if ftype == TType.LIST then
        self.infos = {}
        local _etype3, _size0 = iprot:readListBegin()
        for _i=1,_size0 do
          local _elem4 = PlayerPosInfo:new{}
          _elem4:read(iprot)
          table.insert(self.infos, _elem4)
        end
        iprot:readListEnd()
      else
        iprot:skip(ftype)
      end
    else
      iprot:skip(ftype)
    end
    iprot:readFieldEnd()
  end
  iprot:readStructEnd()
end

function ScAllPlayerPosInfo:write(oprot)
  oprot:writeStructBegin('ScAllPlayerPosInfo')
  if self.infos ~= nil then
    oprot:writeFieldBegin('infos', TType.LIST, 1)
    oprot:writeListBegin(TType.STRUCT, #self.infos)
    for _,iter5 in ipairs(self.infos) do
      iter5:write(oprot)
    end
    oprot:writeListEnd()
    oprot:writeFieldEnd()
  end
  oprot:writeFieldStop()
  oprot:writeStructEnd()
end

CsPlayerStartMove = __TObject:new{
  time,
  posInfo,
  speed
}

function CsPlayerStartMove:read(iprot)
  iprot:readStructBegin()
  while true do
    local fname, ftype, fid = iprot:readFieldBegin()
    if ftype == TType.STOP then
      break
    elseif fid == 1 then
      if ftype == TType.DOUBLE then
        self.time = iprot:readDouble()
      else
        iprot:skip(ftype)
      end
    elseif fid == 2 then
      if ftype == TType.STRUCT then
        self.posInfo = PosInfo:new{}
        self.posInfo:read(iprot)
      else
        iprot:skip(ftype)
      end
    elseif fid == 3 then
      if ftype == TType.I32 then
        self.speed = iprot:readI32()
      else
        iprot:skip(ftype)
      end
    else
      iprot:skip(ftype)
    end
    iprot:readFieldEnd()
  end
  iprot:readStructEnd()
end

function CsPlayerStartMove:write(oprot)
  oprot:writeStructBegin('CsPlayerStartMove')
  if self.time ~= nil then
    oprot:writeFieldBegin('time', TType.DOUBLE, 1)
    oprot:writeDouble(self.time)
    oprot:writeFieldEnd()
  end
  if self.posInfo ~= nil then
    oprot:writeFieldBegin('posInfo', TType.STRUCT, 2)
    self.posInfo:write(oprot)
    oprot:writeFieldEnd()
  end
  if self.speed ~= nil then
    oprot:writeFieldBegin('speed', TType.I32, 3)
    oprot:writeI32(self.speed)
    oprot:writeFieldEnd()
  end
  oprot:writeFieldStop()
  oprot:writeStructEnd()
end

ScPlayerStartMove = __TObject:new{
  id,
  time,
  posInfo,
  speed
}

function ScPlayerStartMove:read(iprot)
  iprot:readStructBegin()
  while true do
    local fname, ftype, fid = iprot:readFieldBegin()
    if ftype == TType.STOP then
      break
    elseif fid == 1 then
      if ftype == TType.I64 then
        self.id = iprot:readI64()
      else
        iprot:skip(ftype)
      end
    elseif fid == 2 then
      if ftype == TType.DOUBLE then
        self.time = iprot:readDouble()
      else
        iprot:skip(ftype)
      end
    elseif fid == 3 then
      if ftype == TType.STRUCT then
        self.posInfo = PosInfo:new{}
        self.posInfo:read(iprot)
      else
        iprot:skip(ftype)
      end
    elseif fid == 4 then
      if ftype == TType.I32 then
        self.speed = iprot:readI32()
      else
        iprot:skip(ftype)
      end
    else
      iprot:skip(ftype)
    end
    iprot:readFieldEnd()
  end
  iprot:readStructEnd()
end

function ScPlayerStartMove:write(oprot)
  oprot:writeStructBegin('ScPlayerStartMove')
  if self.id ~= nil then
    oprot:writeFieldBegin('id', TType.I64, 1)
    oprot:writeI64(self.id)
    oprot:writeFieldEnd()
  end
  if self.time ~= nil then
    oprot:writeFieldBegin('time', TType.DOUBLE, 2)
    oprot:writeDouble(self.time)
    oprot:writeFieldEnd()
  end
  if self.posInfo ~= nil then
    oprot:writeFieldBegin('posInfo', TType.STRUCT, 3)
    self.posInfo:write(oprot)
    oprot:writeFieldEnd()
  end
  if self.speed ~= nil then
    oprot:writeFieldBegin('speed', TType.I32, 4)
    oprot:writeI32(self.speed)
    oprot:writeFieldEnd()
  end
  oprot:writeFieldStop()
  oprot:writeStructEnd()
end

CsPlayerEndMove = __TObject:new{
  posInfo,
  time
}

function CsPlayerEndMove:read(iprot)
  iprot:readStructBegin()
  while true do
    local fname, ftype, fid = iprot:readFieldBegin()
    if ftype == TType.STOP then
      break
    elseif fid == 1 then
      if ftype == TType.STRUCT then
        self.posInfo = PosInfo:new{}
        self.posInfo:read(iprot)
      else
        iprot:skip(ftype)
      end
    elseif fid == 2 then
      if ftype == TType.DOUBLE then
        self.time = iprot:readDouble()
      else
        iprot:skip(ftype)
      end
    else
      iprot:skip(ftype)
    end
    iprot:readFieldEnd()
  end
  iprot:readStructEnd()
end

function CsPlayerEndMove:write(oprot)
  oprot:writeStructBegin('CsPlayerEndMove')
  if self.posInfo ~= nil then
    oprot:writeFieldBegin('posInfo', TType.STRUCT, 1)
    self.posInfo:write(oprot)
    oprot:writeFieldEnd()
  end
  if self.time ~= nil then
    oprot:writeFieldBegin('time', TType.DOUBLE, 2)
    oprot:writeDouble(self.time)
    oprot:writeFieldEnd()
  end
  oprot:writeFieldStop()
  oprot:writeStructEnd()
end

ScPlayerEndMove = __TObject:new{
  id,
  posInfo
}

function ScPlayerEndMove:read(iprot)
  iprot:readStructBegin()
  while true do
    local fname, ftype, fid = iprot:readFieldBegin()
    if ftype == TType.STOP then
      break
    elseif fid == 1 then
      if ftype == TType.I64 then
        self.id = iprot:readI64()
      else
        iprot:skip(ftype)
      end
    elseif fid == 2 then
      if ftype == TType.STRUCT then
        self.posInfo = PosInfo:new{}
        self.posInfo:read(iprot)
      else
        iprot:skip(ftype)
      end
    else
      iprot:skip(ftype)
    end
    iprot:readFieldEnd()
  end
  iprot:readStructEnd()
end

function ScPlayerEndMove:write(oprot)
  oprot:writeStructBegin('ScPlayerEndMove')
  if self.id ~= nil then
    oprot:writeFieldBegin('id', TType.I64, 1)
    oprot:writeI64(self.id)
    oprot:writeFieldEnd()
  end
  if self.posInfo ~= nil then
    oprot:writeFieldBegin('posInfo', TType.STRUCT, 2)
    self.posInfo:write(oprot)
    oprot:writeFieldEnd()
  end
  oprot:writeFieldStop()
  oprot:writeStructEnd()
end

CsPlayerUpdateMoveDir = __TObject:new{
  posInfo,
  time
}

function CsPlayerUpdateMoveDir:read(iprot)
  iprot:readStructBegin()
  while true do
    local fname, ftype, fid = iprot:readFieldBegin()
    if ftype == TType.STOP then
      break
    elseif fid == 1 then
      if ftype == TType.STRUCT then
        self.posInfo = PosInfo:new{}
        self.posInfo:read(iprot)
      else
        iprot:skip(ftype)
      end
    elseif fid == 2 then
      if ftype == TType.DOUBLE then
        self.time = iprot:readDouble()
      else
        iprot:skip(ftype)
      end
    else
      iprot:skip(ftype)
    end
    iprot:readFieldEnd()
  end
  iprot:readStructEnd()
end

function CsPlayerUpdateMoveDir:write(oprot)
  oprot:writeStructBegin('CsPlayerUpdateMoveDir')
  if self.posInfo ~= nil then
    oprot:writeFieldBegin('posInfo', TType.STRUCT, 1)
    self.posInfo:write(oprot)
    oprot:writeFieldEnd()
  end
  if self.time ~= nil then
    oprot:writeFieldBegin('time', TType.DOUBLE, 2)
    oprot:writeDouble(self.time)
    oprot:writeFieldEnd()
  end
  oprot:writeFieldStop()
  oprot:writeStructEnd()
end

ScPlayerUpdateMoveDir = __TObject:new{
  id,
  posInfo,
  time
}

function ScPlayerUpdateMoveDir:read(iprot)
  iprot:readStructBegin()
  while true do
    local fname, ftype, fid = iprot:readFieldBegin()
    if ftype == TType.STOP then
      break
    elseif fid == 1 then
      if ftype == TType.I64 then
        self.id = iprot:readI64()
      else
        iprot:skip(ftype)
      end
    elseif fid == 2 then
      if ftype == TType.STRUCT then
        self.posInfo = PosInfo:new{}
        self.posInfo:read(iprot)
      else
        iprot:skip(ftype)
      end
    elseif fid == 3 then
      if ftype == TType.DOUBLE then
        self.time = iprot:readDouble()
      else
        iprot:skip(ftype)
      end
    else
      iprot:skip(ftype)
    end
    iprot:readFieldEnd()
  end
  iprot:readStructEnd()
end

function ScPlayerUpdateMoveDir:write(oprot)
  oprot:writeStructBegin('ScPlayerUpdateMoveDir')
  if self.id ~= nil then
    oprot:writeFieldBegin('id', TType.I64, 1)
    oprot:writeI64(self.id)
    oprot:writeFieldEnd()
  end
  if self.posInfo ~= nil then
    oprot:writeFieldBegin('posInfo', TType.STRUCT, 2)
    self.posInfo:write(oprot)
    oprot:writeFieldEnd()
  end
  if self.time ~= nil then
    oprot:writeFieldBegin('time', TType.DOUBLE, 3)
    oprot:writeDouble(self.time)
    oprot:writeFieldEnd()
  end
  oprot:writeFieldStop()
  oprot:writeStructEnd()
end

CsPlayerEndMovePos = __TObject:new{
  posInfo
}

function CsPlayerEndMovePos:read(iprot)
  iprot:readStructBegin()
  while true do
    local fname, ftype, fid = iprot:readFieldBegin()
    if ftype == TType.STOP then
      break
    elseif fid == 1 then
      if ftype == TType.STRUCT then
        self.posInfo = PosInfo:new{}
        self.posInfo:read(iprot)
      else
        iprot:skip(ftype)
      end
    else
      iprot:skip(ftype)
    end
    iprot:readFieldEnd()
  end
  iprot:readStructEnd()
end

function CsPlayerEndMovePos:write(oprot)
  oprot:writeStructBegin('CsPlayerEndMovePos')
  if self.posInfo ~= nil then
    oprot:writeFieldBegin('posInfo', TType.STRUCT, 1)
    self.posInfo:write(oprot)
    oprot:writeFieldEnd()
  end
  oprot:writeFieldStop()
  oprot:writeStructEnd()
end

ScPlayerEndMovePos = __TObject:new{
  id,
  posInfo
}

function ScPlayerEndMovePos:read(iprot)
  iprot:readStructBegin()
  while true do
    local fname, ftype, fid = iprot:readFieldBegin()
    if ftype == TType.STOP then
      break
    elseif fid == 1 then
      if ftype == TType.I64 then
        self.id = iprot:readI64()
      else
        iprot:skip(ftype)
      end
    elseif fid == 2 then
      if ftype == TType.STRUCT then
        self.posInfo = PosInfo:new{}
        self.posInfo:read(iprot)
      else
        iprot:skip(ftype)
      end
    else
      iprot:skip(ftype)
    end
    iprot:readFieldEnd()
  end
  iprot:readStructEnd()
end

function ScPlayerEndMovePos:write(oprot)
  oprot:writeStructBegin('ScPlayerEndMovePos')
  if self.id ~= nil then
    oprot:writeFieldBegin('id', TType.I64, 1)
    oprot:writeI64(self.id)
    oprot:writeFieldEnd()
  end
  if self.posInfo ~= nil then
    oprot:writeFieldBegin('posInfo', TType.STRUCT, 2)
    self.posInfo:write(oprot)
    oprot:writeFieldEnd()
  end
  oprot:writeFieldStop()
  oprot:writeStructEnd()
end

CsPlayerUseSkill = __TObject:new{
  skillId
}

function CsPlayerUseSkill:read(iprot)
  iprot:readStructBegin()
  while true do
    local fname, ftype, fid = iprot:readFieldBegin()
    if ftype == TType.STOP then
      break
    elseif fid == 1 then
      if ftype == TType.I32 then
        self.skillId = iprot:readI32()
      else
        iprot:skip(ftype)
      end
    else
      iprot:skip(ftype)
    end
    iprot:readFieldEnd()
  end
  iprot:readStructEnd()
end

function CsPlayerUseSkill:write(oprot)
  oprot:writeStructBegin('CsPlayerUseSkill')
  if self.skillId ~= nil then
    oprot:writeFieldBegin('skillId', TType.I32, 1)
    oprot:writeI32(self.skillId)
    oprot:writeFieldEnd()
  end
  oprot:writeFieldStop()
  oprot:writeStructEnd()
end

ScPlayerUseSkill = __TObject:new{
  id,
  skillId
}

function ScPlayerUseSkill:read(iprot)
  iprot:readStructBegin()
  while true do
    local fname, ftype, fid = iprot:readFieldBegin()
    if ftype == TType.STOP then
      break
    elseif fid == 1 then
      if ftype == TType.I64 then
        self.id = iprot:readI64()
      else
        iprot:skip(ftype)
      end
    elseif fid == 2 then
      if ftype == TType.I32 then
        self.skillId = iprot:readI32()
      else
        iprot:skip(ftype)
      end
    else
      iprot:skip(ftype)
    end
    iprot:readFieldEnd()
  end
  iprot:readStructEnd()
end

function ScPlayerUseSkill:write(oprot)
  oprot:writeStructBegin('ScPlayerUseSkill')
  if self.id ~= nil then
    oprot:writeFieldBegin('id', TType.I64, 1)
    oprot:writeI64(self.id)
    oprot:writeFieldEnd()
  end
  if self.skillId ~= nil then
    oprot:writeFieldBegin('skillId', TType.I32, 2)
    oprot:writeI32(self.skillId)
    oprot:writeFieldEnd()
  end
  oprot:writeFieldStop()
  oprot:writeStructEnd()
end

ScPlayerCheckFailured = __TObject:new{
  posInfo
}

function ScPlayerCheckFailured:read(iprot)
  iprot:readStructBegin()
  while true do
    local fname, ftype, fid = iprot:readFieldBegin()
    if ftype == TType.STOP then
      break
    elseif fid == 1 then
      if ftype == TType.STRUCT then
        self.posInfo = PosInfo:new{}
        self.posInfo:read(iprot)
      else
        iprot:skip(ftype)
      end
    else
      iprot:skip(ftype)
    end
    iprot:readFieldEnd()
  end
  iprot:readStructEnd()
end

function ScPlayerCheckFailured:write(oprot)
  oprot:writeStructBegin('ScPlayerCheckFailured')
  if self.posInfo ~= nil then
    oprot:writeFieldBegin('posInfo', TType.STRUCT, 1)
    self.posInfo:write(oprot)
    oprot:writeFieldEnd()
  end
  oprot:writeFieldStop()
  oprot:writeStructEnd()
end