using System.Collections;

namespace HotFix.GameFrame.SceneController
{
    class LoginSceneManager : BaseSceneManager
    {
        public new const string SCENE_NAME = "Login";

        public override IEnumerator BeforeEnterScene(object data)
        {
            return null; ;
        }

        public override IEnumerator OnEnterScene(object data)
        {
            ApplicationFacade.Instance.RegisterProxy(new LoginProxy());
            ApplicationFacade.Instance.RegisterMediator(new LoginMediator());
            return null; ;
        }

        public override IEnumerator BeforeLeaveScene(object data)
        {
            ApplicationFacade.Instance.RemoveMediator(LoginMediator.NAME);
            ApplicationFacade.Instance.RemoveProxy(LoginProxy.NAME);
            return null; ;
        }

        public override IEnumerator AfterLeaveScene(object data)
        {
            return null; ;
        }
    }
}
