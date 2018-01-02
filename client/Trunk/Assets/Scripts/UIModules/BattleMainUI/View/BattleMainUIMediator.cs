using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;
using Monster.BaseSystem;
using Monster.BaseSystem.ResourceManager;
using Monster.Net;
using RedDragon.Protocol;
using UnityEngine.UI;

public class BattleMainUIMediator : AbstractMediator
{
    public new const string NAME = "BattleMainUIMediator";
    public const string PANEL_PATH = "Prefab/UI/BattleMainUI/BattleMainUIPanel";

    private SkillProxy _proxy;
    private VirtualStick _stick;
    private BattleMainUISkin _skin;
    private GameObject origin;

    private Dictionary<int, Button> _skillBtnMap;
    private Dictionary<int, Image> _skillImageMap;
    private Dictionary<Image, Tweener> _skillTweenerMap; 
    public BattleMainUIMediator()
        : base(NAME)
    {
        this._proxy = ApplicationFacade.Instance.RetrieveProxy(SkillProxy.NAME) as SkillProxy;
        ResourcesFacade.Instance.LoadAsync<GameObject>(PANEL_PATH, OnPanelLoadComplete);
        RegisterNotificationHandler(NotificationConst.USE_SKILL, OnSkillUse);
    }

    private void OnPanelLoadComplete(IAsyncResourceRequest resourcerequest)
    {
        origin = (resourcerequest as AsyncResourceRequest).asset as GameObject;

        Debug.Log(origin);

        var view = GameObject.Instantiate(origin);
        this._skin = view.GetComponent<BattleMainUISkin>();

        this._skin.attackBtn.onClick.AddListener(OnAttackBtnClick);
        this._skin.skillBtn1.onClick.AddListener(OnSkillBtn1Click);
        this._skin.skillBtn2.onClick.AddListener(OnSkillBtn1Click);
        this._skin.skillBtn2.onClick.RemoveListener(OnSkillBtn1Click);
        this._skin.skillBtn2.onClick.AddListener(OnSkillBtn2Click);
        this._skin.skillBtn3.onClick.AddListener(OnSkillBtn3Click);

        this._stick = _skin.stick;
        this._stick.OnStickMovementEnd += OnStickMovementEnd;
        this._stick.OnStickMovementStart += OnStickMovementStart;
        this._stick.OnJoystickMovement += OnStickMovement;

        this._skillBtnMap = new Dictionary<int, Button>();
        this._skillImageMap = new Dictionary<int, Image>();
        this._skillTweenerMap = new Dictionary<Image, Tweener>();

        this._skillBtnMap.Add(_proxy.attackVO.meta.id, _skin.attackBtn);
        this._skillBtnMap.Add(_proxy.skill1VO.meta.id, _skin.skillBtn1);
        this._skillBtnMap.Add(_proxy.skill2VO.meta.id, _skin.skillBtn2);
        this._skillBtnMap.Add(_proxy.skill3VO.meta.id, _skin.skillBtn3);

        this._skillImageMap.Add(_proxy.skill1VO.meta.id, _skin.skillCdImage1);
        this._skillImageMap.Add(_proxy.skill2VO.meta.id, _skin.skillCdImage2);
        this._skillImageMap.Add(_proxy.skill3VO.meta.id, _skin.skillCdImage3);

        RefreshSkillArea();
        UIManager.Intance.AddChild(_skin.transform);

        UpdateProxy.Instance.UpdateEvent += UpdateTime;
    }

    public override void OnRemove()
    {
        if (_skin == null)
            return;
        ResourcesFacade.Instance.UnLoadAsset(origin);
        UIManager.Intance.RemoveChild(_skin.transform);
        this._skin.attackBtn.onClick.RemoveListener(OnAttackBtnClick);
        this._skin.skillBtn1.onClick.RemoveListener(OnSkillBtn1Click);
        this._skin.skillBtn2.onClick.RemoveListener(OnSkillBtn2Click);
        this._skin.skillBtn3.onClick.RemoveListener(OnSkillBtn3Click);

        this._stick.OnStickMovementEnd -= OnStickMovementEnd;
        this._stick.OnStickMovementStart -= OnStickMovementStart;
        this._stick.OnJoystickMovement -= OnStickMovement;

        UpdateProxy.Instance.UpdateEvent -= UpdateTime;

        GameObject.Destroy(_skin.gameObject);
    }

    #region component callBack
    private void OnAttackBtnClick()
    {
        _proxy.UseSkill(_proxy.attackVO);
        CommonUtil.ReStart();
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
        NetManager.Instance.SendMessage(new CsAsyncTime() { });
        _proxy.UseSkill(_proxy.skill3VO);
    }

    private void OnStickMovementStart(VirtualStick stick, Vector2 arg2)
    {
        SendNotification(NotificationConst.SELF_START_MOVE);
    }

    private void OnStickMovementEnd(VirtualStick obj)
    {
        var pos = GetPlayerPosInfo();
        NetManager.Instance.SendMessage(new CsPlayerEndMove() { PosInfo = new PosInfo((int)pos.posX, (int)pos.posY, (int)pos.angle),Time = GameConfig.Time});
        _hasSendStartMove = false;
        SendNotification(NotificationConst.SELF_END_MOVE);
    }

