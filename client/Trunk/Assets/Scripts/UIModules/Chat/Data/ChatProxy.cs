using System.Collections.Generic;
using Monster.Protocol;

public class ChatProxy : BaseProxy
{
    public new const string NAME = "ChatProxy";

    private List<ChatVO> mList;
    public ChatProxy() : base(NAME)
    {
        RegisterMessageHandler(MsgIDDefineDic.ScTalk, OnScTalkMessage);
        mList = new List<ChatVO>();
    }

    public override void OnRemove()
    {
        UnRegisterMessageHandler(MsgIDDefineDic.ScTalk);
        base.OnRemove();
    }

    private void OnScTalkMessage(object msg)
    {
        ScTalk info = msg as ScTalk;;
        mList.Add(new ChatVO() {content = info.content, fromPlayerName = info.form});
        SendNotification(NotificationConst.ON_GET_TALK_MSG);
    }
}
