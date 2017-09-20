using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Monster.BaseSystem.ResourceManager
{
    public class EditorResourceManager : BaseResourceManager
    {

        public void UnLoadAsset(Object assetToUnload)
        {
            //todo
            Resources.UnloadAsset(assetToUnload);
        }


        protected override GameObject DoLoadPrefab(string path)
        {
            throw new NotImplementedException();
        }

        protected override Object DoLoad(string path, Type type)
        {
            throw new NotImplementedException();
        }


        protected override IAsyncRequest DoLoadAsync(string path, Type type, ResourceAsyncCallBack callBack)
        {
            throw new NotImplementedException();
        }

        protected override T DoLoad<T>(string path)
        {
            throw new NotImplementedException();
        }


        protected override IAsyncRequest DoLoadAsync<T>(string path, ResourceAsyncCallBack callBack)
        {
            throw new NotImplementedException();
        }

        protected override void DoUnLoadAsset(Object assetToUnload)
        {
            throw new NotImplementedException();
        }
    }
}
