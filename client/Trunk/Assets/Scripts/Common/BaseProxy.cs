using System;
using Monster.Net;
using PureMVC.Patterns.Proxy;

public class BaseProxy : Proxy
{
    protected NetManager netManager;
    public BaseProxy(string proxyName) : base(proxyName, null)
    {
        netManager = NetManager.Instance;;
    }

    protected void RegisterMessageHandler(int msgNo, MessageHandler handler)
    {
        netManager.RegisterMessageHandler(msgNo, handler);
    }

    protected void UnRegisterMessageHandler(int msgNo)
    {
        netManager.UnRegistreMessageHandler(msgNo);
    }

    public void SendNotification(string notification, object data = null)
    {
        base.SendNotification(notification, data, null);
    }
}
