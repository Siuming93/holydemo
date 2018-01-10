//using System.Collections;
//using DG.Tweening;
//using Monster.Net;
//using UnityEngine;
//using System.Collections.Generic;
//using Monster.BaseSystem;

//public class PlayerMoelController : BaseModelController
//{
//    public Transform modelTransform { get { return model.transform; }}

//    #region move
//    public void Move(Vector2 delta)
//    {
//        if (model != null)
//        {
//            Vector3 endPos = model.transform.localPosition + new Vector3(delta.x, 0, delta.y);
//            Vector3 worldPos = model.transform.localToWorldMatrix * endPos;
//            model.transform.localPosition = endPos;
//            model.transform.LookAt(worldPos, Vector3.up);
//        }
//    }

//    protected bool addListener = false;
//    public void StartMove()
//    {
//        isMove = true;
//        PlayMoveAnimation(true);
//        if (!addListener)
//        {
//            addListener = true;
//            UpdateProxy.Instance.UpdateEvent += Update;
//        }
//    }
//    public void EndMove()
//    {
//        isMove = false;
//        if (addListener)
//        {
//            UpdateProxy.Instance.UpdateEvent -= Update;
//            addListener = false;
//        }
//        PlayMoveAnimation(false);
//    }

//    public void UpdateMoveDir(int angle)
//    {
//        moveDir = MathUtil.GetCoordinate(angle);
//        LookAt(angle);
//    }

//    protected bool isMove;
//    protected Vector2 moveDir;
//    private float _lastTickTime;
//    protected void Update()
//    {
//        if (model != null)
//        {
//            if (isMove)
//            {
//                float distance = RoleProperty.RunSpeed * (Time.deltaTime);
//                float dx = distance*cos;
//                float dz = distance*sin;
//                Vector3 delta = new Vector3(dx, 0f, dz);
//                Vector3 endPos = model.transform.localPosition + delta;
//                model.transform.localPosition = endPos;
//            }
//        }
//    }

//    protected Tweener moveTweener;
//    public void MoveTo(double x, double y)
//    {
//        if (model != null)
//        {
//            //Debug.Log("model Pos:" + model.transform.localPosition + " async Pos:" + new Vector3((float)x, 0, (float)y));
//            model.transform.position = new Vector3((float)x, model.transform.position.y, (float)y);
//        }
//    }

//    public void ForceAsync(WorldRoleInfoVO vo)
//    {
//        MoveTo(vo.posInfo.posX, vo.posInfo.posY);
//    }
//    #endregion
//}

