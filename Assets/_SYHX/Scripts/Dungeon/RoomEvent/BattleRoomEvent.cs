using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRoomEvent : RoomEvent
{
    public List<EnemyGroup> EnemyGroup;

    public override void EnterEvent()
    {
        SceneStatusManager.Ins.SetSceneStatus(new BattleStatus(SceneStatusManager.Ins), true, true);
    }
}
