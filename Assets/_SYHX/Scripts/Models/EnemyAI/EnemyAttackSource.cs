using UnityEngine;
using System.Collections;

public class EnemyAttackSource : EnemyActionSource<EnemyAttackContent> { }

[System.Serializable]
public class EnemyAttackContent : EnemyActionContent
{
    [CloneField] public float damageRate;
    [CustomDesc("damageRate")] public string damage { get => (damageRate * 100).ToString() + "%"; }

    protected override IEnumerator execute()
    {
        Damage.CalculateAndApply(enemy, BattleCharacterManager.Ins.hero, damageRate, DamageTrigger.ByEnemy);
        yield return new WaitForSeconds(1f);
        yield break;
    }
}
