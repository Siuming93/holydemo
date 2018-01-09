using System.Collections;

namespace HotFix.GameFrame.SceneManager
{
    class PreloadSceneManager : BaseSceneManager
    {
        public const string SCENE_NAME = "Preload";
        public static bool isFirstLoad = false;

        public override IEnumerator BeforeEnterScene(object data)
        {
            return null;
        }

        public override IEnumerator OnEnterScene(object data)
        {
            return null;
        }

        public override IEnumerator BeforeLeaveScene(object data)
        {
            UIManager.Intance.ClearScreen();
            return null;
        }

        public override IEnumerator AfterLeaveScene(object data)
        {
            return null;
        }
    }
}
