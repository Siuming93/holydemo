using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Monster.BaseSystem
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

        public AsyncOperation LoadAsync(string path)
        {
            return LoadAsync(path, typeof(Object));
        }

        public AsyncOperation LoadAsync<T>(string path) where T : Object
        {
            return LoadAsync(path, typeof(T));
        }

        public AsyncOperation LoadAsync(string path, Type systemTypeInstance)
        {
            return Resources.LoadAsync(path, systemTypeInstance);
        }

        public void UnLoadAsset(Object assetToUnload)
        {
            //todo
            Resources.UnloadAsset(assetToUnload);
        }

        public AsyncOperation UnLoadUnusedAssets()
        {
            //todo
            return Resources.UnloadUnusedAssets();
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
