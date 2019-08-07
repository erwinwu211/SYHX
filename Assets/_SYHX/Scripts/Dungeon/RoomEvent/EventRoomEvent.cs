using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventRoomEvent : RoomEvent
{
    public EventGroupSource eventGroup;

    private List<EventSource> temp;
    public override void EventHappen()
    {
        base.EventHappen();
        //生成一个满足触发条件的事件
        temp = new List<EventSource>();
        foreach (EventSource es in eventGroup.eventList) temp.Add(es);

        // EventSource _event = eventGroup.GetEventSource();
        // Debug.Log("从group中获得了一个新事件"+_event.name);
        // if (EventManager.Ins.CheckCanHappen(_event))
        // {
        //     Debug.Log("将事件传入manager");
        //     EventManager.Ins.ReceiveEvent(_event,this);
        // }
        // else
        // {
        //     eventGroup.eventList.Remove(_event);
        //     if (eventGroup.eventList.Count == 0)
        //     {
        //         Debug.Log("池子已被清空了，没有事件了");
        //         Finished();
        //         return;
        //     }
        //     else
        //     {
        //         Debug.Log("池子还不为空，重新抽一个事件出来");
        //         EventHappen();
        //     }
        // }

        EventSource _event = GetVaildEvent();
        if (_event) EventManager.Ins.ReceiveEvent(_event,this);
    }

    private EventSource GetVaildEvent()
    {
        EventSource _event = eventGroup.GetEventSource();
        Debug.Log("从group中获得了一个新事件"+_event.name);
        if (EventManager.Ins.CheckCanHappen(_event))
        {
            Debug.Log("这个事件是可用的");
            //EventManager.Ins.ReceiveEvent(_event,this);
            return _event;
        }
        else
        {
            Debug.Log("但它不可用，所以把它移除了");
            temp.Remove(_event);
            if (temp.Count == 0)
            {
                Debug.Log("现在池子已被清空了，没有事件了");
                Finished();
                return null;
            }
            else
            {
                Debug.Log("池子还不为空，重新抽一个事件出来");
                return GetVaildEvent();
            }
        }
    }
}
