﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRoomEvent : RoomEvent
{
    public List<EnemyGroup> EnemyGroup;

    public override void EventHappen()
    {
        base.EventHappen();
        int index = Random.Range(0, EnemyGroup.Count - 1);
        DungeonManager.Ins.BattleHappen(EnemyGroup[index]);
    }
}
