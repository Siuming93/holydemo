namespace csharp RedDragon.Protocol

struct CsPing
{
	
}

struct CsPong
{
	
}

struct CsLogin
{
	1:required i64 accountid;
}

struct ScLogin
{
	1:required i32 result;
	2:required i64 accountid;
}

struct CsAsyncTime
{
}

struct ScAsyncTime
{
	1:required i64 time;
}

struct CsEnterScene 
{
	1:required string name;
}

struct ScEnterScene
{
	1:required bool result;
}

struct PosInfo
{
	1:required i32 posX;
	2:required i32 posY;
	3:required i32 angle;
}

struct ScOtherRoleEnterScene
{
	1:required i64 id;
	2:required PosInfo posInfo;
}

struct CsLeaveScene
{
}

struct ScLeaveScene
{
	1:required i64 id;
}

struct PlayerPosInfo
{
	1:required i64 id;
	2:required PosInfo posInfo;
	3:required bool isMove;
}

struct ScAllPlayerPosInfo
{
	1:required list<PlayerPosInfo> infos;
}

struct CsPlayerStartMove
{
	1:required i64 time;
	2:required PosInfo posInfo;
	3:required i32 speed;
}

struct ScPlayerStartMove
{
	1:required i64 id;
	2:required i64 time;
	3:required PosInfo posInfo;
	4:required i32 speed;
}

struct CsPlayerEndMove
{
	1:required PosInfo posInfo;
}

struct ScPlayerEndMove
{
	1:required i64 id;
	2:required PosInfo posInfo;
}

struct CsPlayerUpdateMoveDir
{
	1:required PosInfo posInfo;
	2:required i64 time;
}

struct ScPlayerUpdateMoveDir
{
	1:required i64 id;
	2:required PosInfo posInfo;
	3:required i64 time;
}

struct CsPlayerEndMovePos
{
	1:required PosInfo posInfo;
}

struct ScPlayerEndMovePos
{
	1:required i64 id;
	2:required PosInfo posInfo;
}

struct CsPlayerUseSkill
{
	1:required i32 skillId;
}

struct ScPlayerUseSkill
{
	1:required i64 id;
	2:required i32 skillId;
}

struct ScPlayerCheckFailured
{
	1:required PosInfo posInfo;
}