using System.Collections;

namespace Monster.BaseSystem.SceneManager
{
    class PreloadSceneManager : BaseSceneManager
    {
        public override IEnumerator BeforeEnterScene(object data)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerator OnEnterScene(object data)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerator BeforeLeaveScene(object data)
        {
            UIManager.Intance.ClearScreen();
            return null;
        }

        public override IEnumerator AfterLeaveScene(object data)
        {
            throw new System.NotImplementedException();
        }
    }
}
