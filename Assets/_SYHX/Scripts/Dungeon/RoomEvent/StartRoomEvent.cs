using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoomEvent : RoomEvent
{
    public BattleRoomScript RoomScript;

    //用来标记是否已经完成探测的flag
    private bool flag = true;

    private void Start()
    {
        OnDetected();
    }

    private void Update()
    {
        if (RoomScript.hasLoaded && flag)
        {
            Debug.Log("房间信息已加载完成，开始探测周围房间");
            RoomScript.DetectNearby();
            flag = false;
        }
    }

    public override void EventHappen()
    {
        Finished();
    }
}
