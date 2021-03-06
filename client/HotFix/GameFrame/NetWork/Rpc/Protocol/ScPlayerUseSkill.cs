/**
 * Autogenerated by Thrift Compiler (0.10.0)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Thrift;
using Thrift.Collections;
using System.Runtime.Serialization;
using Thrift.Protocol;
using Thrift.Transport;

namespace RedDragon.Protocol
{

  #if !SILVERLIGHT
  [Serializable]
  #endif
  public partial class ScPlayerUseSkill : TBase
  {

    public long Id { get; set; }

    public int SkillId { get; set; }

    public ScPlayerUseSkill() {
    }

    public ScPlayerUseSkill(long id, int skillId) : this() {
      this.Id = id;
      this.SkillId = skillId;
    }

    public void Read (TProtocol iprot)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        bool isset_id = false;
        bool isset_skillId = false;
        TField field;
        iprot.ReadStructBegin();
        while (true)
        {
          field = iprot.ReadFieldBegin();
          if (field.Type == TType.Stop) { 
            break;
          }
          switch (field.ID)
          {
            case 1:
              if (field.Type == TType.I64) {
                Id = iprot.ReadI64();
                isset_id = true;
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 2:
              if (field.Type == TType.I32) {
                SkillId = iprot.ReadI32();
                isset_skillId = true;
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            default: 
              TProtocolUtil.Skip(iprot, field.Type);
              break;
          }
          iprot.ReadFieldEnd();
        }
        iprot.ReadStructEnd();
        if (!isset_id)
          throw new TProtocolException(TProtocolException.INVALID_DATA);
        if (!isset_skillId)
          throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public void Write(TProtocol oprot) {
      oprot.IncrementRecursionDepth();
      try
      {
        TStruct struc = new TStruct("ScPlayerUseSkill");
        oprot.WriteStructBegin(struc);
        TField field = new TField();
        field.Name = "id";
        field.Type = TType.I64;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        oprot.WriteI64(Id);
        oprot.WriteFieldEnd();
        field.Name = "skillId";
        field.Type = TType.I32;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(SkillId);
        oprot.WriteFieldEnd();
        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("ScPlayerUseSkill(");
      __sb.Append(", Id: ");
      __sb.Append(Id);
      __sb.Append(", SkillId: ");
      __sb.Append(SkillId);
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}
