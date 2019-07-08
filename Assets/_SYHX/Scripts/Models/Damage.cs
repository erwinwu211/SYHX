using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Damage
{

    public static int CalculateAndGet(BattleCharacter giver, BattleCharacter receiver, float rate, DamageTrigger trigger)
    {
        return 0;
    }
    public static int CalculateAndApply(BattleCharacter giver, BattleCharacter receiver, float rate, DamageTrigger trigger)
    {
        var damage = giver.attack * rate;
        damage *= (1 + giver.attackRate);
        return giver.GiveDamage(receiver, (int)damage, trigger);
    }
}


public enum DamageTrigger
{
    ByCard, ByEnemy
}