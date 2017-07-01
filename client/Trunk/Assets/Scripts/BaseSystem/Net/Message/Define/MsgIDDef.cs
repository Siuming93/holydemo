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
		sc_msg_dic.Add(10001,typeof(CMsgAccountLoginRequest));
		sc_msg_dic.Add(10002,typeof(CMsgAccountLoginResponse));
		sc_msg_dic.Add(10003,typeof(CMsgAccountRegistRequest));
		sc_msg_dic.Add(10004,typeof(CMsgAccountRegistResponse));
		sc_msg_dic.Add(10101,typeof(CMsgRoleListRequest));
		sc_msg_dic.Add(10102,typeof(CMsgRoleListResponse));
		sc_msg_dic.Add(10103,typeof(CMsgRoleCreateRequest));
		sc_msg_dic.Add(10104,typeof(CMsgRoleCreateResponse));
		sc_msg_dic.Add(10201,typeof(CsHelloWorld));
		sc_msg_dic.Add(10202,typeof(ScHelloWorld));
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
