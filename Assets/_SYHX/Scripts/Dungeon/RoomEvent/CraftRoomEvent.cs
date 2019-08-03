using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftRoomEvent : RoomEvent
{
    public override void EventHappen()
    {
        base.EventHappen();
        CraftManager.Ins.ReceiveEvent(this);
    }
}
