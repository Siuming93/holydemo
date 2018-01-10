using HotFix.GameFrame.NetWork;
using RedDragon.Protocol;
using Thrift.Protocol;
using UnityEngine;

public class LoginProxy : BaseProxy
{
    public new const string NAME = "LoginProxy";

    public LoginProxy()
        : base(NAME)
    {
        NetBridge.Instance.RegisterMessageHandler(MsgIDDefine.ScLogin, OnGetLoginResponse);
    }

    public void OnGetLoginResponse(TBase msg)
    {
        ScLogin loginMsg = msg as ScLogin;
        Debug.Log(loginMsg.Accountid);
        if (loginMsg.Result == 1)
            SendNotification(NotificationConst.LOGIN_SUCCESS, loginMsg.Accountid);
    }
}
