using System.Collections;

namespace HotFix.GameFrame.SceneManager
{
    public abstract class BaseSceneManager
    {
        public const string SCENE_NAME = "";
        public const string SCENE_PATH = "";

        public abstract IEnumerator BeforeEnterScene(object data);
        public abstract IEnumerator OnEnterScene(object data);
        public abstract IEnumerator BeforeLeaveScene(object data);
        public abstract IEnumerator AfterLeaveScene(object data);
    }
}
