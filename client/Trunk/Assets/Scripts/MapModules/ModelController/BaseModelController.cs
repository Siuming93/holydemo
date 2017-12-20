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

    protected int angle;
    protected int posX;
    protected int posY;


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
    /// <summary>
    /// 模型正方向为顺时针方向,0度角为向上 与笛卡尔坐标系不同
    /// </summary>
    public ModelPosVO posInfo
    {
        get
        {
            return new ModelPosVO()
            {
                posX = (int)model.transform.localPosition.x,
                posY = (int)model.transform.localPosition.z,
                angle = angle,
            };
        }
    }

    protected Tweener _rotateTweener;
    public void LookAt(int angle)
    {
        if (model != null)
        {
            if (_rotateTweener != null)
            {
                _rotateTweener.Kill();
            }
            this.angle = angle;
            float duration = (model.transform.localEulerAngles.y + (angle - 90)) / 360 * 0.5f;
            _rotateTweener = model.transform.DOLocalRotate(new Vector3(0, -(angle - 90), 0), duration);
            //model.transform.localEulerAngles = new Vector3(0, -(angle - 90), 0);
        }
    }

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
