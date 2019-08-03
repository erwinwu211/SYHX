using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestRoomEvent : RoomEvent
{
    public override void EnterEvent()
    {
        base.EnterEvent();
        Finished();
    }
}
