using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftRoomEvent : RoomEvent
{

    public override void EnterEvent()
    {
        base.EnterEvent();
        CraftManager.Ins.ReceiveEvent(this);
    }
}
