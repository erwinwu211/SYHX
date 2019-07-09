using System.Collections;
using UnityEngine;
namespace SYHX.Cards

{
    [SourceName("剑柄打击")]
    public class AttackWithDrawCardSource : CardSource<AttackWithDrawCardContent> { }

    [System.Serializable]
    public class AttackWithDrawCardContent : CardContent
    {
        [Header("伤害率")] [CloneField] public float damageRate;
        [Header("群伤？")] [CloneField] public bool isAOE;
        [Header("抽牌数量")] [CloneField] public int drawNumber;

        [CustomDesc("damageRate")] public string dRateString { get => (damageRate * 100).ToString() + "%"; }
        [CustomDesc("drawNumber")] public string dDraw { get => drawNumber.ToString(); }
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
            BattleManager.sDraw(drawNumber);
            yield break;
        }
    }

}

