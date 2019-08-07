using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestRoomEvent : RoomEvent
{
    public ItemSource item;
    public override void EventHappen()
    {
        base.EventHappen();
        Debug.Log(item.count);
        Finished();
    }
}
