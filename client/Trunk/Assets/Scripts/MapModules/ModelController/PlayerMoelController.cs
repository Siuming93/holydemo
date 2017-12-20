using System.Collections;
using DG.Tweening;
using Monster.Net;
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
    public void StartMove(int angle)
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

    public void UpdateMoveDir(int angle)
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
                Vector3 delta = new Vector3(moveDir.x, 0f, moveDir.y) * RoleProperty.RunSpeed * Time.deltaTime;
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
            moveTweener = model.transform.DOLocalMove(pos, distance / RoleProperty.RunSpeed).SetEase(Ease.Linear);
        }
    }
    #endregion
}

