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
		msgid2msgname[10001] = "CsLogin";
		msgname2msgid["CsLogin"] = 10001;
		msgid2msgname[10002] = "ScLogin";
		msgname2msgid["ScLogin"] = 10002;
		msgid2msgname[10201] = "CsHelloWorld";
		msgname2msgid["CsHelloWorld"] = 10201;
		msgid2msgname[10202] = "ScHelloWorld";
		msgname2msgid["ScHelloWorld"] = 10202;
		msgid2msgname[10301] = "CsTalk";
		msgname2msgid["CsTalk"] = 10301;
		msgid2msgname[10302] = "ScTalk";
		msgname2msgid["ScTalk"] = 10302;
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
