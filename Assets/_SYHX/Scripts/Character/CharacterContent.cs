using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterContent : SAssitant<CharacterSource>
{
    public string Name { get; protected set; }
    public int HpMax { get; set; }
    public int Attack { get; set; }
    public int Defend { get; set; }
    public int DrawCount { get; set; }
    public int Energy { get; set; }
    public List<CardContent> Cards { get; set; }
    public List<Talent> Talents { get; protected set; }

    public CharacterContent() { }
    public override void SetOwner(CharacterSource owner)
    {
        this.owner = owner;
        this.Name = owner.Name;
        this.HpMax = owner.HpMax;
        this.Attack = owner.Attack;
        this.Defend = owner.Defend;
        this.DrawCount = owner.DrawCount;
        this.Energy = owner.Energy;
        Cards = new List<CardContent>();
        Talents = new List<Talent>();
        foreach (CardSource cs in owner.Cards)
        {
            // // var cc = new CardContent();
            // cc.SetOwner(cs);
            // Cards.Add(cc);
        }
        foreach (Talent t in owner.Talents)
        {
            Talents.Add(t);
            t.SetOwner(this);
            t.DoEffect();
        }
    }
}
