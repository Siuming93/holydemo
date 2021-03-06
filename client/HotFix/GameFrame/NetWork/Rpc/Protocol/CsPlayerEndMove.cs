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
  public partial class CsPlayerEndMove : TBase
  {

    public PosInfo PosInfo { get; set; }

    public double Time { get; set; }

    public CsPlayerEndMove() {
    }

    public CsPlayerEndMove(PosInfo posInfo, double time) : this() {
      this.PosInfo = posInfo;
      this.Time = time;
    }

    public void Read (TProtocol iprot)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        bool isset_posInfo = false;
        bool isset_time = false;
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
              if (field.Type == TType.Struct) {
                PosInfo = new PosInfo();
                PosInfo.Read(iprot);
                isset_posInfo = true;
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 2:
              if (field.Type == TType.Double) {
                Time = iprot.ReadDouble();
                isset_time = true;
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
        if (!isset_posInfo)
          throw new TProtocolException(TProtocolException.INVALID_DATA);
        if (!isset_time)
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
        TStruct struc = new TStruct("CsPlayerEndMove");
        oprot.WriteStructBegin(struc);
        TField field = new TField();
        field.Name = "posInfo";
        field.Type = TType.Struct;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        PosInfo.Write(oprot);
        oprot.WriteFieldEnd();
        field.Name = "time";
        field.Type = TType.Double;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        oprot.WriteDouble(Time);
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
      StringBuilder __sb = new StringBuilder("CsPlayerEndMove(");
      __sb.Append(", PosInfo: ");
      __sb.Append(PosInfo== null ? "<null>" : PosInfo.ToString());
      __sb.Append(", Time: ");
      __sb.Append(Time);
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}
