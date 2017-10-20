﻿using System.Collections;
using Monster.BaseSystem;
using Monster.Net;
using Monster.Protocol;
using UnityEngine;
using Monster.BaseSystem.SceneManager;

public class LoginMediator : AbstractMediator
{
    public new const string NAME = "LoginMediator";
    public const string PANEL_PATH = "Prefab/UI/Login/LoginView";

    private LoginUI _skin;
    private GameObject _view;
    private LoginProxy _proxy;

    public LoginMediator(): base(NAME)
    {
        _proxy = ApplicationFacade.Instance.RetrieveProxy(LoginProxy.NAME) as LoginProxy;
        RegisterNotificationHandler(NotificationConst.LOGIN_SUCCESS, OnLoginSuccess);
        _view = ResourcesFacade.Instance.LoadPrefab(PANEL_PATH);
        UIManager.Intance.AddChild(_view.transform);
        _skin = _view.GetComponent<LoginUI>();

        _skin.loginButton.onClick.AddListener(OnLoginBtnClick);
        if (PlayerPrefs.HasKey("last_ip"))
        {
            _skin.ipAddressInputField.text = PlayerPrefs.GetString("last_ip");
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
        PlayerPrefs.SetString("last_ip", _skin.ipAddressInputField.text);
        PlayerPrefs.Save();
        UpdateProxy.Instance.StartCoroutine(SceneSwitcher.Instance.LoadScene(LeiTaiSceneManager.SCENE_NAME));
    }
    #endregion

    #region component callback
    private void OnLoginBtnClick()
    {
        var ipAdress = _skin.ipAddressInputField.text;
        var id = _skin.acountInputField.text;
        NetManager.Instance.Close();
        NetManager.Instance.TryConnect(ipAdress);
        NetManager.Instance.SendMessage(new CsLogin() { account = id });
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