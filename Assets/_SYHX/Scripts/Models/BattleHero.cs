using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleHero : BattleCharacter
{
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI barrierText;
    public override void RefreshUI()
    {
        ShowHP();
        ShowBarrier();
    }
    public void ShowHP()
    {
        hpText.text = $"{this.currentHp}/{this.maxHp}";
    }
    public void ShowBarrier()
    {
        barrierText.text = $"护盾：{this.barrier}";
    }
    public override void ChildAwake()
    {
        RefreshUI();
    }

    public override void Death()
    {
        base.Death();
    }

}
