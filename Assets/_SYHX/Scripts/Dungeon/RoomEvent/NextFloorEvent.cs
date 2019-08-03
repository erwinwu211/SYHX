using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextFloorEvent : RoomEvent
{
    public override void EventHappen()
    {
        base.EventHappen();
        DungeonManager.Ins.GotoNextFloor();
        Finished();
    }


}
