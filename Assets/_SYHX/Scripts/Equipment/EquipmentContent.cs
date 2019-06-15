using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentContent : SAssitant<EquipmentSource>
{
    public EquipmentContent() { }
    public override void SetOwner(EquipmentSource owner)
    {
        this.owner = owner;
        this.Attack = owner.Attack;
        this.Hp = owner.Hp;
        this.Defend = owner.Defend;
    }

    public int Attack { get; private set; }
    public int Hp { get; private set; }
    public int Defend { get; private set; }
    public List<CardContent> Cards()
    {
        List<CardContent> res = new List<CardContent>();
        foreach (CardSource card in owner.Cards)
        {
            // var cc = new CardContent();
            // cc.SetOwner(card);
            // res.Add(cc);
        }
        return res;
    }
}