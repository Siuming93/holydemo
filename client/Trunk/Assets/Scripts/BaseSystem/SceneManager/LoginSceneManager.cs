using System;
using System.Collections;
using Monster.BaseSystem.ResourceManager;
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
            ResourcesFacade.Instance.LoadAsync<GameObject>("Prefab/UI/Login/LoginView", OnLoginViewLoadComplete);
            return null; ;
        }

        private void OnLoginViewLoadComplete(ResourceManager.IAsyncResourceRequest resourceRequest)
        {
            var origin = (resourceRequest as AsyncResourceRequest).asset;
            _loginView = GameObject.Instantiate(origin) as GameObject;
            UIManager.Intance.AddChild(_loginView.transform);
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
