using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventRoomEvent_10001 : RoomEvent
{
    public override void EventHappen()
    {
        base.EventHappen();
        Finished();
    }
}
