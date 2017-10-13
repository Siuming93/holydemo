using Monster.BaseSystem;
using Monster.BaseSystem.ResourceManager;
using UnityEngine;

public abstract class BaseModelController
{
    public GameObject model { protected set; get; }

    public Transform root { protected set; get; }

    public RectTransform uiRoot { protected set; get; }

    public void LoadModelPrefabAsync(string path)
    {
        ResourcesFacade.Instance.LoadAsync(path, OnResourceLoadComplete);
    }

    public void LoadModelPrefab(string path)
    {
        origin = ResourcesFacade.Instance.Load<GameObject>(path);
        InstantiateModel(origin);
    }

    public void Move(Vector2 delta)
    {
        if (model != null)
        {
            Vector3 endPos = model.transform.localPosition + new Vector3(delta.x, 0, delta.y);
            Vector3 worldPos = model.transform.localToWorldMatrix * endPos;
            model.transform.localPosition = endPos;
            model.transform.LookAt(worldPos, Vector3.up);
        }
    }

    protected GameObject origin;

    protected  virtual void OnResourceLoadComplete(IAsyncResourceRequest resourcerequest)
    {
        var request = resourcerequest as AsyncResourceRequest;
        if (request == null)
        {
            return;
        }

        InstantiateModel(request.asset as GameObject);
    }

    protected virtual void InstantiateModel(GameObject origin)
    {
        this.origin = origin;
        model = Object.Instantiate(origin);
    }

    protected virtual void Dispose()
    {
        GameObject.Destroy(model);
        ResourcesFacade.Instance.UnLoadAsset(origin);
    }

    
}
