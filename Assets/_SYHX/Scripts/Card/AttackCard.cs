using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCard : CardSource
{
    public int damage;
    public override void OnUse()
    {
        BattleManager.Ins.hero.GiveDamage(BattleManager.Ins.selectedEnemy, damage);
    }
}
