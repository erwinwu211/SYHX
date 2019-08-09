using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomEvent : MonoBehaviour
{
    public GameObject Front;
    public GameObject Back;
    public int happenedCount = 0; //该房间的该事件已经触发了的次数
    public int maxHappenCount = 1; //该房间的该事件最多可以触发的次数

    /// <summary>
    /// 初始化
    /// </summary>
    void Start()
    {
        Front.SetActive(false);
        Back.SetActive(true);
    }

    
    /// <summary>
    /// 进入房间时触发的事件，返回是否已超过事件发生次数
    /// </summary>
    public virtual void EnterEvent()
    {
        DungeonManager.Ins.currentEvent = this;
        DungeonManager.Ins.Disable();
        if (happenedCount >= maxHappenCount)
        {
            Debug.Log("超过事件发生次数");
            Finished();
            return;
        }
        happenedCount += 1;
        EventHappen();
    }

    public virtual void EventHappen() {}

    /// <summary>
    /// 在事件结束后，恢复地宫界面的鼠标接收
    /// </summary>
    public virtual void Finished()
    {
        DungeonManager.Ins.Enable();
        DungeonManager.Ins.currentEvent = null;
    }

    /// <summary>
    /// 离开房间时触发的事件
    /// </summary>
    public virtual void LeaveEvent()
    {
    }

    /// <summary>
    /// 被探测到时触发的事件
    /// </summary>
    public virtual void OnDetected()
    {
        Front.SetActive(true);
        Back.SetActive(false);
    }

}
