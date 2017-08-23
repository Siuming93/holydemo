using UnityEngine;
using UnityEngine.UI;
using XLua;

[Hotfix]
class ChatMediator : HotFixMediator
{
    public new const string NAME = "ChatMediator";

    private GameObject mView;

    private bool mHasSendFromCsharp;

    public ChatMediator(GameObject view) : base(NAME)
    {
        mView = view;
        Button button = mView.transform.FindChild("SendBtn").GetComponent<Button>();
        button.onClick.AddListener(OnSendButtonClick);
    }

    protected override string GetLuaScript()
    {
        return @"
                xlua.hotfix(CS.HotfixTest, 'OnSendButtonClick', function(self)
                        print('on send btn click, call from lua'..'object:'..self.mView.name )
                end)
            ";
    }

    [LuaCallCSharp]
    private void OnSendButtonClick()
    {
        if (mHasSendFromCsharp)
        {
            CheckLuaScript();
        }
        mHasSendFromCsharp = true;
        Debug.Log("on send btn click, call from c#");
    }

}
