using System.Collections;
using UnityEngine;
namespace SYHX.Cards

{
    [SourceName("攻击")]
    public class AttackWithGenerateCardSource : CardSource<AttackWithGenerateCardContent> { }

    [System.Serializable]
    public class AttackWithGenerateCardContent : CardContent
    {
        [Header("伤害率")] [CloneField] public float damageRate;
        [Header("群伤？")] [CloneField] public bool isAOE;
        [Header("生成卡牌")] [CloneField] public CardSource cardName;
        [CustomDesc("damageRate")] public string dRateString { get => (damageRate * 100).ToString() + "%"; }
        [CustomDesc("cardName")] public string dCard { get => cardName.Name; }
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
            var card = cardName.GenerateCard();
            BattleCardManager.Ins.GenerateCardTo(card, CardPosition.Hand);
            yield break;
        }
    }

}

