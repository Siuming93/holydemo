using Monster.Net;
using RedDragon.Protocol;

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
        if (loginMsg.Result == 1)
            SendNotification(NotificationConst.LOGIN_SUCCESS, loginMsg.Accountid);
    }
}
