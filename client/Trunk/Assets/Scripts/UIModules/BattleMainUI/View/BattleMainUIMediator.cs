using UnityEngine;
using System.Collections;

public class BattleMainUIMediator : AbstractMediator
{
    public new const string NAME = "BattleMainUIMediator";

    private VirtualStick _stick;
    private GameObject _view;
    public BattleMainUIMediator(GameObject view)
        : base(NAME)
    {
        this._view = view;
        UpdateProxy.Instance.FixedUpdateEvent += OnFixedUpdate;
        this._stick = VirtualStick.Instance; ;
    }

    public override void OnRemove()
    {
        UpdateProxy.Instance.FixedUpdateEvent -= OnFixedUpdate;
    }

    private void OnFixedUpdate()
    {
        if (!_stick.isPressed)
            return;

        bool moveX = !Mathf.Approximately(_stick.Coordinates.x, 0);
        bool moveY = !Mathf.Approximately(_stick.Coordinates.y, 0);

        if (!moveX && !moveY)
            return;

        int directX = _stick.Coordinates.x > 0 ? 1 : -1;
        int directY = _stick.Coordinates.y > 0 ? 1 : -1;

        //todo 给后端发送消息;

        Vector2 delta = _stick.Coordinates * PlayerProperty.RunSpeed * Time.fixedDeltaTime;
        Debug.Log("Mode delta: " + delta);
        SendNotification(NotificationConst.PLAYER_MOVE_DElTA, delta);

    }
}
