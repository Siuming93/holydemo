//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: scene_message.proto
namespace Monster.Protocol
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CsEnterScene")]
  public partial class CsEnterScene : global::ProtoBuf.IExtensible
  {
    public CsEnterScene() {}
    
    private int _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int id
    {
      get { return _id; }
      set { _id = value; }
    }
    private string _name;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string name
    {
      get { return _name; }
      set { _name = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ScEnterScene")]
  public partial class ScEnterScene : global::ProtoBuf.IExtensible
  {
    public ScEnterScene() {}
    
    private int _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int id
    {
      get { return _id; }
      set { _id = value; }
    }
    private int _result;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"result", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int result
    {
      get { return _result; }
      set { _result = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"PlayerPosInfo")]
  public partial class PlayerPosInfo : global::ProtoBuf.IExtensible
  {
    public PlayerPosInfo() {}
    
    private long _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long id
    {
      get { return _id; }
      set { _id = value; }
    }
    private float _posX;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"posX", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    public float posX
    {
      get { return _posX; }
      set { _posX = value; }
    }
    private float _posY;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"posY", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    public float posY
    {
      get { return _posY; }
      set { _posY = value; }
    }
    private float _dirX;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"dirX", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    public float dirX
    {
      get { return _dirX; }
      set { _dirX = value; }
    }
    private float _dirY;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, Name=@"dirY", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    public float dirY
    {
      get { return _dirY; }
      set { _dirY = value; }
    }
    private bool _isMove;
    [global::ProtoBuf.ProtoMember(6, IsRequired = true, Name=@"isMove", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public bool isMove
    {
      get { return _isMove; }
      set { _isMove = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CsPlayerMove")]
  public partial class CsPlayerMove : global::ProtoBuf.IExtensible
  {
    public CsPlayerMove() {}
    
    private float _dirX = default(float);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"dirX", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue(default(float))]
    public float dirX
    {
      get { return _dirX; }
      set { _dirX = value; }
    }
    private float _dirY = default(float);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"dirY", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue(default(float))]
    public float dirY
    {
      get { return _dirY; }
      set { _dirY = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ScAllPlayerPosInfo")]
  public partial class ScAllPlayerPosInfo : global::ProtoBuf.IExtensible
  {
    public ScAllPlayerPosInfo() {}
    
    private readonly global::System.Collections.Generic.List<Monster.Protocol.PlayerPosInfo> _infos = new global::System.Collections.Generic.List<Monster.Protocol.PlayerPosInfo>();
    [global::ProtoBuf.ProtoMember(1, Name=@"infos", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Monster.Protocol.PlayerPosInfo> infos
    {
      get { return _infos; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CsPlayerStartMove")]
  public partial class CsPlayerStartMove : global::ProtoBuf.IExtensible
  {
    public CsPlayerStartMove() {}
    
    private long _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long id
    {
      get { return _id; }
      set { _id = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ScPlayerStartMove")]
  public partial class ScPlayerStartMove : global::ProtoBuf.IExtensible
  {
    public ScPlayerStartMove() {}
    
    private long _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long id
    {
      get { return _id; }
      set { _id = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CsPlayerEndMove")]
  public partial class CsPlayerEndMove : global::ProtoBuf.IExtensible
  {
    public CsPlayerEndMove() {}
    
    private long _id = default(long);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long id
    {
      get { return _id; }
      set { _id = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ScPlayerEndMove")]
  public partial class ScPlayerEndMove : global::ProtoBuf.IExtensible
  {
    public ScPlayerEndMove() {}
    
    private long _id = default(long);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long id
    {
      get { return _id; }
      set { _id = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CsPlayerUpdateMoveDir")]
  public partial class CsPlayerUpdateMoveDir : global::ProtoBuf.IExtensible
  {
    public CsPlayerUpdateMoveDir() {}
    
    private float _dirX = default(float);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"dirX", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue(default(float))]
    public float dirX
    {
      get { return _dirX; }
      set { _dirX = value; }
    }
    private float _dirY = default(float);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"dirY", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue(default(float))]
    public float dirY
    {
      get { return _dirY; }
      set { _dirY = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ScPlayerUpdateMoveDir")]
  public partial class ScPlayerUpdateMoveDir : global::ProtoBuf.IExtensible
  {
    public ScPlayerUpdateMoveDir() {}
    
    private float _dirX = default(float);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"dirX", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue(default(float))]
    public float dirX
    {
      get { return _dirX; }
      set { _dirX = value; }
    }
    private float _dirY = default(float);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"dirY", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue(default(float))]
    public float dirY
    {
      get { return _dirY; }
      set { _dirY = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CsPlayerEndMovePos")]
  public partial class CsPlayerEndMovePos : global::ProtoBuf.IExtensible
  {
    public CsPlayerEndMovePos() {}
    
    private float _posX = default(float);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"posX", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue(default(float))]
    public float posX
    {
      get { return _posX; }
      set { _posX = value; }
    }
    private float _posY = default(float);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"posY", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue(default(float))]
    public float posY
    {
      get { return _posY; }
      set { _posY = value; }
    }
    private float _dirX = default(float);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"dirX", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue(default(float))]
    public float dirX
    {
      get { return _dirX; }
      set { _dirX = value; }
    }
    private float _dirY = default(float);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"dirY", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue(default(float))]
    public float dirY
    {
      get { return _dirY; }
      set { _dirY = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CsPlayerUseSkill")]
  public partial class CsPlayerUseSkill : global::ProtoBuf.IExtensible
  {
    public CsPlayerUseSkill() {}
    
    private int _skillId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"skillId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int skillId
    {
      get { return _skillId; }
      set { _skillId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ScPlayerUseSkill")]
  public partial class ScPlayerUseSkill : global::ProtoBuf.IExtensible
  {
    public ScPlayerUseSkill() {}
    
    private long _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long id
    {
      get { return _id; }
      set { _id = value; }
    }
    private int _skillId;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"skillId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int skillId
    {
      get { return _skillId; }
      set { _skillId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}