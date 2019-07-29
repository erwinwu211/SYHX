using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextFloorEvent : RoomEvent
{
    public override void EnterEvent()
    {
        DungeonManager.Ins.GotoNextFloor();
    }


}
