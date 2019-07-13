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
        var trans = receiver.damageTrans;
        var damage = giver.GiveDamage(receiver, rate, trigger);
        var damageSource = GameObject.Instantiate(Initializer.Ins.damageSource, trans);
        damageSource.SetText(damage.ToString());
        return damage;
    }
}


public enum DamageTrigger
{
    ByCard, ByEnemy
}