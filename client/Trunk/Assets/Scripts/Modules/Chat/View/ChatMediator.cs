using UnityEngine;
using UnityEngine.UI;
using XLua;

[Hotfix]
class ChatMediator //: HotFixMediator
{
    LuaEnv luaenv = new LuaEnv();

    public int tick = 0; //如果是private的，在lua设置xlua.private_accessible(CS.HotfixTest)后即可访问

    private void Print()
    {
         Debug.Log(">>>>>>>>Update in C#, tick = " + tick);
    }

    public new const string NAME = "ChatMediator";

    private GameObject mView;

    private bool mHasSendFromCsharp;

    public ChatMediator(GameObject view) //: base(NAME)
    {
        mView = view;
        Button button = mView.transform.FindChild("SendBtn").GetComponent<Button>();
        button.onClick.AddListener(OnSendButtonClick);
    }

//    protected override string GetLuaScript()
//    {
//        return @"
//                xlua.hotfix(CS.HotfixTest, 'Print', function(self)
//                        print('on send btn click, call from lua'..'object:'..self.mView.name )
//                end)
//            ";
//    }

    private void OnSendButtonClick()
    {
        Print();
        luaenv.DoString(@"
                xlua.hotfix(CS.HotfixTest, 'Print', function(self)
                    self.tick = self.tick + 1
                    print('<<<<<<<<Update in lua, tick = ' .. self.tick)
                end)
                self.Print()
            ");
        Print();
    }

}
