using System;
using System.Collections.Generic;
using Monster.Net;
using PureMVC.Patterns.Proxy;

public class BaseProxy : Proxy
{
    protected NetManager netManager;

    private List<int> _registerList = new List<int>(); 

    public BaseProxy(string proxyName) : base(proxyName, null)
    {
        netManager = NetManager.Instance;;
    }

    public override void OnRemove()
    {
        foreach (int msgNo in _registerList)
        {
            UnRegisterMessageHandler(msgNo);
        }
        _registerList.Clear();
    }

    protected void RegisterMessageHandler(int msgNo, MessageHandler handler)
    {
        netManager.RegisterMessageHandler(msgNo, handler);
        _registerList.Add(msgNo);
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
