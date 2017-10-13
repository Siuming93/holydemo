using Monster.BaseSystem;
using UnityEngine;
using Monster.Net;
using Monster.Protocol;
using Monster.BaseSystem.SceneManager;

public class LoginController 
{
    private static LoginController _instance;
    public static LoginController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LoginController();
            }
            return _instance;
        }
    }
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
        UpdateProxy.Instance.StartCoroutine(SceneSwitcher.Instance.LoadScene(LeiTaiSceneManager.SCENE_NAME));
    }
}
