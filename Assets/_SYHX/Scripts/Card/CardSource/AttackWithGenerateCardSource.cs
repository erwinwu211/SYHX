using System.Collections;
namespace SYHX.Cards

{
    [SourceName("攻击")]
    public class AttackWithGenerateCardSource : CardSource<AttackWithGenerateCardContent> { }

    [System.Serializable]
    public class AttackWithGenerateCardContent : CardContent
    {
        [CloneField] public float damageRate;
        [CloneField] public bool isAOE;
        [CloneField] public CardSource generateCard;
        [CustomDesc("damageRate")] public string dRateString { get => (damageRate * 100).ToString() + "%"; }
        [CustomDesc("cardName")] public string dCard{get=> generateCard.Name;}
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
            var card = generateCard.GenerateCard();
            BattleCardManager.Ins.GenerateCardTo(card,CardPosition.Hand);
            yield break;
        }
    }

}

