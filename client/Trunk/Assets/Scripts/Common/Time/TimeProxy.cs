
using Monster.BaseSystem;
using Monster.Protocol;

public class TimeProxy : BaseProxy
{
    public new const string NAME = "TimeProxy";


    public TimeProxy(): base(NAME)
    {
        RegisterMessageHandler(MsgIDDefine.ScAsyncTime, OnAsyncServerTime);
        netManager.SendMessage(new CsAsyncTime() { id = (int)RoleProperty.ID });   
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
        GameConfig.lastServerTime = msg.time;
    }

    private void AsyncTime()
    {
        if (UnityEngine.Time.time - GameConfig.lastLocaleTime >= 60)
        {
            netManager.SendMessage(new CsAsyncTime(){id = (int)RoleProperty.ID});
            GameConfig.lastLocaleTime = UnityEngine.Time.time; 
        }
    }
}

