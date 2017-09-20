using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Monster.BaseSystem.ResourceManager
{
    public abstract class BaseResourceManager:IResourceManager
    {
        private List<IAsyncRequest> _doneARList;
        private Dictionary<IAsyncRequest, ResourceAsyncCallBack> _asyncCallBacks;
        
        #region Interface
        public virtual void Init(object data)
        {
            _doneARList = new List<IAsyncRequest>();
            _asyncCallBacks = new Dictionary<IAsyncRequest, ResourceAsyncCallBack>();
        }

        GameObject IResourceManager.LoadPrefab(string path)
        {
            return LoadPrefab(path);
        }

        public Object Load(string path)
        {
            return DoLoad(path, typeof(Object));
        }

        public GameObject LoadPrefab(string path)
        {
            return DoLoadPrefab(path);
        }

        public Object Load(string path, Type systemTypeInstance)
        {
            return DoLoad(path, systemTypeInstance) as Object;
        }

        public T Load<T>(string path) where T : Object
        {
            return DoLoad<T>(path);
        }

        public IAsyncRequest LoadAsync(string path, ResourceAsyncCallBack callBack)
        {
            return DoLoadAsync(path, typeof(Object), callBack);
        }

        public IAsyncRequest LoadAsync(string path, Type systemTypeInstance, ResourceAsyncCallBack callBack)
        {
            return DoLoadAsync(path, systemTypeInstance, callBack);
        }
        public IAsyncRequest LoadAsync<T>(string path, ResourceAsyncCallBack callBack) where T : Object
        {
            return DoLoadAsync<T>(path, callBack);
        }

        public void UnLoadAsset(UnityEngine.Object assetToUnload)
        {
            DoUnLoadAsset(assetToUnload);
        }

        public IAsyncRequest UnLoadUnusedAssets()
        {
            return new AsyncOperationRequest(Resources.UnloadUnusedAssets());
        }

        public void Trik()
        {
            DoTrik();
        }
        #endregion

#region self funcs

        private void DoTrik()
        {
            var itor = _asyncCallBacks.GetEnumerator();
            while (itor.MoveNext())
            {
                var ar = itor.Current.Key;
                if (ar.isDone)
                {
                    _doneARList.Add(ar);
                }
            }

            for (int i = 0, count = _doneARList.Count; i < count; i++)
            {
                var ar = _doneARList[i];
                var callback = _asyncCallBacks[ar];
                if (callback != null)
                {
                    callback.Invoke(ar);
                }
                _asyncCallBacks.Remove(ar);
            }
        }
#endregion
        protected void AddAsyncCallback(IAsyncRequest request, ResourceAsyncCallBack callback)
        {
            _asyncCallBacks.Add(request, callback);
        }

        protected abstract GameObject DoLoadPrefab(string path);

        protected abstract Object DoLoad(string path, Type type);
        protected abstract IAsyncRequest DoLoadAsync(string path, Type type, ResourceAsyncCallBack callBack);


        protected abstract T DoLoad<T>(string path) where T : Object;
        protected abstract IAsyncRequest DoLoadAsync<T>(string path, ResourceAsyncCallBack callBack) where T : Object;

        protected abstract void DoUnLoadAsset(Object assetToUnload);
    }
}
