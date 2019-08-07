using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : SingletonMonoBehaviour<EventManager>
{
    protected override void UnityAwake() { }
    private EventSource _event;
    private RoomEvent context;

    private List<int> floorEventIDList = new List<int>();
    private List<int> permanentEventIDList = new List<int>();
    public EventUI mUI;

    /// <summary>
    /// 接受一个事件，并将其注册进事件簿中
    /// </summary>
    /// <param name="e"></param>
    public void ReceiveEvent(EventSource e, RoomEvent context)
    {
        this._event = e;
        this.context = context;
        if (e.floorCount != 0) floorEventIDList.Add(e.id);
        if (e.permanentCount != 0) permanentEventIDList.Add(e.id);
        mUI.ShowEventUI(e);
    }

    /// <summary>
    /// 释放一个事件
    /// </summary>
    public void ReleaseEvent()
    {
        this._event = null;
        this.context.Finished();
        mUI.EnhanceEventUI();
    }

    /// <summary>
    /// 查询一个事件是否满足触发条件
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public bool CheckCanHappen(EventSource e) => CheckInFloorList(e) && CheckInPermanentList(e);

    /// <summary>
    /// 查询一个事件是否已在本层中触发过，返回true表示该事件可以触发
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public bool CheckInFloorList(EventSource e)
    {
        if (e.floorCount == 0) return true;
        int repeatNum = 0;
        foreach (int v in floorEventIDList)
        {
            if (v == e.id) repeatNum++;
        }
        return (repeatNum < e.floorCount);
    }

    /// <summary>
    /// 查询一个事件是否已在本次地宫探索中触发过，返回true表示该事件可以触发
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public bool CheckInPermanentList(EventSource e)
    {
        if (e.permanentCount == 0) return true;
        int repeatNum = 0;
        foreach (int v in permanentEventIDList)
        {
            if (v == e.id) repeatNum++;
        }
        return (repeatNum < e.permanentCount);
    }

    public void ClearFloorList()
    {
        floorEventIDList.Clear();
    }

    public void ClearPermanentList()
    {
        permanentEventIDList.Clear();
    }

    private class RepeatInfo
    {
        //值
        public int Value { get; set; }
        //重复次数
        public int RepeatNum { get; set; }
    }
}
