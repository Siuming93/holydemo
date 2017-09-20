using UnityEngine;

namespace Monster.BaseSystem.ResourceManager
{
    public class AsyncBundleRequest: IAsyncRequest
    {
        public string id { get; set; }
        public string isDone { get; set; }

        public AssetBundleRequest request;

        public AsyncBundleRequest(AssetBundleRequest request)
        {
            this.request = request;
        }
    }
}
