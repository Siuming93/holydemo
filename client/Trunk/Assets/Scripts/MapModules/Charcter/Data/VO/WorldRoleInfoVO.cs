using RedDragon.Protocol;
using UnityEngine;

public class WorldRoleInfoVO
{
    public long id;
    public ModelPosVO posInfo = new ModelPosVO();
    public bool isMove;
    public double time;
    public void Update(PlayerPosInfo info)
    {
        this.id = info.Id;
        this.posInfo.posX = info.PosInfo.PosX;
        //this.posInfo.posY = info.posInfo.posY;
        //this.posInfo.angle = info.posInfo.angle;
        this.isMove = info.IsMove;
    }
    public void Update(PosInfo pos)
    {
        this.posInfo.posX = pos.PosX;
        //this.posInfo.posY = pos.posY;
        //this.posInfo.angle = pos.angle;
    }

    public void Update(PosInfo pos, bool isMove, double time)
    {
        this.posInfo.posX = pos.PosX;
        //this.posInfo.posY = pos.posY;
        //this.posInfo.angle = pos.angle;
        this.isMove = isMove;
        this.time = time;
    }

    public void Update(PosInfo pos, bool isMove)
    {
        this.posInfo.posX = pos.PosX;
        //this.posInfo.posY = pos.posY;
        //this.posInfo.angle = pos.angle;
        this.isMove = isMove;
    }

    public void Update(float posX, float posY, float angle)
    {
        this.posInfo.posX = posX;
        this.posInfo.posY = posY;
        this.posInfo.angle = angle;
    }

    public void UpdateDir(float angle)
    {
        this.posInfo.angle = angle;
    }
}

public class ModelPosVO
{
    public double posX;
    public double posY;
    public double angle;
}
