
using Monster.BaseSystem;

public class TimeProxy
{
    public new const string NAME = "TimeProxy";


    public TimeProxy()
    {
        //RegisterMessageHandler(MsgIDDefine.ScAsyncTime, OnAsyncServerTime);
        //RegisterMessageHandler(MsgIDDefine.ScPong, OnScPong);
        //netManager.SendMessage(new CsAsyncTime() { });   
        UpdateProxy.Instance.UpdateEvent += AsyncTime;
    }

    public void OnRemove()
    {
        UpdateProxy.Instance.UpdateEvent -= AsyncTime;
    }

    private void OnScPong(object data)
    {
        GameConfig.Rtt = (UnityEngine.Time.time - _lastPingSendTime) * 1000;
    }

    private void OnAsyncServerTime(object data)
    {
        //ScAsyncTime msg = data as ScAsyncTime;
        //GameConfig.lastServerTime = msg.Time;
        GameConfig.lastLocaleTime = UnityEngine.Time.time; 
    }
    private float _lastPingSendTime;
    private float _lastAsyncSendTime;

    private void AsyncTime()
    {
        if (UnityEngine.Time.time - _lastAsyncSendTime >= 60)
        {
            //netManager.SendMessage(new CsAsyncTime(){});
            _lastAsyncSendTime = UnityEngine.Time.time;
        }

        if (UnityEngine.Time.time - _lastPingSendTime >= 1)
        {
            //netManager.SendMessage(new CsPing());
            _lastPingSendTime = UnityEngine.Time.time; 
        }
    }
}

