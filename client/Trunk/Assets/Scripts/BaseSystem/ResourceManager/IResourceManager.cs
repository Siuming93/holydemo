using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Monster.BaseSystem.ResourceManager
{
    public interface IResourceManager
    {
        void Init(object data);
        Object Load(string path);
        Object Load(string path, Type type);
        T Load<T>(string path) where T : Object;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns>已经实例化的GameObject</returns>
        GameObject LoadPrefab(string path);
        IAsyncRequest LoadAsync(string path);
        IAsyncRequest LoadAsync<T>(string path) where T : Object;
        IAsyncRequest LoadAsync(string path, Type systemTypeInstance);
        void UnLoadAsset(Object assetToUnload);
        IAsyncRequest UnLoadUnusedAssets();
    }
}
