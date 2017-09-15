using System;
using System.Collections.Generic;
using Monster.BaseSystem;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Monster.BaseSystem
{
    public class BundleResourceManager : IResourceManager
    {
        #region interface 

        public void Init(object data)
        {
            InitHintMap();
        }

        public Object Load(string path)
        {
            return DoLoad(path, typeof(Object));
        }
        public GameObject LoadPrefab(string path)
        {
            return DoLoad(path, typeof(GameObject)) as GameObject;
        }
        public Object Load(string path, Type systemTypeInstance)
        {
            return DoLoad(path, systemTypeInstance) as Object;
        }
        public T Load<T>(string path) where T : Object
        {
            return DoLoad<T>(path);
        }
        public AsyncOperation LoadAsync(string path)
        {
            return DoLoadAsync(path);
        }
        public AsyncOperation LoadAsync<T>(string path) where T : Object
        {
            return DoLoadAsync(path);
        }
        public AsyncOperation LoadAsync(string path, Type systemTypeInstance)
        {
            return DoLoadAsync(path);
        }
        public void UnLoadAsset(Object assetToUnload)
        {
            Object.Destroy(assetToUnload);
            Resources.UnloadAsset(assetToUnload);
        }
        public AsyncOperation UnLoadUnusedAssets()
        {
            return Resources.UnloadUnusedAssets();
        }
        #endregion

        private Object DoLoad(string path, Type type)
        {
            var id = GetID(path);
            AssetBundleHint hint;
            if (hintMap.TryGetValue(id, out hint))
            {
                if (hint.bundle == null)
                {
                    hint.bundle = AssetBundle.LoadFromFile(hint.bundlePath);
                }
                return hint.bundle.LoadAsset(hint.assetName);
            }
            else
            {
                Debug.LogError("没有Asset资源");
                return null;
            }
        }
        private T DoLoad<T>(string path) where T : Object
        {
            return DoLoad(path, typeof(T)) as T;
        }
        private string GetUri(string path)
        {
            var arr = path.Split('/');
            return GameConfig.BUNDLE_PATH + "/" + arr[arr.Length - 1] + ".assetbundle";
        }

        private string GetID(string path)
        {
            return "Assets/Resources/" + path;
        }
        private AsyncOperation DoLoadAsync(string path)
        {
            return AssetBundle.LoadFromFileAsync(GetUri(path));
        }

        #region  Reference Countor

        private Dictionary<string, AssetBundleHint> hintMap;
        private Dictionary<long, AssetBundleHint> objMap;

        private void InitHintMap()
        {
            hintMap = new Dictionary<string, AssetBundleHint>();
            foreach (var path in AssetBundleConfig.map.Keys)
            {
                var node = AssetBundleConfig.map[path];
                var pathNoExtension = GetPathNoExtension(path);
                hintMap[pathNoExtension] = new AssetBundleHint()
                {
                    bundlePath = Application.streamingAssetsPath + "/" + node.assetName + ".assetbundle",
                    assetName = node.assetName
                };
            }
        }

        private string GetPathNoExtension(string path)
        {
            int index = path.LastIndexOf('.');
            if (index > 0 && index < path.Length)
            {
                return path.Substring(0, index);
            }
            return path;
        }
        private class AssetBundleHint
        {
            public AssetBundle bundle;
            public string assetName;
            public int refCount;
            public string bundlePath;
        }
        #endregion
    }


}
