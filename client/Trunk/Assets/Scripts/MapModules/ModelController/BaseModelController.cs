using DG.Tweening;
using Monster.BaseSystem;
using Monster.BaseSystem.ResourceManager;
using UnityEngine;

public abstract class BaseModelController
{
    private bool _visible;
    public virtual bool visible
    {
        get { return _visible; }
        set
        {
            _visible = value;
            if (model != null)
            {
                model.gameObject.SetActive(_visible);
            }
        }
    }
    protected GameObject model { set; get; }

    protected Transform root { set; get; }

    protected RectTransform uiRoot {  set; get; }

    protected Animator animator { set; get; }


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

  

    #region animation

    protected bool _isMove;
    public void PlayMoveAnimation(bool isMove)
    {
        if (isMove != _isMove)
        {
            animator.SetBool("IsMove", isMove);
            _isMove = isMove;
        }
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
        this.model.SetActive(_visible);
        this.animator = model.transform.GetComponentInChildren<Animator>(true);
    }

    protected virtual void Dispose()
    {
        GameObject.Destroy(model);
        ResourcesFacade.Instance.UnLoadAsset(origin);
    }
#endregion
    
}
