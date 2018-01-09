namespace HotFix.GameFrame.ResourceManager
{
    public interface IAsyncResourceRequest
    {
        string id { get; }
        bool isDone { get; }
    }
}
