using UnityEngine;

public class WorldPlayerBattleMediator : AbstractMediator
{
    public new const string NAME = "WorldPlayerBattleMediator";
    public const string MODEL_PATH = "Prefab/Model/Charcter/Boy";

    private PlayerMoelController _playerController;
    private CameraMovement _cameraMovement;
    public WorldPlayerBattleMediator(Transform cameraTransform): base(NAME)
    {
        _playerController = new PlayerMoelController();
        _playerController.LoadModelPrefab(MODEL_PATH);

        _cameraMovement = new CameraMovement()
        {
            cameraTransform = cameraTransform,
            player = _playerController.model.transform,
            speed = 10f,
        };
        RegisterNotificationHandler(NotificationConst.PLAYER_MOVE_DElTA, OnPlayerMove);
    }

    private void OnPlayerMove(object obj)
    {
        Vector2 delta = (Vector2) obj;
        _playerController.Move(delta);
    }
}
