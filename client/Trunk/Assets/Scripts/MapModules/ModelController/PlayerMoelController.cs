using UnityEngine;

public class PlayerMoelController : BaseModelController
{

}

public class OtherPlayerMoelController : BaseModelController
{
    public void Async(WorldPlayerInfoVO vo)
    {
        //model.transform.position = vo.pos;
        UpdateMoveDir(vo.dir);
        LookAt(vo.dir);
        if (vo.isMove && !isMove)
        {
            StartMove(vo.dir);
        }
        if (!vo.isMove && isMove)
        {
            EndMove();
        }
    }
}