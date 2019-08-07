using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGroupSource : ScriptableObject
{
    public List<EventSource> eventList;
    public EventSource GetEventSource()
    {
        if (eventList.Count > 0)
        {
            return eventList[Random.Range(0, eventList.Count - 1)];
        }
        else
        {
            return null;
        }
    }
}
