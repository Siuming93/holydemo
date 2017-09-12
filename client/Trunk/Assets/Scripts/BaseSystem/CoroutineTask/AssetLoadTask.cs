
using System.Collections;
using System.IO;
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
            foreach (var name in FindAllBundleNames())
            {
                File.Copy(BUNDLE_REMOTE_PATH + "/" + name, targetPath + "/" + name, true);
            }
        }

        private static string BUNDLE_REMOTE_PATH = Application.dataPath.Replace("Assets", "") + "Bundles";
        private string[] FindAllBundleNames()
        {
            var mainBudles = AssetBundle.LoadFromFile(BUNDLE_REMOTE_PATH + "/Bundles");
            return null;
        }
#endregion
    }
}
