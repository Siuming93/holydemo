using System.Collections.Generic;
using Monster.BaseSystem;
using Monster.Net;
using Monster.Protocol;
using UnityEngine;

public class WorldPlayerBattleMediator : AbstractMediator
{
    public new const string NAME = "WorldPlayerBattleMediator";
    public const string MODEL_PATH = "Prefab/Model/Charcter/Boy";

    public Vector3 playerPos { get { return _playerController.model.transform.localPosition; } }

    private PlayerMoelController _playerController;
    private CameraMovement _cameraMovement;

    private Dictionary<long, OtherPlayerMoelController> controllers;
    private Stack<OtherPlayerMoelController> controllerPool;

    private LeitaiCharcterProxy _proxy;
    public WorldPlayerBattleMediator()
        : base(NAME)
    {
        _playerController = new PlayerMoelController();
        PlayerProperty.ModelController = _playerController;
        _playerController.LoadModelPrefab(MODEL_PATH);

        _cameraMovement = new CameraMovement()
        {
            cameraTransform = GameObject.Find("PlayerCamera").transform,
            player = _playerController.model.transform,
            speed = 10f,
        };
        RegisterNotificationHandler(NotificationConst.ALL_OTHER_PLAYER_INFO_UPDATE, OnOtherPlayerPosUpdate);
        RegisterNotificationHandler(NotificationConst.PLAYER_START_MOVE, OnPlayerStartMove);
        RegisterNotificationHandler(NotificationConst.PLAYER_END_MOVE, OnPlayerEndMove);
        RegisterNotificationHandler(NotificationConst.PLAYER_UPDATE_MOVE_DIR, OnUpdatePlayerMoveDir);
        RegisterNotificationHandler(NotificationConst.USE_SKILL, OnUseSkill);
        controllers = new Dictionary<long, OtherPlayerMoelController>();
        controllerPool = new Stack<OtherPlayerMoelController>();

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

    private void OnOtherPlayerPosUpdate(object obj)
    {
        foreach (var controller in controllers.Values)
        {
            controller.model.gameObject.SetActive(false);
            controllerPool.Push(controller);
        }
        var list = _proxy.otherPlayerList;
        foreach (WorldPlayerInfoVO vo in list)
        {
            OtherPlayerMoelController controller;
            if (controllerPool.Count > 0)
            {
                controller = controllerPool.Pop();
            }
            else
            {
                controller = new OtherPlayerMoelController();
                controller.LoadModelPrefab(MODEL_PATH);
            }
            controllers.Add(vo.id, controller);
            controller.model.gameObject.SetActive(true);
            controller.Async(vo);
        }
    }
    private void OnPlayerStartMove(object obj)
    {
        var vo = _proxy.playerInfo;
        var controller = _playerController;

        controller.StartMove(vo.dir);
        controller.LookAt(vo.dir);
        controller.PlayMoveAnimation(true);
    }

    private void OnUpdatePlayerMoveDir(object obj)
    {
        var vo = _proxy.playerInfo;
        var dir = vo.dir;
        var controller = _playerController;
        controller.LookAt(dir);
        controller.UpdateMoveDir(dir);
    }

    private void OnPlayerEndMove(object obj)
    {
        var vo = _proxy.playerInfo;
        var controller = _playerController;
        controller.EndMove();
        NetManager.Instance.SendMessage(new CsPlayerEndMovePos()
        {
            posX = controller.pos.x,
            posY = controller.pos.y,
            dirX = controller.dir.x,
            dirY = controller.dir.y
        });
        controller.LookAt(vo.dir);
        controller.PlayMoveAnimation(false);
    }

    #endregion
}
