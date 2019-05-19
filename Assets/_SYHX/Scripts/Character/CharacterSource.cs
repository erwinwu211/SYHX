using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterSource
{
    public string Name { get; protected set; }
    public int HpMax { get; protected set; }
    public int Attack { get; protected set; }
    public int Defend { get; protected set; }
    public int DrawCount { get; protected set; }
    public int Energy { get; protected set; }
    public List<CardSource> Cards { get; protected set; }
    public List<Talent> Talents { get; protected set; }

    public virtual CharacterContent GenerateCharacter()
    {
        var cc = new CharacterContent();
        cc.SetOwner(this);
        return cc;
    }

    public void EffectTalent(Talent tl)
    {
        if (Talents.Contains(tl))
        {
            tl.IsEffect = true;
        }
    }
}

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
    }
}
