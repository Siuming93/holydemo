using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PureMVC;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;


public class AbstractMediator : Mediator
{
    public virtual bool isActive
    {
        get { return true; }
    }

    private Dictionary<string, Action<object>> _notificationMap;
    private HashSet<string> _notIgnore;

    public AbstractMediator(string mediatorName) : base(mediatorName, null)
    {
        _notificationMap = new Dictionary<string, Action<object>>();
    }

    public override void HandleNotification(INotification notification)
    {
        Action<object> action;
        if (_notificationMap.TryGetValue(notification.Name, out action) &&
            (isActive || _notIgnore.Contains(notification.Name)))
        {
            action.Invoke(notification.Body);
        }
    }

    public override string[] ListNotificationInterests()
    {
        return _notificationMap.Keys.ToArray();
    }

    protected virtual void RegisterNotificationHandler(string name, Action<object>  action, bool ignoreWhenUnActive = false)
    {
        _notificationMap.Add(name, action);
        if (!ignoreWhenUnActive)
            _notIgnore.Add(name);
    }

    protected virtual void RemoveNotificationHandler(string name)
    {
        _notificationMap.Remove(name);
        if (_notIgnore.Contains(name))
            _notIgnore.Remove(name);
    }
}

