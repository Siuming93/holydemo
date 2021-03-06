﻿//using DG.Tweening;
//using Monster.BaseSystem;
//using UnityEngine;

//public abstract class BaseModelController
//{
//    private bool _visible;
//    public virtual bool visible
//    {
//        get { return _visible; }
//        set
//        {
//            _visible = value;
//            if (model != null)
//            {
//                model.gameObject.SetActive(_visible);
//            }
//        }
//    }
//    public GameObject model { set; get; }

//    protected Transform root { set; get; }

//    protected RectTransform uiRoot {  set; get; }

//    protected Animator animator { set; get; }

//    protected int angle;
//    protected int posX;
//    protected int posY;


//    #region public funcs
//    #region load
//    public void LoadModelPrefabAsync(string path)
//    {
//        ResourcesFacade.Instance.LoadAsync(path, OnResourceLoadComplete);
//    }

//    public void LoadModelPrefab(string path)
//    {
//        origin = ResourcesFacade.Instance.Load<GameObject>(path);
//        InstantiateModel(origin);
//    }
//    #endregion

//    #region animation

//    protected bool _isMove;
//    public void PlayMoveAnimation(bool isMove)
//    {
//        if (isMove != _isMove)
//        {
//            animator.SetBool("IsMove", isMove);
//            _isMove = isMove;
//        }
//    }

//    public void PlaySkillAnimation(SkillAnimationMeta meta)
//    {
//        //vp_Timer.In(meta.time, SetAnimiationTrigger, meta.name);
//        animator.SetTrigger(meta.name);
//    }
//    private void SetAnimiationTrigger(object id)
//    {
//        animator.SetTrigger((string)id);
//    }

//    public void PlayDeathAnimation()
//    {
//        animator.SetTrigger("Death");
//    }
//    #endregion
//    /// <summary>
//    /// 模型正方向为顺时针方向,0度角为向上 与笛卡尔坐标系不同
//    /// </summary>
//    public ModelPosVO posInfo
//    {
//        get
//        {
//            return new ModelPosVO()
//            {
//                posX = model.transform.position.x,
//                posY = model.transform.position.z,
//                angle = angle,
//            };
//        }
//    }


//    protected float cos;
//    protected float sin;
//    protected Tweener _rotateTweener;
//    protected const float PI_DIV_180 = Mathf.PI / 180f;
//    public void LookAt(int angle)
//    {
//        if (model != null)
//        {
//            if (_rotateTweener != null)
//            {
//                _rotateTweener.Kill();
//            }
//            this.angle = angle;
//            model.transform.localEulerAngles = new Vector3(0, -(angle - 90), 0);
//            cos = Mathf.Cos(angle * PI_DIV_180);
//            sin = Mathf.Sin(angle * PI_DIV_180);
//            //Debug.Log("angle " + angle + " cos " + cos + "sin " + sin);
//        }
//    }

//    #endregion

//    #region protected
//    protected GameObject origin;

//    protected  virtual void OnResourceLoadComplete(IAsyncResourceRequest resourcerequest)
//    {
//        var request = resourcerequest as AsyncResourceRequest;
//        if (request == null)
//        {
//            return;
//        }

//        InstantiateModel(request.asset as GameObject);
//    }

//    protected virtual void InstantiateModel(GameObject origin)
//    {
//        this.origin = origin;
//        this.model = Object.Instantiate(origin);
//        this.model.SetActive(_visible);
//        this.animator = model.transform.GetComponentInChildren<Animator>(true);
//    }

//    protected virtual void Dispose()
//    {
//        GameObject.Destroy(model);
//        ResourcesFacade.Instance.UnLoadAsset(origin);
//    }
//#endregion
    
//}
