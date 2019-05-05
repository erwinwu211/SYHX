using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    // public int
    public BattleCharacter giver { get; private set; }
    public BattleCharacter receiver { get; private set; }
    public Damage(BattleCharacter giver, BattleCharacter receiver)
    {

    }

    public static int CalculateAndGet(BattleCharacter giver, BattleCharacter receiver, float value)
    {
        return 0;
    }
    public static void CalculateAndApply()
    {

    }
}


public enum DamageTrigger
{
    ByCard, ByEnemy
}