using UnityEditor;
using UnityEngine;

namespace Assets.Editor.AssetBundleBuilder
{
    internal class BuilderConfig
    {
        public string LoadPath = Application.dataPath + "/BundleResources";
        public string ExportPath = Application.dataPath.Replace("Assets","") + "Bundles";

        public BuildAssetBundleOptions options = BuildAssetBundleOptions.None;
        public BuildTarget target = BuildTarget.StandaloneWindows;
    }
}
