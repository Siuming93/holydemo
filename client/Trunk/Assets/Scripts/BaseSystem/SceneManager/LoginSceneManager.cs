using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Monster.BaseSystem.SceneManager
{
    class LoginSceneManager : BaseSceneManager
    {
        public new const string SCENE_NAME = "Login";

        private GameObject _loginView;
        public override IEnumerator BeforeEnterScene(object data)
        {
            return null;;
        }

        public override IEnumerator OnEnterScene(object data)
        {
            Object origin = ResourcesFacade.Instance.Load<GameObject>("Prefab/UI/Login/LoginView");
            _loginView = GameObject.Instantiate(origin) as GameObject;
            UIManager.Intance.AddChild(_loginView.transform);
            return null; ;
        }

        public override IEnumerator BeforeLeaveScene(object data)
        {
            UIManager.Intance.RemoveChild(_loginView.transform);
            return null; ;
        }

        public override IEnumerator AfterLeaveScene(object data)
        {
            return null; ;
        }
    }
}
