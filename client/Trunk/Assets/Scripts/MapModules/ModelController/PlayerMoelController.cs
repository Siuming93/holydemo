using System.Collections;
using DG.Tweening;
using Monster.Net;
using Monster.Protocol;
using UnityEngine;
using System.Collections.Generic;

public class PlayerMoelController : BaseModelController
{
    public Transform modelTransform { get { return model.transform; }}

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

    protected bool addListener = false;
    public void StartMove(float angle)
    {
        isMove = true;
        moveDir = MathUtil.GetCoordinate(angle);
        LookAt(angle);
        PlayMoveAnimation(true);
        if (!addListener)
        {
            addListener = true;
            UpdateProxy.Instance.UpdateEvent += Update;
        }
    }
    public void EndMove()
    {
        isMove = false;
        if (addListener)
        {
            UpdateProxy.Instance.UpdateEvent -= Update;
            addListener = false;
        }
        PlayMoveAnimation(false);
    }

    public void UpdateMoveDir(float angle)
    {
        moveDir = MathUtil.GetCoordinate(angle);
        LookAt(angle);
    }

    protected bool isMove;
    protected Vector2 moveDir;
    protected void Update()
    {
        if (model != null)
        {
            if (isMove)
            {
                Vector3 delta = new Vector3(moveDir.x, 0f, moveDir.y) * PlayerProperty.RunSpeed * Time.deltaTime;
                Vector3 endPos = model.transform.localPosition + delta;
                model.transform.localPosition = endPos;
            }
        }
    }

    protected Tweener moveTweener;
    public void MoveTo(Vector3 pos)
    {
        if (model != null)
        {
            float distance = (pos - model.transform.localPosition).magnitude;
            Debug.Log(distance);
            if (moveTweener != null && moveTweener.IsPlaying())
                moveTweener.Kill();
            moveTweener = model.transform.DOLocalMove(pos, distance / PlayerProperty.RunSpeed).SetEase(Ease.Linear);
        }
    }
    #endregion
}

public class OtherPlayerMoelController : BaseModelController
{
    protected Tweener moveTweener;

    protected Queue<Vector3> targetList = new Queue<Vector3>();
    protected Vector3 _lastPos;
    protected void MoveTo(Vector3 pos)
    {
        if (model != null)
        {
            float distance = (pos - model.transform.localPosition).magnitude;

            if (!Mathf.Approximately(_lastPos.x, pos.x) || !Mathf.Approximately(_lastPos.z, pos.z))
                targetList.Enqueue(pos);

            if (targetList.Count > 3)
            {
                moveTweener = model.transform.DOLocalMove(pos, distance / PlayerProperty.RunSpeed).SetEase(Ease.Linear);
                moveTweener.onComplete += OnMoveComplete;                
            }
        }
    }

    private void OnMoveComplete()
    {
        if (targetList.Count == 0)
            return;
        var pos = targetList.Dequeue();
        float distance = (pos - model.transform.localPosition).magnitude;
        moveTweener = model.transform.DOLocalMove(pos, distance / PlayerProperty.RunSpeed).SetEase(Ease.Linear);
        moveTweener.onComplete += OnMoveComplete;    
    }

    protected void LookAt(float angle)
    {
        if (model != null)
        {
            model.transform.localEulerAngles = new Vector3(0f, angle);
        }
    }


    public void Async(WorldPlayerInfoVO vo)
    {
        //MoveTo(vo.pos);
        //LookAt(vo.dir.x);
        PlayMoveAnimation(vo.isMove);
    }
}