using System.Collections;
using System.Collections.Generic;
using System.IO;
using HotFix.GameFrame.Common;
using HotFix.GameFrame.ResourceManager;
using HotFix.GameFrame.SceneManager;
using LitJson;
using UnityEngine;
using UnityEngine.UI;

namespace HotFix.GameFrame.PreLoad
{
    public class PreloadManager
    {
        private static PreloadManager m_Instance = new PreloadManager();

        public static PreloadManager Instance
        {
            get { return m_Instance; }
        }

        private PreloadManager() { }

        private PreLoadTask m_Task;

        public void Init()
        {
            m_Task = new PreLoadTask();
            TickProxy.Instance.StartCoroutine(PreLoad());
        }

        private IEnumerator PreLoad()
        {
            Debug.Log("load Scene");

#if UNITY_EDITOR
            var targetPath = Application.streamingAssetsPath;
            File.Copy(BundleConfig.BUNDLE_REMOTE_PATH + "/" + BundleConfig.CONFIG_FILE_NAME, targetPath + "/" + BundleConfig.CONFIG_FILE_NAME, true);
            this.Description = "加载AssetBundlesConfig";
            yield return 1;
#endif
            var url = BundleConfig.LOCALE_BUNDLE_FLODER_URL + "/" + BundleConfig.CONFIG_FILE_NAME;
            WWW www = new WWW(url);
            Debug.Log(url + " exits:" + File.Exists(url));
            while (!www.isDone)
            {
                Debug.Log("www ok?" + www.isDone);
                yield return null;
            }
            var map = FindAllBundleNames(www.text);
            Debug.Log("FindAllBundleNames");

            AssetBundleConfig.map = map;
            yield return null;
            ResourcesFacade.Instance.Init(null);

#if UNITY_EDITOR
            foreach (var name in map.Keys)
            {
                var bundleName = BundleTool.GetBundleFileName(name);
                File.Copy(BundleConfig.BUNDLE_REMOTE_PATH + "/" + bundleName, targetPath + "/" + bundleName, true);
                this.Description = "复制AssetBundlesConfig";
                yield return 1;
            }
#endif

            UnityEngine.SceneManagement.SceneManager.LoadScene("Preload");
            var root = GameObject.Find("Canvas");
            while (root == null)
            {
                root = GameObject.Find("Canvas");
                Debug.Log("GameObject.Find()" + www.isDone);
                yield return null;
            }
            new UIManager(root.transform);
            GameObject preloadView = ResourcesFacade.Instance.LoadPrefab("Prefab/UI/Preload/PreloadPanel");
            UIManager.Intance.AddChild(preloadView.transform);
            yield return null;
            yield return SceneSwitcher.Instance.LoadScene(LoginSceneManager.SCENE_NAME);
            Debug.Log("load Scene done");
            //ResourcesFacade.Instance.UnLoadAsset(preloadView);
            yield return null;
        }

        private Dictionary<string, AssetBundleInfoNode> FindAllBundleNames(string config)
        {
            var map = JsonMapper.ToObject<Dictionary<string, AssetBundleInfoNode>>(config);
            return map;
        }
    }
}