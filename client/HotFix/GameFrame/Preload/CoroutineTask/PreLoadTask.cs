
using System.Collections;
using System.Collections.Generic;
using HotFix.GameFrame.Common;
using HotFix.GameFrame.ResourceManager;
using HotFix.GameFrame.SceneManager;
using UnityEngine;

namespace HotFix.GameFrame.PreLoad
{
    public class PreLoadTask : BaseCoroutineTask
    {

        public override IEnumerator Run()
        {
            Debug.Log("load Scene");
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


            new UIManager(GameObject.Find("Canvas").transform);
            GameObject preloadView = ResourcesFacade.Instance.LoadPrefab("Prefab/UI/Preload/PreloadPanel");
            UIManager.Intance.AddChild(preloadView.transform);
            yield return step++;
            Debug.Log("load Scene");
            yield return SceneSwitcher.Instance.LoadScene(LoginSceneManager.SCENE_NAME);
            Debug.Log("load Scene done");
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
