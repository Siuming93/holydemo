using UnityEngine;

namespace Monster.BaseSystem.ResourceManager
{
    class AsyncBundleRequest: IAsyncRequest
    {
        public string id { get; set; }
        public bool isDone { get; set; }

        public AssetBundleRequest request;

        public AssetBundleHint hint;

    }
}