    private int _lastAngle;
    bool _hasSendStartMove = false;

    private void OnStickMovement(VirtualStick stick, Vector2 dir)
    {
        int angle = GetAngleMod(stick.Angle);
        var pos = GetPlayerPosInfo();
        if (!_hasSendStartMove)
        {
            NetManager.Instance.SendMessage(new CsPlayerStartMove()
            {
                Time = GameConfig.Time,
                PosInfo = new PosInfo() { PosX = pos.posX, PosY = pos.posY, Angle = (int)angle },
                Speed = (int)RoleProperty.RunSpeed,
            });
            _hasSendStartMove = true;
            _lastAngle = angle;
            SendNotification(NotificationConst.SELF_UPDATE_MOVE_DIR, angle);
        }

        if (_lastAngle == angle )
            return;

        NetManager.Instance.SendMessage(new CsPlayerUpdateMoveDir(new PosInfo((int)pos.posX, (int)pos.posY, angle), GameConfig.Time));
        _lastAngle = angle;

        SendNotification(NotificationConst.SELF_UPDATE_MOVE_DIR, angle);
    }

    private ModelPosVO GetPlayerPosInfo()
    {
        return (ApplicationFacade.Instance.RetrieveMediator(WorldPlayerBattleMediator.NAME) as WorldPlayerBattleMediator).playerMoelController.posInfo;
    }

    private Vector3 GetModelPos()
    {
        return (ApplicationFacade.Instance.RetrieveMediator(WorldPlayerBattleMediator.NAME) as WorldPlayerBattleMediator).playerMoelController.modelTransform.position;
    }

    #endregion

    #region noti funcs

    private void OnSkillUse(object obj)
    {
       RefreshSkillBtn(obj as SkillVO);
    }

    private void RefreshSkillArea()
    {
        RefreshSkillBtn(_proxy.attackVO);
        RefreshSkillBtn(_proxy.skill1VO);
        RefreshSkillBtn(_proxy.skill2VO);
        RefreshSkillBtn(_proxy.skill3VO);
    }

    private void RefreshSkillBtn(SkillVO vo)
    {
        float cd = vo.meta.cd;
        float useTime = (float)GameConfig.Time - vo.lastUseMiliSceond;
        bool isCD = useTime < vo.meta.cd;
        var button = _skillBtnMap[vo.meta.id];

        button.interactable = !isCD;

        Image image;
        if (_skillImageMap.TryGetValue(vo.meta.id, out image))
        {
            image.enabled = isCD;
            if (!isCD)
                return;
            Tweener tweener = null;
            if (!_skillTweenerMap.TryGetValue(image, out tweener))
            {
                tweener = DOTween.To(() => 1 - useTime / cd, (value) => { image.fillAmount = value; }, 0f, cd - useTime);
                tweener.onComplete += () => { button.interactable = true; _skillTweenerMap.Remove(image); };
                _skillTweenerMap[image] = tweener;
            }
            tweener.PlayForward();
        }
    }

    #endregion

    #region self funcs

    private double _lastTime;
    private void UpdateTime()
    {
        if (GameConfig.Time - _lastTime > 1)
        {
            _skin.timeText.text = TimeUtil.ToLongTimeString(GameConfig.Time) + string.Format("  Ping:{0:0}",GameConfig.Rtt);
            _lastTime = GameConfig.Time;
        }
    }

    private int GetAngleMod(int angle)
    {
        return (int)(angle - 22.5) / 45 * 45; ;
    }
    private double lastLogTime = 0;
    private float lastX;
    private float lastY;
    private int lastAngle;
    private float _lastUnityTime;
    private void LogInfo(int angle, bool log = false)
    {
        var deltTime = (GameConfig.Time - lastLogTime);
        var distance = RoleProperty.RunSpeed * deltTime;
        var dx = distance * Mathf.Cos(lastAngle / 180f * Mathf.PI);
        var dy = distance * Mathf.Sin(lastAngle / 180f * Mathf.PI);
        var curX = lastX + (float)dx;
        var curY = lastY + (float)dy;


        var pos = GetModelPos();


        var st = new StringBuilder();
        st.Append(" deltTime:" + deltTime);
        st.Append(" dx:" + dx);
        st.Append(" dy:" + dy);
        st.Append(" curX:" + curX);
        st.Append(" curY:" + curY);
        st.Append(" posX:" + pos.x);
        st.Append(" posY:" + pos.z);
        st.Append(" angle:" + lastAngle);

        Debug.Log(st);
        if (Mathf.Abs((float)curX - pos.x) > 1 || Mathf.Abs((float)curY - pos.z) > 1)
        {
            lastX = (float)curX;
            lastY = (float)curY;
            Debug.Log("---------------------------------time:" + (UnityEngine.Time.time - _lastUnityTime));
        }
        else
        {
            lastX = pos.x;
            lastY = pos.z;
        }
        lastAngle = angle;

        lastLogTime = GameConfig.Time;
        _lastUnityTime = UnityEngine.Time.time;
    }
    #endregion
}
