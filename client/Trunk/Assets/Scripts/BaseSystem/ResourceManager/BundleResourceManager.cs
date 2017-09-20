using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Monster.BaseSystem.ResourceManager
{
    public class BundleResourceManager : IResourceManager
    {
        #region interface 
        public void Init(object data)
        {
            HintInit();
        }

        public Object Load(string path)
        {
            return DoLoad(path, typeof(Object));
        }
        public GameObject LoadPrefab(string path)
        {
            return DoLoad(path, typeof(GameObject), true) as GameObject;;
        }
        public Object Load(string path, Type systemTypeInstance)
        {
            return DoLoad(path, systemTypeInstance) as Object;
        }
        public T Load<T>(string path) where T : Object
        {
            return DoLoad<T>(path);
        }
        public IAsyncRequest LoadAsync(string path)
        {
            return DoLoadAsync(path);
        }
        public IAsyncRequest LoadAsync<T>(string path) where T : Object
        {
            return DoLoadAsync(path);
        }
        public IAsyncRequest LoadAsync(string path, Type systemTypeInstance)
        {
            return DoLoadAsync(path);
        }
        public void UnLoadAsset(Object assetToUnload)
        {
            AssetBundleHint hint;
            int assetInstacneId = assetToUnload.GetInstanceID();
            if (loadedAssetHintMap.TryGetValue(assetInstacneId, out hint))
            {
                if (assetInstacneId != hint.mainAsset.GetInstanceID())
                {
                    Object.Destroy(assetToUnload);
                }
                HintReduceRefCount(hint);
                loadedAssetHintMap.Remove(assetInstacneId);
            }
            else
            {
                Debug.LogError("Asset Not Loaded From Facade:", assetToUnload);
                //Object.Destroy(assetToUnload);                     
            }
        }
        public AsyncOperation UnLoadUnusedAssets()
        {
            return Resources.UnloadUnusedAssets();
        }
        #endregion

        #region DoLoad Funcs
        private Object DoLoad(string path, Type type, bool instantiate = false)
        {
            AssetBundleHint hint = HintGet(path);
            if (hint != null)
            {
                return HintLoadMainAsset(hint, instantiate);
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
        private IAsyncRequest DoLoadAsync(string path, AsyncCallback callback = null)
        {
            AssetBundleHint hint = HintGet(path);
            if (hint != null)
            {
               /// return HintLoadMainAsset(hint, instantiate);
            }
            else
            {
                Debug.LogError("没有Asset资源");
                return null;
            }

            return null;
             ///AssetBundle.LoadFromFileAsync("");
        }
        #endregion

        #region  Reference Countor

        private Dictionary<string, AssetBundleHint> hintMap;
        private Dictionary<long, AssetBundleHint> loadedAssetHintMap;

        private void HintInit()
        {
            hintMap = new Dictionary<string, AssetBundleHint>();
            loadedAssetHintMap= new Dictionary<long, AssetBundleHint>();
            foreach (var pair in AssetBundleConfig.map)
            {
                var name = pair.Key;
                var node = pair.Value;
                hintMap[name] = new AssetBundleHint()
                {
                    bundlePath = BundleTool.GetBundleFilePath(node.assetName),
                    assetName = node.assetName
                };
            }

            foreach (var pair in AssetBundleConfig.map)
            {
                var name = pair.Key;
                var node = pair.Value;
                var hint = hintMap[name];
                foreach (var depName in node.depenceList)
                {
                    hint.dependenceList.Add(hintMap[depName]);
                }
            }
        }
        private AssetBundleHint HintGet(string path)
        {
            var name = BundleTool.GetAssetName(path);
            AssetBundleHint hint;
            hintMap.TryGetValue(name, out hint);
            return hint;
        }
        private Object HintLoadMainAsset(AssetBundleHint hint, bool instantiate = false)
        {
            if (hint.bundle == null)
            {
                HintLoadAssetBundle(hint);
            }
            if (hint.mainAsset == null)
            {
                hint.mainAsset = hint.bundle.LoadAsset(hint.assetName);
            }
            HintIncreaseRefCount(hint);
            var asset = instantiate ? Object.Instantiate(hint.mainAsset) : hint.mainAsset;
            loadedAssetHintMap.Add(asset.GetInstanceID(), hint);
            return asset;
        }
        private void HintLoadAssetBundle(AssetBundleHint hint)
        {
            foreach (var depHint in hint.dependenceList)
            {
                HintLoadAssetBundle(depHint);
            }
            hint.bundle = hint.bundle ?? AssetBundle.LoadFromFile(hint.bundlePath);
        }

        private List<IAsyncRequest> asyncRequests;
        private IAsyncRequest HintLoadMainAssetAsync(AssetBundleHint hint, ResourceAsyncCallBack callback)
        {
            if (hint.bundle == null)
            {
                return HintLoadAssetBundleAsync(hint, callback);
            }
            return HintLoadMainAssetAsync(hint);
        }

        private Dictionary<IAsyncRequest, AssetBundleHint> asyncOpertaionMap;
        private IAsyncRequest HintLoadAssetBundleAsync(AssetBundleHint hint, ResourceAsyncCallBack callback)
        {
            var operation = AssetBundle.LoadFromFileAsync(hint.bundlePath);
            var request = new AsyncOperationRequest(operation);
            asyncCallbacks.Add(request, callback);
            return request;
        }

        private void OnAssetBundleLoadAsyncComplete(AssetBundleCreateRequest request, AssetBundleHint hint, ResourceAsyncCallBack callBack)
        {
            if (request.isDone)
            {
                hint.bundle = request.assetBundle;
            }

            var assetLoadRequest = HintLoadMainAssetAsync(hint);
        }

        private IAsyncRequest HintLoadMainAssetAsync(AssetBundleHint hint)
        {
            var request = hint.bundle.LoadAllAssetsAsync();
            return new AsyncBundleRequest(request);
        }
        private void HintRecyle(AssetBundleHint hint)
        {
            hint.mainAsset = null;
            hint.bundle.Unload(true);
            Object.Destroy(hint.bundle);
            hint.bundle = null;
        }
        private void HintReduceRefCount(AssetBundleHint hint)
        {
            foreach (var depHint in hint.dependenceList)
            {
                HintReduceRefCount(depHint);
            }

            hint.refCount--;
            if (hint.refCount == 0)
            {
                HintRecyle(hint);
            }
        }
        private void HintIncreaseRefCount(AssetBundleHint hint)
        {
            foreach (var depHint in hint.dependenceList)
            {
                HintReduceRefCount(depHint);
            }

            hint.refCount++;
        }

        private class AssetBundleHint
        {
            public AssetBundle bundle;
            public string assetName;
            public int refCount;
            public string bundlePath;
            public Object mainAsset;
            public List<AssetBundleHint> dependenceList = new List<AssetBundleHint>();
        }
        #endregion

        #region base

        IAsyncRequest IResourceManager.UnLoadUnusedAssets()
        {
            throw new NotImplementedException();
        }

        private Dictionary<IAsyncRequest, ResourceAsyncCallBack> asyncCallbacks;
        public void AddAscyncCallback(IAsyncRequest asyncRequest, ResourceAsyncCallBack callback)
        {
            //AssetBundle.
        }
        #endregion
    }


}
