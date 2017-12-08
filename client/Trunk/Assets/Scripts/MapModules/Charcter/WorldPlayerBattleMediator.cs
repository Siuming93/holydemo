using System.Collections.Generic;
using Monster.BaseSystem;
using UnityEngine;

public class WorldPlayerBattleMediator : AbstractMediator
{
    public new const string NAME = "WorldPlayerBattleMediator";
    public const string MODEL_PATH = "Prefab/Model/Charcter/Boy";

    public PlayerMoelController playerMoelController { get { return _playerController; }}

    private PlayerMoelController _playerController;
    private CameraMovement _cameraMovement;

    private Dictionary<long, OtherRoleMoelController> controllerMaps;

    private LeitaiCharcterProxy _proxy;
    public WorldPlayerBattleMediator()
        : base(NAME)
    {
        _playerController = new PlayerMoelController();
        RoleProperty.ModelController = _playerController;
        _playerController.LoadModelPrefab(MODEL_PATH);
        _playerController.visible = true;

        _cameraMovement = new CameraMovement()
        {
            cameraTransform = GameObject.Find("PlayerCamera").transform,
            player = _playerController.modelTransform,
            speed = 10f,
        };
        RegisterNotificationHandler(NotificationConst.GET_SCENE_ROLE_INFO_LIST, OnGetSceneRoleList);
        RegisterNotificationHandler(NotificationConst.USE_SKILL, OnUseSkill);

        RegisterNotificationHandler(NotificationConst.SELF_START_MOVE, OnSelfStartMove);
        RegisterNotificationHandler(NotificationConst.SELF_END_MOVE, OnSelfEndMove);
        RegisterNotificationHandler(NotificationConst.SELF_UPDATE_MOVE_DIR, OnSelfUpdateMoveDir);

        RegisterNotificationHandler(NotificationConst.OTHER_ROLE_START_MOVE, OnOtherRoleStartMove);
        RegisterNotificationHandler(NotificationConst.OTHER_ROLE_END_MOVE, OnOtherRoleEndMove);
        RegisterNotificationHandler(NotificationConst.OTHER_ROLE_UPDATE_MOVE_DIR, OnOtherRoleUpdateMoveDir);
        RegisterNotificationHandler(NotificationConst.OTHER_ROLE_ENTER_SCENE, OnOtherRoleEnterScene);
        controllerMaps = new Dictionary<long, OtherRoleMoelController>();

        _proxy = ApplicationFacade.Instance.RetrieveProxy(LeitaiCharcterProxy.NAME) as LeitaiCharcterProxy; ;
    }

    public override void OnRemove()
    {
        _cameraMovement.Dispose();
        base.OnRemove();
    }

    #region noti handler
    private void OnUseSkill(object obj)
    {
        SkillVO vo = obj as SkillVO;
        _playerController.PlayMoveAnimation(false);
        _playerController.PlaySkillAnimation(vo.meta.animationMeta);
    }

    private void OnSelfUpdateMoveDir(object obj)
    {
        _playerController.StartMove((float)obj);
    }

    private void OnSelfEndMove(object obj)
    {
        _playerController.EndMove();
    }

    private void OnSelfStartMove(object obj)
    {
        _playerController.UpdateMoveDir((float)obj);
    }

    private void OnOtherRoleStartMove(object obj)
    {
        var roleVO = obj as WorldRoleInfoVO;
        OtherRoleMoelController controller;
        if (!controllerMaps.TryGetValue(roleVO.id, out controller))
        {
            Debug.LogError("OnOtherRoleStartMove No Controller");
            return;
        }
        controller.StartMove((float)roleVO.posInfo.angle);
    }

    private void OnOtherRoleUpdateMoveDir(object obj)
    {
        var roleVO = obj as WorldRoleInfoVO;
        OtherRoleMoelController controller;
        if (!controllerMaps.TryGetValue(roleVO.id, out controller))
        {
            Debug.LogError("OnOtherRoleUpdateMoveDir No Controller");
            return;
        }
        controller.UpdateMoveDir((float)roleVO.posInfo.angle);
    }

    private void OnOtherRoleEndMove(object obj)
    {
        var roleVO = obj as WorldRoleInfoVO;
        OtherRoleMoelController controller;
        if (!controllerMaps.TryGetValue(roleVO.id, out controller))
        {
            Debug.LogError("OnOtherRoleEndMove No Controller");
            return;
        }
        controller.EndMove();
    }
    private void OnOtherRoleEnterScene(object obj)
    {
        AddRoleController(obj as WorldRoleInfoVO);
    }

    private void OnGetSceneRoleList(object obj)
    {
        var list = _proxy.otherPlayerList;
        foreach (WorldRoleInfoVO vo in list)
        {
            AddRoleController(vo);
        }
    }

    #endregion

    #region self funcs 

    private void AddRoleController(WorldRoleInfoVO vo)
    {
        OtherRoleMoelController controller = new OtherRoleMoelController();
        controllerMaps.Add(vo.id, controller);
        controller.LoadModelPrefab(MODEL_PATH);
        controller.visible = true;
        controller.Async(vo);
    }
    #endregion
}
