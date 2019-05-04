using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCard : CardSource
{
    public int damage;
    public bool isAOE;
    public override void Effect(CardContent cc)
    {
        if (isAOE)
        {
            foreach (var enemy in BattleManager.Ins.enemyList)
            {
                BattleManager.Ins.hero.GiveDamage(enemy, damage);
            }
        }
        else
        {
            BattleManager.Ins.hero.GiveDamage(BattleManager.Ins.selectedEnemy, damage);
        }
    }
}
