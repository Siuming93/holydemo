
using Monster.BaseSystem;
using RedDragon.Protocol;

public class TimeProxy : BaseProxy
{
    public new const string NAME = "TimeProxy";


    public TimeProxy(): base(NAME)
    {
        RegisterMessageHandler(MsgIDDefine.ScAsyncTime, OnAsyncServerTime);
        RegisterMessageHandler(MsgIDDefine.ScPong, OnScPong);
        netManager.SendMessage(new CsAsyncTime() { });   
        UpdateProxy.Instance.UpdateEvent += AsyncTime;
    }

    public override void OnRemove()
    {
        UpdateProxy.Instance.UpdateEvent -= AsyncTime;
        base.OnRemove();
    }

    private void OnScPong(object data)
    {
        GameConfig.Rtt = (UnityEngine.Time.time - _lastPingSendTime) * 1000;
    }

    private void OnAsyncServerTime(object data)
    {
        ScAsyncTime msg = data as ScAsyncTime;
        GameConfig.lastServerTime = msg.Time;
    }
    private float _lastPingSendTime;

    private void AsyncTime()
    {
        if (UnityEngine.Time.time - GameConfig.lastLocaleTime >= 60)
        {
            netManager.SendMessage(new CsAsyncTime(){});
            GameConfig.lastLocaleTime = UnityEngine.Time.time; 
        }

        if (UnityEngine.Time.time - _lastPingSendTime >= 1)
        {
            netManager.SendMessage(new CsPing());
            _lastPingSendTime = UnityEngine.Time.time; 
        }
    }
}

