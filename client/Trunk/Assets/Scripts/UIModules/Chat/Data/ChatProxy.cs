using System.Collections.Generic;

public class ChatProxy : BaseProxy
{
    public new const string NAME = "ChatProxy";

    private List<ChatVO> mList;
    public ChatProxy() : base(NAME)
    {
        mList = new List<ChatVO>();
    }

    public override void OnRemove()
    {
        base.OnRemove();
    }

    private void OnScTalkMessage(object msg)
    {
        SendNotification(NotificationConst.ON_GET_TALK_MSG);
    }
}
