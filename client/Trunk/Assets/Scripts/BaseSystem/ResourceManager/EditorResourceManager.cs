using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Monster.BaseSystem.ResourceManager
{
    public class EditorResourceManager : IResourceManager
    {
        public void Init(object data)
        {
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

        public IAsyncRequest LoadAsync(string path)
        {
            return LoadAsync(path, typeof(Object));
        }

        public IAsyncRequest LoadAsync<T>(string path) where T : Object
        {
            return LoadAsync(path, typeof(T));
        }

        public IAsyncRequest LoadAsync(string path, Type systemTypeInstance)
        {
            return new AsyncOperationRequest() { operation = Resources.LoadAsync(path, systemTypeInstance) };
        }

        public void UnLoadAsset(Object assetToUnload)
        {
            //todo
            Resources.UnloadAsset(assetToUnload);
        }

        public IAsyncRequest UnLoadUnusedAssets()
        {
            //todo
            return new AsyncOperationRequest() {operation = Resources.UnloadUnusedAssets()};
        }

        private Object DoLoad(string path, Type type)
        {
            return Resources.Load(path, type);
        }

        private T DoLoad<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }
    }
}
