using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterContent : SAssitant<CharacterSource>
{
    public string Name { get; protected set; }
    public int HpMax { get; protected set; }
    public int Attack { get; protected set; }
    public int Defend { get; protected set; }
    public int DrawCount { get; protected set; }
    public int Energy { get; protected set; }
    public List<CardContent> Cards { get; protected set; }
    public List<Talent> Talents { get; protected set; }

    public CharacterContent() { }
    public override void SetOwner(CharacterSource owner)
    {
        this.owner = owner;
        this.HpMax = owner.HpMax;
        this.Attack = owner.Attack;
        this.Defend = owner.Defend;
        this.DrawCount = owner.DrawCount;
        this.Energy = owner.Energy;
        foreach(CardSource cs in owner.Cards)
        {
            var cc = new CardContent();
            cc.SetOwner(cs);
            Cards.Add(cc);
        }
        foreach (Talent t in Talents)
        {
            t.SetOwner(this);
            t.DoEffect();
        }
    }


}
