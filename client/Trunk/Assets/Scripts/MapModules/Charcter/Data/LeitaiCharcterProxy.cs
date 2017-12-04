using System.Collections.Generic;
using Monster.Protocol;
using UnityEngine;
using UnityEngineInternal;

public class LeitaiCharcterProxy : BaseProxy
{
    public new const string NAME = "LeitaiCharcterProxy";

    public List<WorldRoleInfoVO> otherPlayerList 
    {
        get { return new List<WorldRoleInfoVO>(_roleList);}
    }
    private List<WorldRoleInfoVO> _roleList = new List<WorldRoleInfoVO>();
    private Dictionary<long, WorldRoleInfoVO> _roleMap = new Dictionary<long, WorldRoleInfoVO>();

    public LeitaiCharcterProxy()
        : base(NAME)
    {
        RegisterMessageHandler(MsgIDDefine.ScAllPlayerPosInfo, OnSceneRoleInfoList);
        RegisterMessageHandler(MsgIDDefine.ScPlayerEndMove, OnPlayerEndMove);
        RegisterMessageHandler(MsgIDDefine.ScPlayerUpdateMoveDir, OnPlayerUpdateMoveDir);
        RegisterMessageHandler(MsgIDDefine.ScPlayerStartMove, OnPlayerStartMove);
        RegisterMessageHandler(MsgIDDefine.ScOtherRoleEnterScene, OnOtherRoleEnterScene);
    }

    public override void OnRemove()
    {
        UnRegisterMessageHandler(MsgIDDefine.ScAllPlayerPosInfo);
        UnRegisterMessageHandler(MsgIDDefine.ScPlayerEndMove);
        UnRegisterMessageHandler(MsgIDDefine.ScPlayerUpdateMoveDir);
        UnRegisterMessageHandler(MsgIDDefine.ScPlayerStartMove);
        UnRegisterMessageHandler(MsgIDDefine.ScEnterScene);
    }

    private void OnOtherRoleEnterScene(object data)
    {
        ScOtherRoleEnterScene msg = data as ScOtherRoleEnterScene;
        long roleId = msg.id;
        var vo = new WorldRoleInfoVO();
        vo.Update(msg.posInfo);
        _roleMap.Add(roleId, vo);
        _roleList.Add(vo);

        SendNotification(NotificationConst.OTHER_ROLE_ENTER_SCENE, vo);
    }


    private void OnSceneRoleInfoList(object msg)
    {
        otherPlayerList.Clear();
        ScAllPlayerPosInfo allInfo = msg as ScAllPlayerPosInfo;
        foreach (var info in allInfo.infos)
        {
            var id = info.id;
            if (id == RoleProperty.ID)
            {
                RoleProperty.Postion = new Vector2(info.posInfo.posX, info.posInfo.posY);
            }
            else
            {
                WorldRoleInfoVO vo;
                if (!_roleMap.ContainsKey(id))
                {
                    vo = new WorldRoleInfoVO();
                    _roleMap.Add(id, vo);
                }
                vo = _roleMap[id];
                vo.Update(info);
                _roleList.Add(vo);
            }
        }

        SendNotification(NotificationConst.GET_SCENE_ROLE_INFO_LIST);
    }

    private void OnPlayerEndMove(object data)
    {
        var msg = data as ScPlayerEndMove;
        var role = GetRoleVO(msg.id);
        role.Update(msg.posInfo, false);
        SendNotification(NotificationConst.OTHER_ROLE_END_MOVE, role);
    }

    private void OnPlayerStartMove(object data)
    {
        ScPlayerStartMove msg = data as ScPlayerStartMove;
        var role = GetRoleVO(msg.id);
        role.Update(msg.posInfo, true, msg.time);
        SendNotification(NotificationConst.OTHER_ROLE_START_MOVE, role);
    }

    private void OnPlayerUpdateMoveDir(object data)
    {
        var msg = data as ScPlayerUpdateMoveDir;
        var role = GetRoleVO(msg.id);
        role.Update(msg.posInfo);
        SendNotification(NotificationConst.OTHER_ROLE_UPDATE_MOVE_DIR, role);
    }

    #region self funcs 

    private WorldRoleInfoVO GetRoleVO(long roleId)
    {
        if (!_roleMap.ContainsKey(roleId))
        {
            var vo = new WorldRoleInfoVO();
            _roleMap.Add(roleId, vo);
            _roleList.Add(vo);
        }
        return _roleMap[roleId];
    }
    #endregion
}
