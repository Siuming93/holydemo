using UnityEngine;

namespace HotFix.GameFrame.ResourceManager
{
    public class AsyncOperationRequest: IAsyncResourceRequest
    {
        public string id { get; set; }
        public bool isDone {
            get { return operation.isDone; }
        }

        public object state;
        public AsyncOperation operation;

        public AsyncOperationRequest(AsyncOperation operation)
        {
            this.operation = operation;
        }
    }
}
