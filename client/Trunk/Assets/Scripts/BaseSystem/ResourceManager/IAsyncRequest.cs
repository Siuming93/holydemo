namespace Monster.BaseSystem.ResourceManager
{
    public interface IAsyncRequest
    {
        string id { get; }
        string isDone { get; }
    }
}
