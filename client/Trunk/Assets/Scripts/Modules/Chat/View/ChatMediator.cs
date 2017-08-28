using System;
using Monster.Net;
using UnityEngine;
using UnityEngine.UI;
using XLua;

[Hotfix]
class ChatMediator :AbstractMediator
{
    public new const string NAME = "ChatMediator";
    private GameObject mView;

    public ChatMediator(GameObject view) : base(NAME)
    {
        mView = view;
        Button button = mView.transform.FindChild("SendBtn").GetComponent<Button>();
        button.onClick.AddListener(OnSendButtonClick);

        RegisterNotificationHandler(NotificationConst.ON_GET_TALK_MSG, OnGetTalkMsg, true);
    }

    private void OnSendButtonClick()
    {

    }

    private void OnGetTalkMsg(object obj)
    {

    }

}
