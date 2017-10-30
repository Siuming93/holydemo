using Monster.Net;
using Monster.Protocol;

public class LeitaiWorldMapMediator : AbstractMediator
{
    public new const string NAME = "WorldMapMediator";
    public LeitaiWorldMapMediator(): base(NAME)
    {
        NetManager.Instance.SendMessage(new CsEnterScene() { id = 0, name = "leitai" });
    }
}
