using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FukasakiKotone : CharacterSource
{
    public FukasakiKotone()
    {
        Name = "深崎琴音";
        HpMax = 70;
        Attack = 6;
        Defend = 5;
        DrawCount = 5;
        Energy = 3;
        Cards = new List<CardSource>();
        Talents = new List<Talent>();
        Talents.Add(new Talent_00001());
        Talents[0].IsEffect = true;
    }

    public override CharacterContent GenerateCharacter()
    {
        return base.GenerateCharacter();
    }
}
