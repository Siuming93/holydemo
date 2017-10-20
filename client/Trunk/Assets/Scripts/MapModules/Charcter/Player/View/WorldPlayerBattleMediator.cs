using UnityEngine;

public class WorldPlayerBattleMediator : AbstractMediator
{
    public new const string NAME = "WorldPlayerBattleMediator";
    public const string MODEL_PATH = "Prefab/Model/Charcter/Boy";

    private PlayerMoelController _playerController;
    private CameraMovement _cameraMovement;
    public WorldPlayerBattleMediator(): base(NAME)
    {
        _playerController = new PlayerMoelController();
        _playerController.LoadModelPrefab(MODEL_PATH);

        _cameraMovement = new CameraMovement()
        {
            cameraTransform = GameObject.Find("PlayerCamera").transform,
            player = _playerController.model.transform,
            speed = 10f,
        };
        RegisterNotificationHandler(NotificationConst.PLAYER_MOVE, OnPlayerMove);
        RegisterNotificationHandler(NotificationConst.PLAYER_MOVE_END, OnPlayerMoveEnd);
        RegisterNotificationHandler(NotificationConst.PLAYER_MOVE_START, OnPlayerMoveStart);
        RegisterNotificationHandler(NotificationConst.USE_SKILL, OnUseSkill);
    }

    #region noti handler
    private void OnUseSkill(object obj)
    {
        SkillVO vo = obj as SkillVO;
        _playerController.PlayMoveAnimation(false);
        _playerController.PlaySkillAnimation(vo.meta.animationMeta);
    }
    private void OnPlayerMoveStart(object obj)
    {
        _playerController.PlayMoveAnimation(true);
    }
    private void OnPlayerMoveEnd(object obj)
    {
        _playerController.PlayMoveAnimation(false);
    }
    private void OnPlayerMove(object obj)
    {
        ModelMoveVO vo = (ModelMoveVO)obj;
        PlayerProperty.Postion = vo.endPos;
        PlayerProperty.Rotation = vo.dir;
        _playerController.MoveTo(vo.endPos);
        _playerController.LookAt(vo.dir);
    }
    #endregion
}
