using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardContent : SAssitant<CardSource>
{
    public CardContent() { }
    public override void SetOwner(CardSource owner)
    {
        this.owner = owner;
        this.EP = owner.EP;
        this.cardType = owner.cardType;
    }
    public CardType cardType { get; private set; }
    public int EP { get; private set; }
    public void OnDraw() => owner.OnDraw();
    public void OnUse() => owner.OnUse(this);
    public void OnFold() => owner.OnFold();
    public void OnExiled() => owner.OnExiled();
    public void OnOtherCardUse(CardSource context) => owner.OnOtherCardUse(context);
}
