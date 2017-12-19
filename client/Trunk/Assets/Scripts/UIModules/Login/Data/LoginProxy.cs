using Monster.Net;
using RedDragon.Protocol;
using UnityEngine;

public class LoginProxy : BaseProxy
{
    public new const string NAME = "LoginProxy";

    public LoginProxy() : base(NAME)
    {
        NetManager.Instance.RegisterMessageHandler(MsgIDDefine.ScLogin, OnGetLoginResponse);
    }

    public void OnGetLoginResponse(object msg)
    {
        ScLogin loginMsg = msg as ScLogin;
        Debug.Log(loginMsg.Accountid);
        if (loginMsg.Result == 1)
            SendNotification(NotificationConst.LOGIN_SUCCESS, loginMsg.Accountid);
    }
}
