using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalRoomEvent : RoomEvent
{
    public override void EventHappen()
    {
        base.EventHappen();
        Finished();
    }
}
