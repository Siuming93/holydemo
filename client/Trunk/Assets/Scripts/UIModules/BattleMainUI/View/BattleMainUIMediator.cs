using UnityEngine;
using System.Collections;
using Monster.BaseSystem;

public class BattleMainUIMediator : AbstractMediator
{
    public new const string NAME = "BattleMainUIMediator";

    private SkillProxy _proxy;
    private VirtualStick _stick;
    private BattleMainUISkin  _skin;
    public BattleMainUIMediator(GameObject view)
        : base(NAME)
    {
        this._skin = view.GetComponent<BattleMainUISkin>();
        this._proxy = ApplicationFacade.Instance.RetrieveProxy(SkillProxy.NAME) as SkillProxy;

        UpdateProxy.Instance.FixedUpdateEvent += OnFixedUpdate;

        this._skin.attackBtn.onClick.AddListener(OnAttackBtnClick);
        this._skin.skillBtn1.onClick.AddListener(OnSkillBtn1Click);
        this._skin.skillBtn2.onClick.AddListener(OnSkillBtn1Click);
        this._skin.skillBtn2.onClick.RemoveListener(OnSkillBtn1Click);
        this._skin.skillBtn2.onClick.AddListener(OnSkillBtn2Click);
        this._skin.skillBtn3.onClick.AddListener(OnSkillBtn3Click);

        this._stick = _skin.stick;
        this._stick.OnStickMovementEnd += OnStickMovementEnd;
        this._stick.OnStickMovementStart += OnStickMovementStart;
    }


    private void OnStickMovementStart(VirtualStick arg1, Vector2 arg2)
    {
        SendNotification(NotificationConst.PLAYER_MOVE_START);
    }

    private void OnStickMovementEnd(VirtualStick obj)
    {
        SendNotification(NotificationConst.PLAYER_MOVE_END);
    }

    public override void OnRemove()
    {
        this._skin.attackBtn.onClick.RemoveListener(OnAttackBtnClick);
        this._skin.skillBtn1.onClick.RemoveListener(OnSkillBtn1Click);
        this._skin.skillBtn2.onClick.RemoveListener(OnSkillBtn2Click);
        this._skin.skillBtn3.onClick.RemoveListener(OnSkillBtn3Click);

        UpdateProxy.Instance.FixedUpdateEvent -= OnFixedUpdate;
        this._stick.OnStickMovementEnd -= OnStickMovementEnd;
        this._stick.OnStickMovementStart -= OnStickMovementStart;
    }

    private void OnFixedUpdate()
    {
        if (!_stick.isPressed)
            return;

        bool moveX = !Mathf.Approximately(_stick.Coordinates.x, 0);
        bool moveY = !Mathf.Approximately(_stick.Coordinates.y, 0);

        if (!moveX && !moveY)
            return;

        //todo 给后端发送消息;

        Vector2 delta = _stick.Coordinates * PlayerProperty.RunSpeed * Time.fixedDeltaTime;
        Vector2 endPos = delta + PlayerProperty.Postion;
        endPos.x = Mathf.Clamp(endPos.x, -12, 15.4f);
        endPos.y = Mathf.Clamp(endPos.y, -22, 5.0f);

        SendNotification(NotificationConst.PLAYER_MOVE, new ModelMoveVO() { dir = _stick.Coordinates, endPos = endPos, });
    }

    #region component callBack
    private void OnAttackBtnClick()
    {
        _proxy.UseSkill(_proxy.attackVO);
    }
    private void OnSkillBtn1Click()
    {
        _proxy.UseSkill(_proxy.skill1VO);
    }
    private void OnSkillBtn2Click()
    {
        _proxy.UseSkill(_proxy.skill2VO);
    }
    private void OnSkillBtn3Click()
    {
        _proxy.UseSkill(_proxy.skill3VO);
    }
    #endregion
}
