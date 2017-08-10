using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Monster.BaseSystem.SceneManager
{
    class LoginSceneManager : BaseSceneManager
    {
        public new const string SCENE_NAME = "Login";

        public override IEnumerator BeforeEnterScene(object data)
        {
            return null;;
        }

        public override IEnumerator OnEnterScene(object data)
        {
            Object origin = ResourcesFacade.Instance.Load<GameObject>("Prefab/UI/Login/LoginView");
            GameObject view = GameObject.Instantiate(origin) as GameObject;
            UIManager.Intance.AddChild(view.transform);
            return null; ;
        }

        public override IEnumerator BeforeLeaveScene(object data)
        {
            return null; ;
        }

        public override IEnumerator AfterLeaveScene(object data)
        {
            return null; ;
        }
    }
}
