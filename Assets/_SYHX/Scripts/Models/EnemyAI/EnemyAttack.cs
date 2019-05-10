using UnityEngine;

public class EnemyAttack : EnemyAction
{
    public float damageRate;
    public override void Execute()
    {
        Damage.CalculateAndApply(enemy, BattleCharacterManager.Ins.hero, damageRate, DamageTrigger.ByEnemy);
    }
}
