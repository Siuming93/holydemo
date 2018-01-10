//using System.Collections.Generic;
//using RedDragon.Protocol;
//using UnityEngine;
//using UnityEngineInternal;

//public class LeitaiCharcterProxy : BaseProxy
//{
//    public new const string NAME = "LeitaiCharcterProxy";

//    public List<WorldRoleInfoVO> otherPlayerList 
//    {
//        get { return new List<WorldRoleInfoVO>(_roleList);}
//    }
//    private List<WorldRoleInfoVO> _roleList = new List<WorldRoleInfoVO>();
//    private Dictionary<long, WorldRoleInfoVO> _roleMap = new Dictionary<long, WorldRoleInfoVO>();

//    public LeitaiCharcterProxy()
//        : base(NAME)
//    {
//        RegisterMessageHandler(MsgIDDefine.ScAllPlayerPosInfo, OnSceneRoleInfoList);
//        RegisterMessageHandler(MsgIDDefine.ScPlayerEndMove, OnPlayerEndMove);
//        RegisterMessageHandler(MsgIDDefine.ScPlayerUpdateMoveDir, OnPlayerUpdateMoveDir);
//        RegisterMessageHandler(MsgIDDefine.ScPlayerStartMove, OnPlayerStartMove);
//        RegisterMessageHandler(MsgIDDefine.ScOtherRoleEnterScene, OnOtherRoleEnterScene);
//        RegisterMessageHandler(MsgIDDefine.ScPlayerCheckFailured, OnMoveFailured);
//        selfPosInfo = new WorldRoleInfoVO();
//    }
//    public override void OnRemove()
//    {
//        UnRegisterMessageHandler(MsgIDDefine.ScAllPlayerPosInfo);
//        UnRegisterMessageHandler(MsgIDDefine.ScPlayerEndMove);
//        UnRegisterMessageHandler(MsgIDDefine.ScPlayerUpdateMoveDir);
//        UnRegisterMessageHandler(MsgIDDefine.ScPlayerStartMove);
//        UnRegisterMessageHandler(MsgIDDefine.ScEnterScene);
//    }

//    private void OnOtherRoleEnterScene(object data)
//    {
//        ScOtherRoleEnterScene msg = data as ScOtherRoleEnterScene;
//        long roleId = msg.Id;
//        WorldRoleInfoVO vo;
//        if (!_roleMap.TryGetValue(roleId, out vo))
//        {
//            vo = new WorldRoleInfoVO();
//            _roleMap.Add(roleId, vo);
//            _roleList.Add(vo);
//            vo.Update(msg.PosInfo);

//            SendNotification(NotificationConst.OTHER_ROLE_ENTER_SCENE, vo);
//        }
//        else
//        {
//            vo.Update(msg.PosInfo);
//        }
//    }


//    private void OnSceneRoleInfoList(object msg)
//    {
//        otherPlayerList.Clear();
//        ScAllPlayerPosInfo allInfo = msg as ScAllPlayerPosInfo;
//        foreach (var info in allInfo.Infos)
//        {
//            var id = info.Id;
//            if (id == RoleProperty.ID)
//            {
//                //RoleProperty.Postion = new Vector2((float)info.posInfo.posX, (float)info.posInfo.posY);
//            }
//            else
//            {
//                WorldRoleInfoVO vo;
//                if (!_roleMap.ContainsKey(id))
//                {
//                    vo = new WorldRoleInfoVO();
//                    _roleMap.Add(id, vo);
//                }
//                vo = _roleMap[id];
//                vo.Update(info);
//                _roleList.Add(vo);
//            }
//        }

//        SendNotification(NotificationConst.GET_SCENE_ROLE_INFO_LIST);
//    }

//    private void OnPlayerEndMove(object data)
//    {
//        var msg = data as ScPlayerEndMove;
//        var role = GetRoleVO(msg.Id);
//        role.Update(msg.PosInfo, false);
//        SendNotification(NotificationConst.OTHER_ROLE_END_MOVE, role);
//        Debug.Log(msg);
//    }

//    private void OnPlayerStartMove(object data)
//    {
//        ScPlayerStartMove msg = data as ScPlayerStartMove;
//        var role = GetRoleVO(msg.Id);
//        role.Update(msg.PosInfo, true, msg.Time);
//        SendNotification(NotificationConst.OTHER_ROLE_START_MOVE, role);
//        Debug.Log(msg);
//    }

//    private void OnPlayerUpdateMoveDir(object data)
//    {
//        var msg = data as ScPlayerUpdateMoveDir;
//        var role = GetRoleVO(msg.Id);
//        role.Update(msg.PosInfo);
//        SendNotification(NotificationConst.OTHER_ROLE_UPDATE_MOVE_DIR, role);
//        Debug.Log(msg);

//    }

//    private WorldRoleInfoVO selfPosInfo;
//    private void OnMoveFailured(object data)
//    {
//        ScPlayerCheckFailured msg = data as ScPlayerCheckFailured;
//        selfPosInfo.Update(msg.PosInfo);
//        SendNotification(NotificationConst.ASYNC_SELF_POS, selfPosInfo);
//    }


//    #region self funcs 

//    private WorldRoleInfoVO GetRoleVO(long roleId)
//    {
//        if (!_roleMap.ContainsKey(roleId))
//        {
//            var vo = new WorldRoleInfoVO();
//            _roleMap.Add(roleId, vo);
//            _roleList.Add(vo);
//        }
//        return _roleMap[roleId];
//    }
//    #endregion
//}
