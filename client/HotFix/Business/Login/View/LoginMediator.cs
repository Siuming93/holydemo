using System.Collections;
using HotFix.GameFrame.SceneController;
using Monster.BaseSystem;
using Monster.Net;
using UnityEngine;

public class LoginMediator : AbstractMediator
{
    public new const string NAME = "LoginMediator";
    public const string PANEL_PATH = "Prefab/UI/Login/LoginView";

    private LoginUI _skin;
    private GameObject _view;

    public LoginMediator(): base(NAME)
    {
        RegisterNotificationHandler(NotificationConst.LOGIN_SUCCESS, OnLoginSuccess);
        _view = ResourcesFacade.Instance.LoadPrefab(PANEL_PATH);
        UIManager.Intance.AddChild(_view.transform);
        _skin = _view.GetComponent<LoginUI>();

        _skin.loginButton.onClick.AddListener(OnLoginBtnClick);
        if (PlayerPrefs.HasKey("last_ip"))
        {
            _skin.ipAddressInputField.text = PlayerPrefs.GetString("last_ip");
        }
        if (PlayerPrefs.HasKey("last_id"))
        {
            _skin.acountInputField.text = PlayerPrefs.GetString("last_id");
        }
        _skin.StartCoroutine(ChcekState());
    }

    public override void OnRemove()
    {
        UIManager.Intance.RemoveChild(_view.transform);
        ResourcesFacade.Instance.UnLoadAsset(_view);
        base.OnRemove();
    }

    #region noti handler
    private void OnLoginSuccess(object obj)
    {
        Debug.Log("LoginSuccess");
        PlayerPrefs.SetString("last_id", _skin.acountInputField.text);
        PlayerPrefs.SetString("last_ip", _skin.ipAddressInputField.text);
        PlayerPrefs.Save();
        RoleProperty.ID = (long)obj;
        SceneSwitcher.Instance.LoadScene(LeiTaiSceneManager.SCENE_NAME);
    }
    #endregion

    #region component callback
    private void OnLoginBtnClick()
    {
        ////todo test
        //UpdateProxy.Instance.StartCoroutine(SceneSwitcher.Instance.LoadScene(LeiTaiSceneManager.SCENE_NAME));
        //return;
        var ipAdress = _skin.ipAddressInputField.text;
        var id = _skin.acountInputField.text;
        NetManager.Instance.Close();
        NetManager.Instance.TryConnect(ipAdress);
        //NetManager.Instance.SendMessage(new CsLogin() { Accountid = long.Parse(id) });
    }
    #endregion
    private IEnumerator ChcekState()
    {
        WaitForSeconds wait = new WaitForSeconds(1);
        int count = 0;
        while (true)
        {
            switch (NetManager.Instance.connectState)
            {
                case ConnectState.TryConnecting:
                    string content = "正在连接";
                    for (int i = 0; i < count; i++)
                    {
                        content += ".";
                    }
                    _skin.stateText.text = content;
                    count++;
                    count %= 5;
                    break;
                case ConnectState.NonConnect:
                    _skin.stateText.text = "未连接";
                    break;
                case ConnectState.Connected:
                    _skin.stateText.text = "已连接";
                    break;
                case ConnectState.ConnectingOutTime:
                    _skin.stateText.text = "连接超时!!";
                    break;
                case ConnectState.ConnectedFailed:
                    _skin.stateText.text = "连接失败!!";
                    break;
                case ConnectState.ConnectedBreak:
                    _skin.stateText.text = "链接已经断开!!";
                    break;
            }
            yield return wait;
        }
    }
}
