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
        [Header("群伤？")] [CloneField] public bool isAOE;
        [CustomDesc("damageRate")] public string dRateString { get => (damageRate * 100).ToString() + "%"; }
        protected override IEnumerator UseEffect(CardUseTrigger trigger)
        {
            if (isAOE)
            {
                foreach (var enemy in BattleManager.enemyList)
                {
                    Damage.CalculateAndApply(BattleManager.Hero, enemy, damageRate, DamageTrigger.ByCard);
                }
            }
            else
            {
                Damage.CalculateAndApply(BattleManager.Hero, BattleManager.selectedEnemy, damageRate, DamageTrigger.ByCard);
            }
            yield break;
        }
    }

}

