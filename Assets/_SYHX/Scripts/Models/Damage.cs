using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Damage
{

    public static int CalculateAndGet(BattleCharacter giver, BattleCharacter receiver, float rate, DamageTrigger trigger)
    {
        return 0;
    }
    public static void CalculateAndApply(BattleCharacter giver, BattleCharacter receiver, float rate, DamageTrigger trigger, int times = 1)
    {
        BattleProgressEvent.Ins.OnGiveDamage(giver, receiver, trigger);
        var damage = giver.attack * rate;
        damage *= (1 + giver.attackRate);
        for (int i = 0; i < times; i++)
        {
            BattleProgressEvent.Ins.OnGiveDamagePertime(giver, receiver, trigger);
            giver.GiveDamage(receiver, (int)damage, trigger);
            BattleProgressEvent.Ins.OnReceiveDamagePertime(giver, receiver, trigger);
        }
        BattleProgressEvent.Ins.OnReceiveDamage(giver, receiver, trigger);
    }
}


public enum DamageTrigger
{
    ByCard, ByEnemy
}