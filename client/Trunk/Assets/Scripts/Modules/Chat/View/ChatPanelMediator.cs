using UnityEngine;
using XLua;

public class ChatPanelMediator : AbstractMediator
{
    public new const string NAME = "ChatPanelMediator";
    public const string PANEL_PATH = "";

    private GameObject _view;
    public ChatPanelMediator(): base(NAME)
    {
        RegisterNotifications();
        CreateView();
    }

    [Hotfix]
    private void RegisterNotifications()
    {
        
    }

    [Hotfix]
    private void CreateView()
    {
        _view = Object.Instantiate(ResourcesFacade.Instance.Load<GameObject>(PANEL_PATH));
        
    }

}
