using DG.Tweening;
using Monster.BaseSystem;
using Monster.BaseSystem.ResourceManager;
using UnityEngine;

public abstract class BaseModelController
{
    public GameObject model { protected set; get; }

    public Transform root { protected set; get; }

    public RectTransform uiRoot { protected set; get; }

    public Animator animator { protected set; get; }

    #region public funcs
    #region load
    public void LoadModelPrefabAsync(string path)
    {
        ResourcesFacade.Instance.LoadAsync(path, OnResourceLoadComplete);
    }

    public void LoadModelPrefab(string path)
    {
        origin = ResourcesFacade.Instance.Load<GameObject>(path);
        InstantiateModel(origin);
    }
    #endregion

    #region move
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

    public void MoveTo(Vector2 pos)
    {
        if (model != null)
        {
            Vector3 endPos = new Vector3(pos.x, 0f, pos.y);
            model.transform.localPosition = endPos;
        }
    }

    public void LookAt(Vector2 dir)
    {
        if (model != null)
        {
            Vector3 endPos = model.transform.localPosition +  new Vector3(dir.x, 0, dir.y);
            Vector3 worldPos = endPos;
            model.transform.LookAt(worldPos, Vector3.up);
        }
    }
    #endregion

    #region animation

    public void PlayMoveAnimation(bool isMove)
    {
        animator.SetBool("IsMove", isMove);
    }

    public void PlaySkillAnimation(SkillAnimationMeta meta)
    {
        //vp_Timer.In(meta.time, SetAnimiationTrigger, meta.name);
        animator.SetTrigger(meta.name);
    }
    private void SetAnimiationTrigger(object id)
    {
        animator.SetTrigger((string)id);
    }

    public void PlayDeathAnimation()
    {
        animator.SetTrigger("Death");
    }
    #endregion

    #endregion

    #region protected
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
        this.model = Object.Instantiate(origin);
        this.animator = model.transform.GetComponentInChildren<Animator>();
    }

    protected virtual void Dispose()
    {
        GameObject.Destroy(model);
        ResourcesFacade.Instance.UnLoadAsset(origin);
    }
#endregion
    
}
