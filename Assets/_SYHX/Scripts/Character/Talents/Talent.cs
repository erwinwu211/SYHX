using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Talent : ScriptableObject
{
    public Talent() { }

    //是否初始就已经解锁
    public bool IsUnlock;
    public TalentGroup Owner;
    public string Name;
    public string Desc;
    public Sprite Icon;
    public TalentEventType type;


    public virtual void OnActive()
    {
        if (type == TalentEventType.Active) DoEffect();
    }

    public virtual void OnEnterRoom(RoomEvent _event)
    {
        if (type == TalentEventType.EnterRoom) DoEffect(_event);
    }
    public virtual void OnLeaveRoom(RoomEvent _event)
    {
        if (type == TalentEventType.LeaveRoom) DoEffect(_event);
    }
    public virtual void OnBattleEnd()
    {
        if (type == TalentEventType.BattleEnd) DoEffect();
    }
    public virtual void OnRest()
    {
        if (type == TalentEventType.Rest) DoEffect();
    }

    public virtual void DoEffect()
    {
        Debug.Log(Name + "天赋触发");

    }

    public virtual void DoEffect(RoomEvent _event)
    {
        Debug.Log(Name + "天赋触发");
    }

}

public enum TalentEventType
{
    Active,
    EnterRoom,
    LeaveRoom,
    BattleEnd,
    Rest,
}
