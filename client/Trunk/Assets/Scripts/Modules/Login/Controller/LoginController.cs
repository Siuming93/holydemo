using UnityEngine;
using System.Collections;
using Monster.Net;
using Monster.Protocol;
using System;
using BaseSystem;

public class LoginController : Singleton<LoginController>
{
    public LoginController()
    {
        
    }

    private void RegisterProto()
    {
        NetManager.Instance.RegisterMessageHandler(MsgIDDefineDic.CMSGACCOUNTLOGINRESPONSE, OnGetLoginResponse);
    }

    public void Login(string id, string ipAdress)
    {
        NetManager.Instance.Close();
        NetManager.Instance.TryConnect(ipAdress);

        PlayerPrefs.SetString("last_ip", ipAdress);
        PlayerPrefs.Save();
    }

    public void SendMessageTest(string id)
    {
        NetManager.Instance.SendMessage(new CMsgAccountLoginRequest() {account = id});
    }

    private void OnGetLoginResponse(object msg)
    {
        //SceneSwitcher.Instance.LoadScene(sceneName)
    }
}
