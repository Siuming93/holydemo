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
  public partial class PosInfo : TBase
  {

    public double PosX { get; set; }

    public double PosY { get; set; }

    public int Angle { get; set; }

    public PosInfo() {
    }

    public PosInfo(double posX, double posY, int angle) : this() {
      this.PosX = posX;
      this.PosY = posY;
      this.Angle = angle;
    }

    public void Read (TProtocol iprot)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        bool isset_posX = false;
        bool isset_posY = false;
        bool isset_angle = false;
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
              if (field.Type == TType.Double) {
                PosX = iprot.ReadDouble();
                isset_posX = true;
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 2:
              if (field.Type == TType.Double) {
                PosY = iprot.ReadDouble();
                isset_posY = true;
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 3:
              if (field.Type == TType.I32) {
                Angle = iprot.ReadI32();
                isset_angle = true;
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
        if (!isset_posX)
          throw new TProtocolException(TProtocolException.INVALID_DATA);
        if (!isset_posY)
          throw new TProtocolException(TProtocolException.INVALID_DATA);
        if (!isset_angle)
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
        TStruct struc = new TStruct("PosInfo");
        oprot.WriteStructBegin(struc);
        TField field = new TField();
        field.Name = "posX";
        field.Type = TType.Double;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        oprot.WriteDouble(PosX);
        oprot.WriteFieldEnd();
        field.Name = "posY";
        field.Type = TType.Double;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        oprot.WriteDouble(PosY);
        oprot.WriteFieldEnd();
        field.Name = "angle";
        field.Type = TType.I32;
        field.ID = 3;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(Angle);
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
      StringBuilder __sb = new StringBuilder("PosInfo(");
      __sb.Append(", PosX: ");
      __sb.Append(PosX);
      __sb.Append(", PosY: ");
      __sb.Append(PosY);
      __sb.Append(", Angle: ");
      __sb.Append(Angle);
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}