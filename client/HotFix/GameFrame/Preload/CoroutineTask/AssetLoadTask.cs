
using System.Collections;
using System.Collections.Generic;
using HotFix.GameFrame.ResourceManager;
using UnityEngine;

namespace HotFix.GameFrame.PreLoad
{
    public class AssetLoadTask : BaseCoroutineTask
    {
        public override IEnumerator Run()
        {
            Init();
            yield return DownLoadBundles();
            this.Progress = 1;
            this.IsCompleted = true;
        }

        public override void Dispose()
        {
        }
        #region self funcs

        private void Init()
        {
            this.Progress = 0f;
            this.Description = "加载AssetBundles";
        }

        private IEnumerator DownLoadBundles()
        {
#if UNITY_EDITOR
            var targetPath = Application.streamingAssetsPath;
            File.Copy(BundleConfig.BUNDLE_REMOTE_PATH + "/" + BundleConfig.CONFIG_FILE_NAME, targetPath + "/" + BundleConfig.CONFIG_FILE_NAME, true);
            this.Description = "加载AssetBundlesConfig";
            yield return 1;
#endif
            var url = BundleConfig.LOCALE_BUNDLE_FLODER_URL + "/" + BundleConfig.CONFIG_FILE_NAME;
            WWW www = new WWW(url);
            yield return www;
            var map = FindAllBundleNames(www.text);
            AssetBundleConfig.map = map;
            yield return 1;
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
        }

        private Dictionary<string, AssetBundleInfoNode> FindAllBundleNames(string config)
        {
            return new Dictionary<string, AssetBundleInfoNode>();
            //var map = JsonMapper.ToObject<Dictionary<string, AssetBundleInfoNode>>(config);
            //return map;
        }
        #endregion
    }
}
