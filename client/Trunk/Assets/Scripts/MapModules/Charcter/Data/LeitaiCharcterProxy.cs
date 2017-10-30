using System.Collections.Generic;
using Monster.Protocol;
using UnityEngine;

public class LeitaiCharcterProxy : BaseProxy
{
    public new const string NAME = "LeitaiCharcterProxy";

    public List<WorldPlayerInfoVO> otherPlayerList = new List<WorldPlayerInfoVO>();

    private Dictionary<long, WorldPlayerInfoVO> map = new Dictionary<long, WorldPlayerInfoVO>();
    public WorldPlayerInfoVO playerInfo { get; private set; }

    public LeitaiCharcterProxy()
        : base(NAME)
    {
        RegisterMessageHandler(MsgIDDefine.ScAllPlayerPosInfo, OnGetAllPlayerPosInfo);
        RegisterMessageHandler(MsgIDDefine.ScPlayerEndMove, OnPlayerEndMove);
        RegisterMessageHandler(MsgIDDefine.ScPlayerUpdateMoveDir, OnPlayerUpdateMoveDir);
        RegisterMessageHandler(MsgIDDefine.ScPlayerStartMove, OnPlayerStartMove);
        playerInfo = new WorldPlayerInfoVO();
    }

    private void OnGetAllPlayerPosInfo(object msg)
    {
        otherPlayerList.Clear();
        ScAllPlayerPosInfo allInfo = msg as ScAllPlayerPosInfo;
        foreach (var playerPosInfo in allInfo.infos)
        {
            if (playerPosInfo.id == PlayerProperty.ID)
            {
                playerInfo.Update(playerPosInfo);
                PlayerProperty.Postion = new Vector2(playerPosInfo.posX, playerPosInfo.posY);
            }
            else
            {
                WorldPlayerInfoVO vo;
                if (!map.ContainsKey(playerPosInfo.id))
                {
                    vo = new WorldPlayerInfoVO();
                    map.Add(playerPosInfo.id, vo);
                }
                vo = map[playerPosInfo.id];
                vo.Update(playerPosInfo);
                otherPlayerList.Add(vo);
            }
        }

        SendNotification(NotificationConst.ALL_OTHER_PLAYER_INFO_UPDATE);
    }

    private void OnPlayerEndMove(object data)
    {
        SendNotification(NotificationConst.PLAYER_END_MOVE);
    }

    private void OnPlayerStartMove(object data)
    {
        var msg = data as ScPlayerStartMove;
        playerInfo.Update(msg.posX, msg.posY, msg.dirX, msg.dirY);

        SendNotification(NotificationConst.PLAYER_START_MOVE);
    }

    private void OnPlayerUpdateMoveDir(object data)
    {
        var msg = data as ScPlayerUpdateMoveDir;
        playerInfo.UpdateDir(msg.dirX, msg.dirY);

        SendNotification(NotificationConst.PLAYER_UPDATE_MOVE_DIR);
    }
}
