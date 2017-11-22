using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Monster.BaseSystem;
using Monster.BaseSystem.ResourceManager;
using Monster.Net;
using Monster.Protocol;
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
        _proxy.UseSkill(_proxy.skill3VO);
    }
    private void OnStickMovementStart(VirtualStick arg1, Vector2 arg2)
    {
        NetManager.Instance.SendMessage(new CsPlayerStartMove() { id = PlayerProperty.ID });

    }
    private void OnStickMovementEnd(VirtualStick obj)
    {
        _lastDir = Vector2.zero;
        NetManager.Instance.SendMessage(new CsPlayerEndMove() { id = PlayerProperty.ID, });
    }

    private Vector2 _lastDir;
    private void OnStickMovement(VirtualStick arg1, Vector2 arg2)
    {
        SendUpdateMoveDirMsg(arg2);
        return;

        //暂时不做限制
        if (_lastDir == Vector2.zero)
        {
            _lastDir = arg2;
            SendUpdateMoveDirMsg(arg2);
            return;
        }

        var angle = Vector2.Angle(_lastDir, arg2);
        if (Mathf.Abs(angle) > 30)
        {
            _lastDir = arg2;
            SendUpdateMoveDirMsg(arg2);
        }
    }

    private void SendUpdateMoveDirMsg(Vector2 dir)
    {
        NetManager.Instance.SendMessage(new CsPlayerUpdateMoveDir() {dirX = dir.x, dirY = dir.y});
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
        float useTime = GameConfig.ServerTime - vo.lastUseMiliSceond;
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
