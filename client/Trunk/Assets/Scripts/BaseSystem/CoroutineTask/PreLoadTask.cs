﻿
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Monster.BaseSystem.SceneManager;
using Monster.Net;
using UnityEngine;

namespace Monster.BaseSystem.CoroutineTask
{
    public class PreLoadTask : BaseCoroutineTask
    {

        public override IEnumerator Run()
        {
            int step = 0;
            Init();
            for (int i = 0, count = mCoroutineTasks.Count; i < count; i++)
            {
                BaseCoroutineTask task = mCoroutineTasks[i].Key;
                while (!task.IsCompleted)
                {
                    this.Description = task.Description;
                    this.Progress = mCoroutineTasks[i].Value;
                    yield return task.Run();
                }
            }
            yield return step++;

            ResourcesFacade.Instance.Init(null);
            UpdateProxy.Instance.UpdateEvent += ResourcesFacade.Instance.Trik;
            yield return step++;

            MetaManager metaManager = new MetaManager();
            metaManager.AddMeta(MetaManager.FakeMeta());
            yield return step++;

            NetManager.Instance.Init();
            yield return step++;

            new UIManager(GameObject.Find("Canvas").transform);
            GameObject preloadView = ResourcesFacade.Instance.LoadPrefab("Prefab/UI/Preload/PreloadPanel");
            GameObject newObj = Object.Instantiate(preloadView);
            UIManager.Intance.AddChild(preloadView.transform);
            yield return step++;

            yield return SceneSwitcher.Instance.LoadScene(LoginSceneManager.SCENE_NAME);
            ResourcesFacade.Instance.UnLoadAsset(preloadView);
            yield return step++;
        }

        public override void Dispose()
        {
        }
        #region self funcs

        private List<KeyValuePair<BaseCoroutineTask, float>> mCoroutineTasks;
        private void Init()
        {
            this.Progress = 0f;
            mCoroutineTasks = mCoroutineTasks = new List<KeyValuePair<BaseCoroutineTask, float>>()
            {
                {new KeyValuePair<BaseCoroutineTask, float>( new AssetLoadTask(), 0.2f)},
            };
        }
        #endregion
    }
}
