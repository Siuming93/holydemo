using System;
using System.Collections.Generic;
using System.Text;
using Monster.Protocol;
public class MsgIDDefine
{
	public const int CsLogin = 10001; 
	public const int ScLogin = 10002; 
	public const int CsHelloWorld = 10201; 
	public const int ScHelloWorld = 10202; 
	public const int CsTalk = 10301; 
	public const int ScTalk = 10302; 
	public const int CsEnterScene = 10401; 
	public const int ScEnterScene = 10402; 
	public const int CsPlayerMove = 10403; 
	public const int ScAllPlayerPosInfo = 10404; //所有玩家位置状态;定期发
	public const int CsPlayerIsMove = 10405; 
	public const int ScPlayerIsMove = 10406; 
	public const int CsPlayerUseSkill = 10407; 
	public const int ScPlayerUseSkill = 10408; 
}
