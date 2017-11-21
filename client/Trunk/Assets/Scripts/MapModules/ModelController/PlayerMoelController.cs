using System.Collections;
using DG.Tweening;
using Monster.Net;
using Monster.Protocol;
using UnityEngine;

public class PlayerMoelController : BaseModelController
{
    protected IEnumerator _asyncItor;

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
    public void StartMove(Vector2 dir)
    {
        isMove = true;
        moveDir = dir;
        if (!addListener)
        {
            _asyncItor = AsyncMoveState();
            addListener = true;
            UpdateProxy.Instance.UpdateEvent += Update;
            UpdateProxy.Instance.StartCoroutine(_asyncItor);
        }
    }
    public void EndMove()
    {
        isMove = false;
        if (addListener)
        {
            UpdateProxy.Instance.StopCoroutine(_asyncItor);
            UpdateProxy.Instance.UpdateEvent -= Update;
            addListener = false;
        }
    }

    private Vector3 _lastPos;
    private Vector3 _lastEuler;
    private IEnumerator AsyncMoveState()
    {
        WaitForSeconds waitor = new WaitForSeconds(1/10f);
        while (true)
        {
           // Debug.Log("AsyncMoveState");
            Vector3 pos = model.transform.localPosition;
            Vector3 eular = model.transform.localEulerAngles;
            if (!Mathf.Approximately(pos.x, _lastPos.x) || !Mathf.Approximately(pos.y, _lastPos.y) ||
                !Mathf.Approximately(pos.z, _lastPos.z)
                || !Mathf.Approximately(eular.x, _lastEuler.x) || !Mathf.Approximately(eular.y, _lastEuler.y) ||
                !Mathf.Approximately(eular.z, _lastEuler.z))
            {
                _lastPos = pos;
                _lastEuler = eular;
               /* NetManager.Instance.SendMessage(new CsAsyncPlayerPos()
                {
                    posInfo = new PosInfo() { dirX = eular.y, dirY = eular.y, posX =  pos.x, posY = pos.z},
                });*/
            }
            yield return waitor;
        }
    }

    public void UpdateMoveDir(Vector2 dir)
    {
        moveDir = dir;
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

    public void LookAt(Vector2 dir)
    {
        if (model != null)
        {
            Vector3 endPos = model.transform.localPosition + new Vector3(dir.x, 0, dir.y);
            Vector3 worldPos = endPos;
            model.transform.LookAt(worldPos, Vector3.up);
        }
    }
    #endregion
}

public class OtherPlayerMoelController : BaseModelController
{
    protected Tweener moveTweener;
    public void MoveTo(Vector3 pos)
    {
        if (model != null)
        {
            float distance = (pos - model.transform.localPosition).magnitude;
            //Debug.Log(distance);
            if (moveTweener != null && moveTweener.IsPlaying())
                moveTweener.Kill();
            moveTweener = model.transform.DOLocalMove(pos, distance / PlayerProperty.RunSpeed).SetEase(Ease.Linear);
        }
    }
    public void LookAt(float angle)
    {
        if (model != null)
        {
            model.transform.localEulerAngles = new Vector3(0f, angle);
        }
    }

    public void Async(WorldPlayerInfoVO vo)
    {
        MoveTo(vo.pos);
        LookAt(vo.dir.x);
    }
}