using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EnemyAttack : EnemyAction
{
    public float damageRate;

    protected override IEnumerator execute()
    {
        Damage.CalculateAndApply(enemy, BattleCharacterManager.Ins.hero, damageRate, DamageTrigger.ByEnemy);
        yield return new WaitForSeconds(1f);
        BattleManager.enemyOnGoing = false;
        yield break;
    }
}
