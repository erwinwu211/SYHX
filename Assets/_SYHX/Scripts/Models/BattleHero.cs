using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BattleHero : BattleCharacter
{
    public override bool isEnemy { get => false; }
    public Text hpText;
    public Text barrierText;
    public Slider hpSlider;
    public int Force;
    public int Align;
    public int Constitution;
    public override void RefreshUI()
    {
        ShowHP();
        ShowBarrier();
        ShowStatus();
    }
    public void ShowHP()
    {
        hpText.text = $"{this.currentHp}/{this.maxHp}";
        hpSlider.value = (float)currentHp / maxHp;
    }
    public void ShowBarrier()
    {
        barrierText.text = $"{this.barrier}";
    }
    public override void ChildStart()
    {
        RefreshUI();
    }

    public override void Death()
    {
        base.Death();
    }

    //攻击 = 角色攻击力 * 属性修正 * 卡牌百分比 * 我方buff属性 
    public override int GiveDamage(BattleCharacter bc, float damageRate, DamageTrigger trigger)
    {
        var factorRate = Initializer.Ins.factors[Force - 1].Force;
        return bc.TakeDamage(this, Attack * damageRate * factorRate * (1 + attackRate));
    }

}
