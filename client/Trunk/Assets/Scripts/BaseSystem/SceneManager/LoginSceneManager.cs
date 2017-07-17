using System;
using System.Collections;
using UnityEngine;

namespace Monster.SceneManager
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
            Transform uiRoot = GameObject.Find("Canvas").transform;
            GameObject preloadView = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/UI/Login/LoginView"), uiRoot) as GameObject;
            preloadView.transform.localScale = Vector3.one;
            (preloadView.transform as RectTransform).sizeDelta = Vector2.zero;
            (preloadView.transform as RectTransform).localPosition = Vector2.zero;
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
