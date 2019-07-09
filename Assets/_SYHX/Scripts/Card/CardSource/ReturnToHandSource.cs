using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SYHX.Cards
{
    [SourceName("回手")]
    public class ReturnToHandSource : CardSource<ReturnToHand> { }

    [System.Serializable]
    public class ReturnToHand : CardContent
    {
        [Header("手牌数")] [CloneField] public int count;

        protected override IEnumerator UseEffect(CardUseTrigger trigger)
        {
            var cards = new List<CardContent>(BattleCardManager.Ins.safeDiscardPile);
            var result = new List<CardContent>();
            yield return CardSelectorManager.Ins.Register(cards, result, 1, count);
            foreach (var card in result)
            {
                card.AddBattleAttribute("0");
                BattleCardManager.Ins.ReturnToHand(card);
            }
            yield break;
        }
    }


}