using System;
using System.Collections.Generic;
using System.Text;
using Monster.Protocol;
public class MsgIDDefine
{
	static Dictionary<int, string> msgid2msgname = new Dictionary<int, string>();
	static Dictionary<string, int> msgname2msgid = new Dictionary<string, int>();
    public static void Initialize()
	{
		msgid2msgname[10001] = "CMsgAccountLoginRequest";
		msgname2msgid["CMsgAccountLoginRequest"] = 10001;
		msgid2msgname[10002] = "CMsgAccountLoginResponse";
		msgname2msgid["CMsgAccountLoginResponse"] = 10002;
		msgid2msgname[10003] = "CMsgAccountRegistRequest";
		msgname2msgid["CMsgAccountRegistRequest"] = 10003;
		msgid2msgname[10004] = "CMsgAccountRegistResponse";
		msgname2msgid["CMsgAccountRegistResponse"] = 10004;
		msgid2msgname[10101] = "CMsgRoleListRequest";
		msgname2msgid["CMsgRoleListRequest"] = 10101;
		msgid2msgname[10102] = "CMsgRoleListResponse";
		msgname2msgid["CMsgRoleListResponse"] = 10102;
		msgid2msgname[10103] = "CMsgRoleCreateRequest";
		msgname2msgid["CMsgRoleCreateRequest"] = 10103;
		msgid2msgname[10104] = "CMsgRoleCreateResponse";
		msgname2msgid["CMsgRoleCreateResponse"] = 10104;
		msgid2msgname[10201] = "CsHelloWorld";
		msgname2msgid["CsHelloWorld"] = 10201;
		msgid2msgname[10202] = "ScHelloWorld";
		msgname2msgid["ScHelloWorld"] = 10202;
	}
    public static string GetMsgNameByID(int msgid)
	{
		string msgname = null;
		if (msgid2msgname.TryGetValue(msgid,out msgname))
		{
			return msgname;
		}
		return "";
	}
	public static int GetMsgIDByName(string msgname)
	{
		int msgid = 0;
		if (msgname2msgid.TryGetValue(msgname,out msgid))
		{
			return msgid;
		}
		return 0;
	}
}
