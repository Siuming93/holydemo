using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Monster.BaseSystem;
using Monster.BaseSystem.ResourceManager;
using UnityEngine.UI;

public class BattleMainUIMediator : AbstractMediator
{
    public new const string NAME = "BattleMainUIMediator";
    public const string PANEL_PATH = "Prefab/UI/BattleMainUI/BattleMainUIPanel";

    private SkillProxy _proxy;
    private VirtualStick _stick;
    private BattleMainUISkin _skin;
    private GameObject origin;

    private Dictionary<string, Button> _skillBtnMap;
    private Dictionary<string, Image> _skillImageMap;
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
        var view = GameObject.Instantiate(origin);
        this._skin = view.GetComponent<BattleMainUISkin>();

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

        this._skillBtnMap = new Dictionary<string, Button>();
        this._skillImageMap = new Dictionary<string, Image>();
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

        UpdateProxy.Instance.FixedUpdateEvent -= OnFixedUpdate;
        this._stick.OnStickMovementEnd -= OnStickMovementEnd;
        this._stick.OnStickMovementStart -= OnStickMovementStart;

        GameObject.Destroy(_skin.gameObject);
    }

    private void OnFixedUpdate()
    {
        if (!_stick.isPressed)
            return;

        if (_proxy.isUseSkill)
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
    private void OnStickMovementStart(VirtualStick arg1, Vector2 arg2)
    {
        SendNotification(NotificationConst.PLAYER_MOVE_START);
    }
    private void OnStickMovementEnd(VirtualStick obj)
    {
        SendNotification(NotificationConst.PLAYER_MOVE_END);
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
        float useTime = GameConfig.serverTime - vo.lastUseMiliSceond;
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
}
