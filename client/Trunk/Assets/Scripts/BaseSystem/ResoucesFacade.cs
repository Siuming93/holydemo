using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class ResourcesFacade
{
    private static ResourcesFacade mInstance = new ResourcesFacade();
    public static ResourcesFacade Instance {
        get { return mInstance;}
    }

    public Object Load(string path)
    {
        return Load(path, typeof(Object));
    }

    public Object Load<T>(string path)
    {
        return Load(path, typeof(T));
    }

    public Object Load(string path, Type systemTypeInstance)
    {
        return Resources.Load(path, systemTypeInstance);
    }

    public ResourceRequest LoadAsync(string path)
    {
        return LoadAsync(path, typeof(Object));
    }

    public ResourceRequest LoadAsync<T>(string path)
    {
        return LoadAsync(path, typeof(T));
    }

    public ResourceRequest LoadAsync(string path, Type systemTypeInstance)
    {
        return Resources.LoadAsync(path, systemTypeInstance);
    }

    public void UnLoadAsset(Object assetToUnload)
    {
        Resources.UnloadAsset(assetToUnload);
    }

    public AsyncOperation UnLoadUnusedAssets()
    {
        return Resources.UnloadUnusedAssets();
    }
}
