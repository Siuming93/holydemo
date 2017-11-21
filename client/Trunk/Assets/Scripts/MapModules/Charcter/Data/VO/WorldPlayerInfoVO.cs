using Monster.Protocol;
using UnityEngine;

public class WorldPlayerInfoVO
{
    public long id;
    public Vector3 pos;
    public Vector2 dir;
    public bool isMove;
    public void Update(PlayerPosInfo info)
    {
        this.id = info.id;
        this.pos = new Vector3(info.posInfo.posX, 0f, info.posInfo.posY);
        this.dir = new Vector2(info.posInfo.dirX, info.posInfo.dirY);
        this.isMove = info.isMove;
    }

    public void Update(float posX, float posY, float dirX, float dirY)
    {
        this.pos = new Vector3(posX, 0f, posY);
        this.dir = new Vector2(dirX, dirY);
    }

    public void UpdateDir(float dirX, float dirY)
    {
        this.dir = new Vector2(dirX, dirY);
    }
}
