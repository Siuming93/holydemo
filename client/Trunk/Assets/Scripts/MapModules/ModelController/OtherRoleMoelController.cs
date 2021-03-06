﻿//using UnityEngine;

//public class OtherRoleMoelController : BaseModelController
//{
//    protected bool addListener = false;
//    protected bool isMove;
//    protected Vector2 moveDir;

//    public void Init(WorldRoleInfoVO roleInfo)
//    {
//        Async(roleInfo);
//    }

//    public void StartMove(int angle)
//    {
//        isMove = true;
//        moveDir = MathUtil.GetCoordinate(angle);
//        LookAt(angle);
//        PlayMoveAnimation(true);
//        if (!addListener)
//        {
//            addListener = true;
//            UpdateProxy.Instance.UpdateEvent += Update;
//        }
//    }

//    public void EndMove(WorldRoleInfoVO roleInfo)
//    {
//        isMove = false;
//        if (addListener)
//        {
//            UpdateProxy.Instance.UpdateEvent -= Update;
//            addListener = false;
//        }
//        MoveTo(new Vector3((float)roleInfo.posInfo.posX,0f, (float)roleInfo.posInfo.posY));
//        LookAt(roleInfo.posInfo.angle);
//        PlayMoveAnimation(false);
//    }

//    public void UpdateMoveDir(int angle)
//    {
//        moveDir = MathUtil.GetCoordinate(angle);
//        Debug.Log("angle:" + angle);
//        LookAt(angle);
//    }
//    protected void Update()
//    {
//        if (model != null)
//        {
//            if (isMove)
//            {
//                Vector3 delta = new Vector3(moveDir.x, 0f, moveDir.y) * RoleProperty.RunSpeed * Time.deltaTime;
//                Vector3 endPos = model.transform.localPosition + delta;
//                model.transform.localPosition = endPos;
//            }
//        }
//    }

//    public void MoveTo(Vector3 pos)
//    {
//        if (model != null)
//        {
//            model.transform.localPosition = pos;
//        }
//    }

//    public void Async(WorldRoleInfoVO vo)
//    {
//        LookAt(vo.posInfo.angle);
//        MoveTo(new Vector3((float)vo.posInfo.posX, 0f, (float)vo.posInfo.posY));
//    }
//}