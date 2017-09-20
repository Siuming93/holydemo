using UnityEngine;

namespace Monster.BaseSystem.ResourceManager
{
    public class AsyncBundleCreatRequest: IAsyncRequest
    {
        public string id { get; set; }
        public string isDone { get; set; }

        public AssetBundleCreateRequest createRequest;

        public AsyncBundleCreatRequest(AssetBundleCreateRequest createRequest)
        {
            this.createRequest = createRequest;
        }
    }
}
