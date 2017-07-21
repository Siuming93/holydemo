﻿using System.Collections;
using System.Collections.Generic;
using Monster.SceneManager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BaseSystem
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

            _managerMap.Add(LoginSceneManager.SCENE_NAME, new LoginSceneManager());
        }

        public IEnumerator LoadScene(string sceneName)
        {
            return LoadScene(sceneName, mCurSceneName);
        }

        public IEnumerator LoadScene(string sceneName, string oldSceneName)
        {
            BaseSceneManager manager, oldManager;

            if (sceneName != null && _managerMap.TryGetValue(sceneName, out manager))
            {
                if (oldSceneName != null && _managerMap.TryGetValue(oldSceneName, out oldManager))
                {
                    oldManager.BeforeLeaveScene(null);
                }
                AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
                while (!operation.isDone)
                {
                    yield return operation.progress;
                }
                yield return manager.OnEnterScene(null);
            }
            else
            {
                Debug.LogError("Dont contain scene:" + sceneName);
            }
        }
    }
}