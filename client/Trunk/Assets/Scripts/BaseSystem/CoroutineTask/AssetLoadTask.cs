
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using SimpleJson;
using UnityEngine;

namespace Monster.BaseSystem.CoroutineTask
{
    public class AssetLoadTask: BaseCoroutineTask
    {
        public override IEnumerator Run()
        {
            Init();
            yield return 1;
            DownLoadBundles();
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

        private void DownLoadBundles()
        {
            var targetPath = Application.streamingAssetsPath;
            var map = FindAllBundleNames();
            AssetBundleConfig.map = map;
            foreach (var name in map.Values)
            {
                var bundleName = name.assetName + ".assetbundle";
                File.Copy(BUNDLE_REMOTE_PATH + "/" + bundleName, targetPath + "/" + bundleName, true);
            }
        }

        private static string BUNDLE_REMOTE_PATH = Application.dataPath.Replace("Assets", "") + "Bundles";
        private Dictionary<string, AssetRefrenceNode> FindAllBundleNames()
        {
            var reader = new StreamReader(File.OpenRead(GameConfig.BundleConfigPath));
            var config = reader.ReadToEnd();
            reader.Dispose();
            var map = JsonMapper.ToObject<Dictionary<string, AssetRefrenceNode>>(config);
            return map as Dictionary<string, AssetRefrenceNode>;
        }
        #endregion
    }
}
