using System;
using System.Collections.Generic;
using System.Text;
using RedDragon.Protocol;
using Thrift.Protocol;
public class MsgIDDefineDic
{
	private Dictionary<int, Type> id2msgMap = new Dictionary<int, Type>();
	private Dictionary<Type, int> msg2idMap = new Dictionary<Type, int>();
	private static MsgIDDefineDic instance;
	public static MsgIDDefineDic Instance
	{
		get
		{
			if (null == instance)
				instance = new MsgIDDefineDic();
			return instance;
		}
	}
	private MsgIDDefineDic()
	{
		id2msgMap.Add(10001, typeof(CsLogin));
		msg2idMap.Add(typeof(CsLogin), 10001);
		id2msgMap.Add(10002, typeof(ScLogin));
		msg2idMap.Add(typeof(ScLogin), 10002);
		id2msgMap.Add(10003, typeof(CsAsyncTime));
		msg2idMap.Add(typeof(CsAsyncTime), 10003);
		id2msgMap.Add(10004, typeof(ScAsyncTime));
		msg2idMap.Add(typeof(ScAsyncTime), 10004);
		id2msgMap.Add(10401, typeof(CsEnterScene));
		msg2idMap.Add(typeof(CsEnterScene), 10401);
		id2msgMap.Add(10402, typeof(ScEnterScene));
		msg2idMap.Add(typeof(ScEnterScene), 10402);
		id2msgMap.Add(10404, typeof(ScAllPlayerPosInfo));
		msg2idMap.Add(typeof(ScAllPlayerPosInfo), 10404);
		id2msgMap.Add(10405, typeof(CsPlayerStartMove));
		msg2idMap.Add(typeof(CsPlayerStartMove), 10405);
		id2msgMap.Add(10406, typeof(ScPlayerStartMove));
		msg2idMap.Add(typeof(ScPlayerStartMove), 10406);
		id2msgMap.Add(10407, typeof(CsPlayerEndMove));
		msg2idMap.Add(typeof(CsPlayerEndMove), 10407);
		id2msgMap.Add(10408, typeof(ScPlayerEndMove));
		msg2idMap.Add(typeof(ScPlayerEndMove), 10408);
		id2msgMap.Add(10409, typeof(CsPlayerUseSkill));
		msg2idMap.Add(typeof(CsPlayerUseSkill), 10409);
		id2msgMap.Add(10410, typeof(ScPlayerUseSkill));
		msg2idMap.Add(typeof(ScPlayerUseSkill), 10410);
		id2msgMap.Add(10411, typeof(CsPlayerUpdateMoveDir));
		msg2idMap.Add(typeof(CsPlayerUpdateMoveDir), 10411);
		id2msgMap.Add(10412, typeof(ScPlayerUpdateMoveDir));
		msg2idMap.Add(typeof(ScPlayerUpdateMoveDir), 10412);
		id2msgMap.Add(10413, typeof(ScOtherRoleEnterScene));
		msg2idMap.Add(typeof(ScOtherRoleEnterScene), 10413);
		id2msgMap.Add(10490, typeof(CsLeaveScene));
		msg2idMap.Add(typeof(CsLeaveScene), 10490);
		id2msgMap.Add(10491, typeof(ScLeaveScene));
		msg2idMap.Add(typeof(ScLeaveScene), 10491);
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
	public TBase CreatMsg(int msgID)
	{
		TBase msg = null;
		switch (msgID)
		{
			case 10001:
				msg = new CsLogin();
				break;
			case 10002:
				msg = new ScLogin();
				break;
			case 10003:
				msg = new CsAsyncTime();
				break;
			case 10004:
				msg = new ScAsyncTime();
				break;
			case 10401:
				msg = new CsEnterScene();
				break;
			case 10402:
				msg = new ScEnterScene();
				break;
			case 10404:
				msg = new ScAllPlayerPosInfo();
				break;
			case 10405:
				msg = new CsPlayerStartMove();
				break;
			case 10406:
				msg = new ScPlayerStartMove();
				break;
			case 10407:
				msg = new CsPlayerEndMove();
				break;
			case 10408:
				msg = new ScPlayerEndMove();
				break;
			case 10409:
				msg = new CsPlayerUseSkill();
				break;
			case 10410:
				msg = new ScPlayerUseSkill();
				break;
			case 10411:
				msg = new CsPlayerUpdateMoveDir();
				break;
			case 10412:
				msg = new ScPlayerUpdateMoveDir();
				break;
			case 10413:
				msg = new ScOtherRoleEnterScene();
				break;
			case 10490:
				msg = new CsLeaveScene();
				break;
			case 10491:
				msg = new ScLeaveScene();
				break;
		}
		return msg;
	}
}
