using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Monster.BaseSystem
{
    public interface IResourceManager
    {
        Object Load(string path);
        Object Load(string path, Type type);
        T Load<T>(string path) where T : Object;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns>已经实例化的GameObject</returns>
        GameObject LoadPrefab(string path);
        AsyncOperation LoadAsync(string path);
        AsyncOperation LoadAsync<T>(string path) where T : Object;
        AsyncOperation LoadAsync(string path, Type systemTypeInstance);
        void UnLoadAsset(Object assetToUnload);
        AsyncOperation UnLoadUnusedAssets();
    }
}
