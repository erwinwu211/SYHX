using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAttrOnBattleEnd : Talent
{
    private int counter;
    public override void OnActive()
    {
        counter = 0;
    }
    public override void OnLeaveRoom(RoomEvent _event)
    {
        if (_event.roomType == RoomType.battle_normal||_event.roomType == RoomType.battle_elite||_event.roomType == RoomType.battle_boss)
        {
            counter++;
            if (counter >=3) 
            {
                CharacterInDungeon.Ins.IncreaseAttack(1);
                counter = 0;
            }
        }
    }
    
}
