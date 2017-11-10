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

    public LoginController()
    {
        RegisterProto();
    }

    private void RegisterProto()
    {
    }

    public void Login(string id, string ipAdress)
    {
        NetManager.Instance.Close();
        NetManager.Instance.TryConnect(ipAdress);
        NetManager.Instance.SendMessage(new CsLogin() { account = id });
    }

    public void SendMessageTest(string id)
    {
        NetManager.Instance.SendMessage(new CsLogin() {account = id});
    }
}
