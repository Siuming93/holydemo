using System;
using System.Collections.Generic;
using System.Text;
using Monster.Protocol;
public class MsgIDDef
{
	private Dictionary<int, Type> sc_msg_dic = new Dictionary<int, Type>();
	private static MsgIDDef instance;
	public static MsgIDDef Instance()
	{
		if (null == instance)
		{
			instance = new MsgIDDef();
		}
		return instance;
	}
	private MsgIDDef()
	{
		sc_msg_dic.Add(10001,typeof(CsLogin));
		sc_msg_dic.Add(10002,typeof(ScLogin));
		sc_msg_dic.Add(10201,typeof(CsHelloWorld));
		sc_msg_dic.Add(10202,typeof(ScHelloWorld));
		sc_msg_dic.Add(10301,typeof(CsTalk));
		sc_msg_dic.Add(10302,typeof(ScTalk));
	}
	public Type GetMsgType(int msgID)
	{
		Type msgType = null;
		sc_msg_dic.TryGetValue(msgID, out msgType);
		if (msgType==null)
		{
			return null;
		}
		return msgType;
	}
}
