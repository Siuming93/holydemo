namespace Monster.BaseSystem.ResourceManager
{
    public interface IAsyncRequest
    {
        string id { get; }
        bool isDone { get; }
    }
}
