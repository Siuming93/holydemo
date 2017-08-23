using UnityEngine;
using System.Collections;
using Monster.Net;
using Monster.Protocol;
using System;
using BaseSystem;
using Monster.BaseSystem.SceneManager;

public class LoginController : Singleton<LoginController>
{
    private string _lastIp;

    public LoginController()
    {
        RegisterProto();
    }

    private void RegisterProto()
    {
        NetManager.Instance.RegisterMessageHandler(MsgIDDefineDic.ScLogin, OnGetLoginResponse);
    }

    public void Login(string id, string ipAdress)
    {
        NetManager.Instance.Close();
        NetManager.Instance.TryConnect(ipAdress);
        _lastIp = ipAdress;
        NetManager.Instance.SendMessage(new CsLogin() { account = id });
    }

    public void SendMessageTest(string id)
    {
        NetManager.Instance.SendMessage(new CsLogin() {account = id});
    }

    public void OnGetLoginResponse(object msg)
    {
        PlayerPrefs.SetString("last_ip", _lastIp);
        PlayerPrefs.Save();
        UpdateProxy.Instance.StartCoroutine(SceneSwitcher.Instance.LoadScene(CitySceneManager.SCENE_NAME));
    }
}
