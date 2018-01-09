using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HotFix.GameFrame.ResourceManager
{
    class AssetBundleHint
    {
        public AssetBundle bundle;
        public string assetName;
        public int refCount;
        public string bundlePath;
        public UnityEngine.Object mainAsset;
        public List<AssetBundleHint> dependenceList = new List<AssetBundleHint>();
    }
}
