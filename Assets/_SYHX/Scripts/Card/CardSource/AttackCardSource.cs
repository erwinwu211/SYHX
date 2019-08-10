using System.Collections;
using UnityEngine;
namespace SYHX.Cards

{
    [SourceName("攻击")]
    public class AttackCardSource : CardSource<AttackCardContent> { }

    [System.Serializable]
    public class AttackCardContent : CardContent
    {
        [Header("伤害率")] [CloneField] public float damageRate;
        [CustomDesc("damageRate")] public string dRateString { get => (damageRate * 100).ToString() + "%"; }
        [Header("目标选择类型")] [CloneField] public AttackTargetType targetType;
        [Header("伤害次数")] [CloneField] public int attackFrequency = 1;
        [CustomDesc("frequency")] public string dFrequencyString { get => attackFrequency + ""; }


        protected override IEnumerator UseEffect(CardUseTrigger trigger)
        {
            for (int i = 0; i < attackFrequency; i++)
            {
                // switch (targetType)
                // {

                //     case AttackTargetType.mark:
                //         {
                //             Enemy temp = BattleManager.selectedEnemy;
                //             Damage.CalculateAndApply(BattleManager.Hero, BattleManager.selectedEnemy, damageRate, DamageTrigger.ByCard);
                //             if (temp.IsAlive() == false) i = 999;
                //             break;
                //         }
                //     case AttackTargetType.aoe:
                //         for (int j = 0; j < BattleManager.enemyList.Count; j++)
                //         {
                //             Enemy temp = BattleManager.enemyList[j];
                //             Damage.CalculateAndApply(BattleManager.Hero, temp, damageRate, DamageTrigger.ByCard);
                //             if (temp.IsAlive() == false) j--;
                //         }
                //         break;
                //     case AttackTargetType.random:
                //         int r = Random.Range(0, BattleManager.enemyList.Count);
                //         Debug.Log("随机出了" + r);
                //         Damage.CalculateAndApply(BattleManager.Hero, BattleManager.enemyList[r], damageRate, DamageTrigger.ByCard);
                //         break;
                // }
                if (targetType == AttackTargetType.mark)
                {
                    Enemy temp = BattleManager.selectedEnemy;
                    Damage.CalculateAndApply(BattleManager.Hero, BattleManager.selectedEnemy, damageRate, DamageTrigger.ByCard);
                    if (temp.IsAlive() == false) i = 999;
                    break;
                }
                else if (targetType == AttackTargetType.aoe)
                {
                    for (int j = 0; j < BattleManager.enemyList.Count; j++)
                    {
                        Enemy temp = BattleManager.enemyList[j];
                        Damage.CalculateAndApply(BattleManager.Hero, temp, damageRate, DamageTrigger.ByCard);
                        if (temp.IsAlive() == false) j--;
                    }
                    break;
                }
                else if (targetType == AttackTargetType.random)
                {
                    int r = Random.Range(0, BattleManager.enemyList.Count);
                    Debug.Log("随机出了" + r);
                    Damage.CalculateAndApply(BattleManager.Hero, BattleManager.enemyList[r], damageRate, DamageTrigger.ByCard);
                    break;
                }
                // if (targetType == AttackTargetType.aoe)
                // {
                //     foreach (var enemy in BattleManager.enemyList)
                //     {
                //         Damage.CalculateAndApply(BattleManager.Hero, enemy, damageRate, DamageTrigger.ByCard);
                //     }
                // }

                // else
                // {
                //     Damage.CalculateAndApply(BattleManager.Hero, BattleManager.selectedEnemy, damageRate, DamageTrigger.ByCard);
                // }
            }

            yield break;
        }
    }

}

