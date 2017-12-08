
using Monster.BaseSystem;
using RedDragon.Protocol;

public class TimeProxy : BaseProxy
{
    public new const string NAME = "TimeProxy";


    public TimeProxy(): base(NAME)
    {
        RegisterMessageHandler(MsgIDDefine.ScAsyncTime, OnAsyncServerTime);
        netManager.SendMessage(new CsAsyncTime() { });   
        UpdateProxy.Instance.UpdateEvent += AsyncTime;
    }

    public override void OnRemove()
    {
        UpdateProxy.Instance.UpdateEvent -= AsyncTime;
        base.OnRemove();
    }


    private void OnAsyncServerTime(object data)
    {
        ScAsyncTime msg = data as ScAsyncTime;
        GameConfig.lastServerTime = msg.Time;
    }

    private void AsyncTime()
    {
        if (UnityEngine.Time.time - GameConfig.lastLocaleTime >= 60)
        {
            netManager.SendMessage(new CsAsyncTime(){});
            GameConfig.lastLocaleTime = UnityEngine.Time.time; 
        }
    }
}

