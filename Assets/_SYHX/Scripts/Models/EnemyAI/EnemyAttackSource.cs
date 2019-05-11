using UnityEngine;
using System.Collections;

public class EnemyAttackSource : EnemyActionSource
{
    public float damageRate;
    public override EnemyAction GenerateAction()
    {
        return new EnemyAttack(damageRate);
    }
}

public class EnemyAttack : EnemyAction
{
    private float damageRate;
    public EnemyAttack(float damageRate)
    {
        this.damageRate = damageRate;
    }
    public override IEnumerator Execute()
    {
        Damage.CalculateAndApply(enemy, BattleCharacterManager.Ins.hero, damageRate, DamageTrigger.ByEnemy);
        yield break;
    }
}