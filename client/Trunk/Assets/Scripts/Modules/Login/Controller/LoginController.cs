using UnityEngine;
using System.Collections;
using Monster.Net;

public class LoginController : Singleton<LoginController>
{
    public LoginController()
    {
        
    }

    public void Login(string id, string ipAdress)
    {
        //CMsgAccountLoginRequest.DefaultInstance.ToBuilder().Account = id;
        //CMsgAccountLoginRequest.DefaultInstance.ToBuilder().Password = id;
        //var byteString = CMsgAccountLoginRequest.DefaultInstance.ToByteString();
        //Debug.Log(byteString);
        //var newRequest = CMsgAccountLoginRequest.ParseFrom(byteString);
        //Debug.Log(string.Format("id:{0} ip:{1}", newRequest.Account, newRequest.Password));
        NetManager.Instance.TryConnect(ipAdress, 8888);
        NetManager.Instance.StartRun();
    }

    public void SendMessageTest(string id)
    {
        //NetManager.instance.SendMessage(id);
    }
}
