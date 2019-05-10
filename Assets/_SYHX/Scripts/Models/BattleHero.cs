using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleHero : BattleCharacter
{
    public TextMeshProUGUI text;
    public void ShowHp()
    {
        text.text = $"{this.currentHp}/{this.maxHp}";
    }
    public override void ChildAwake()
    {
        ShowHp();
    }
    public override void TakeDamage(BattleCharacter bc, int damage)
    {
        base.TakeDamage(bc, damage);
        ShowHp();
    }

    public override void Death()
    {
        base.Death();
    }

}
