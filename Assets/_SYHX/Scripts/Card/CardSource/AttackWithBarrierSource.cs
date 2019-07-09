using System.Collections;
using UnityEngine;
namespace SYHX.Cards

{
    [SourceName("攻击防御")]
    public class AttackWithBarrierSource : CardSource<AttackWithBarrierContent> { }

    [System.Serializable]
    public class AttackWithBarrierContent : CardContent
    {
        [Header("伤害率")] [CloneField] public float damageRate;
        [Header("群伤？")] [CloneField] public bool isAOE;
        [CustomDesc("damageRate")] public string dRateString { get => (damageRate * 100).ToString() + "%"; }
        protected override IEnumerator UseEffect(CardUseTrigger trigger)
        {
            int defence = 0;
            if (isAOE)
            {
                foreach (var enemy in BattleManager.enemyList)
                {
                    defence += Damage.CalculateAndApply(BattleManager.Hero, enemy, damageRate, DamageTrigger.ByCard);
                }
            }
            else
            {
                defence += Damage.CalculateAndApply(BattleManager.Hero, BattleManager.selectedEnemy, damageRate, DamageTrigger.ByCard);
            }
            BattleManager.Hero.GetBarrier(defence);
            yield break;
        }
    }

}

