using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SYHX.Cards
{
    public class BattleCardUI : ProtoCardUI
    {
        public CardPosition position;
        
        public void OnPointClick()
        {
            if (position == CardPosition.Hand)
                cc.OnUse(CardUseTrigger.ByUser);
        }
    }
}
public enum CardPosition
{
    Hand, Deck, Exhaust, Discard, Used, Temp
}
