using UnityEngine;

namespace Monster.BaseSystem.ResourceManager
{
    public class AsyncOperationRequest: IAsyncRequest
    {
        public string id { get; set; }
        public bool isDone { get; set; }

        public AsyncOperation operation;

        public AsyncOperationRequest(AsyncOperation operation)
        {
            this.operation = operation;
        }
    }
}
