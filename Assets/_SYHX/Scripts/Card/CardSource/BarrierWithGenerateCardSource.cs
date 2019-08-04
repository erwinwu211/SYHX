using System.Collections;
using UnityEngine;
namespace SYHX.Cards

{
    [SourceName("防御生成卡牌")]


    public class BarrierWithGenerateCardSource : CardSource<BarrierWithGenerateCardContent> { }

    [System.Serializable]
    public class BarrierWithGenerateCardContent : CardContent
    {
        [Header("防御率")] [CloneField] public float defenceRate;
        [Header("生成卡牌")] [CloneField] public CardSource cardName;
        [CustomDesc("defenceRate")] public string dRateString { get => (defenceRate * 100).ToString() + "%"; }
        [CustomDesc("cardName")] public string dCard { get => cardName.Name; }
        protected override IEnumerator UseEffect(CardUseTrigger trigger)
        {
            BattleCharacterManager.Ins.hero.GetBarrier(defenceRate);
            var card = cardName.GenerateCard();
            BattleCardManager.Ins.GenerateCardTo(card, CardPosition.Hand);
            yield break;
        }
    }

}
