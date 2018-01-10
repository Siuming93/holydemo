using System.Collections;
using System.Collections.Generic;
using HotFix.GameFrame.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HotFix.GameFrame.SceneController
{
    public class SceneSwitcher
    {
        private static SceneSwitcher mInstance = new SceneSwitcher();
        public static SceneSwitcher Instance
        {
            get { return mInstance; }
        }

        public Dictionary<string, BaseSceneManager> _managerMap;

        private string mCurSceneName;

        public SceneSwitcher()
        {
            _managerMap = new Dictionary<string, BaseSceneManager>();

            _managerMap.Add(PreloadSceneManager.SCENE_NAME, new PreloadSceneManager());
            _managerMap.Add(LoginSceneManager.SCENE_NAME, new LoginSceneManager());
            _managerMap.Add(LeiTaiSceneManager.SCENE_NAME, new LeiTaiSceneManager());
        }

        public void LoadScene(string sceneName)
        {
            LoadScene(sceneName, mCurSceneName);
        }

        public void LoadScene(string sceneName, string oldSceneName)
        {
            if (!string.IsNullOrEmpty(sceneName))
            {
                TickProxy.Instance.StartCoroutine(AsyncLoad(sceneName, oldSceneName));
            }
            else
            {
                Debug.LogError("Dont contain scene:" + sceneName);
            }
        }

        public IEnumerator AsyncLoad(string sceneName, string oldSceneName)
        {
            BaseSceneManager manager, oldManager;
            if (_managerMap.TryGetValue(sceneName, out manager))
            {
                if (!string.IsNullOrEmpty(oldSceneName) && _managerMap.TryGetValue(oldSceneName, out oldManager))
                {
                    yield return oldManager.BeforeLeaveScene(null);
                }

                AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
                while (!operation.isDone)
                {
                    yield return operation.progress;
                }
                mCurSceneName = sceneName;
                yield return manager.OnEnterScene(null);
            }
           
        }
    }
}
