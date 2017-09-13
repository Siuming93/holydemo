using Monster.BaseSystem;
using UnityEngine;

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

    private void RegisterNotifications()
    {
        
    }

    private void CreateView()
    {
        _view = Object.Instantiate(ResourcesFacade.Instance.Load<GameObject>(PANEL_PATH));
        
    }

}
