using System;
using Monster.BaseSystem;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Monster.BaseSystem
{
    class BundleResourceManager : IResourceManager
    {
        #region interface 
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
            Resources.UnloadAsset(assetToUnload);
        }
        public AsyncOperation UnLoadUnusedAssets()
        {
            return Resources.UnloadUnusedAssets();
        }
#endregion

        private Object DoLoad(string path, Type type)
        {
            var assetBundle = AssetBundle.LoadFromFile(GetUri(path));
            return assetBundle.LoadAsset(GetName(path));
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

        private string GetName(string path)
        {
            var arr = path.Split('/');
            return arr[arr.Length - 1] + ".assetbundle";
        }
        private AsyncOperation DoLoadAsync(string path)
        {
            return AssetBundle.LoadFromFileAsync(GetUri(path));
        }
    }
}
