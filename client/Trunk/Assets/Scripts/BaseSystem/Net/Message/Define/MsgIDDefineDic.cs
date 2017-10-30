using System;
using System.Collections.Generic;
using System.Text;
using Monster.Protocol;
public class MsgIDDefineDic
{
	private Dictionary<int, Type> id2msgMap = new Dictionary<int, Type>();
	private Dictionary<Type, int> msg2idMap = new Dictionary<Type, int>();
	private static MsgIDDefineDic instance;
	public static MsgIDDefineDic Instance()
	{
		if (null == instance)
		{
			instance = new MsgIDDefineDic();
		}
		return instance;
	}
	private MsgIDDefineDic()
	{
		id2msgMap.Add(10001, typeof(CsLogin));
		msg2idMap.Add(typeof(CsLogin), 10001);
		id2msgMap.Add(10002, typeof(ScLogin));
		msg2idMap.Add(typeof(ScLogin), 10002);
		id2msgMap.Add(10201, typeof(CsHelloWorld));
		msg2idMap.Add(typeof(CsHelloWorld), 10201);
		id2msgMap.Add(10202, typeof(ScHelloWorld));
		msg2idMap.Add(typeof(ScHelloWorld), 10202);
		id2msgMap.Add(10301, typeof(CsTalk));
		msg2idMap.Add(typeof(CsTalk), 10301);
		id2msgMap.Add(10302, typeof(ScTalk));
		msg2idMap.Add(typeof(ScTalk), 10302);
		id2msgMap.Add(10401, typeof(CsEnterScene));
		msg2idMap.Add(typeof(CsEnterScene), 10401);
		id2msgMap.Add(10402, typeof(ScEnterScene));
		msg2idMap.Add(typeof(ScEnterScene), 10402);
		id2msgMap.Add(10403, typeof(CsPlayerMove));
		msg2idMap.Add(typeof(CsPlayerMove), 10403);
		id2msgMap.Add(10404, typeof(ScAllPlayerPosInfo));
		msg2idMap.Add(typeof(ScAllPlayerPosInfo), 10404);
		id2msgMap.Add(10405, typeof(CsPlayerStartMove));
		msg2idMap.Add(typeof(CsPlayerStartMove), 10405);
		id2msgMap.Add(10406, typeof(CsPlayerEndMove));
		msg2idMap.Add(typeof(CsPlayerEndMove), 10406);
		id2msgMap.Add(10407, typeof(ScPlayerEndMove));
		msg2idMap.Add(typeof(ScPlayerEndMove), 10407);
		id2msgMap.Add(10408, typeof(CsPlayerEndMovePos));
		msg2idMap.Add(typeof(CsPlayerEndMovePos), 10408);
		id2msgMap.Add(10409, typeof(CsPlayerUseSkill));
		msg2idMap.Add(typeof(CsPlayerUseSkill), 10409);
		id2msgMap.Add(10410, typeof(ScPlayerUseSkill));
		msg2idMap.Add(typeof(ScPlayerUseSkill), 10410);
		id2msgMap.Add(10411, typeof(CsPlayerUpdateMoveDir));
		msg2idMap.Add(typeof(CsPlayerUpdateMoveDir), 10411);
		id2msgMap.Add(10412, typeof(ScPlayerUpdateMoveDir));
		msg2idMap.Add(typeof(ScPlayerUpdateMoveDir), 10412);
		id2msgMap.Add(10413, typeof(ScPlayerStartMove));
		msg2idMap.Add(typeof(ScPlayerStartMove), 10413);
	}
	public Type GetMsgType(int msgID)
	{
		Type msgType = null;
		id2msgMap.TryGetValue(msgID, out msgType);
		if (msgType==null)
		{
			return null;
		}
		return msgType;
	}
	public int GetMsgID(Type type)
	{
		int msgID = 0;
		msg2idMap.TryGetValue(type, out msgID);
		return msgID;
	}
}
