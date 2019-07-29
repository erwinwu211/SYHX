using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoomEvent : RoomEvent
{
    public BattleRoomScript RoomScript;
    private bool flag = true;

    private void Start()
    {
        OnDetected();
    }

    private void Update()
    {
        if (RoomScript.hasLoaded && flag)
        {
            RoomScript.DetectNearby();
            flag = false;
        }
    }

    public override void EnterEvent()
    {
    }
}
