
using UnityEngine;

namespace HotFix.GameFrame.ResourceManager
{
    class AsyncResourceRequest : IAsyncResourceRequest
    {
        public string id { get; set; }
        public bool isDone { get; set; }

        public Object asset;
    }
}
