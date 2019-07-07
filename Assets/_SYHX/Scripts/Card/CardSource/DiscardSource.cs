using System.Collections;
using System.Collections.Generic;
namespace SYHX.Cards
{
    [SourceName("弃牌造成伤害")]
    public class DiscardSource : CardSource<DiscardContent> { }

    [System.Serializable]
    public class DiscardContent : CardContent
    {
        [CloneField] public int count;
        [CloneField] public float damageRate;

        [CustomDesc("number")] public string dNumber {get => count.ToString();}
        [CustomDesc("damageRate")] public string dRateString { get => (damageRate * 100).ToString() + "%"; }
        protected override IEnumerator UseEffect(CardUseTrigger trigger)
        {
            var handList = new List<CardContent>(BattleCardManager.Ins.safeDeckPile);
            if(handList.Contains(this))
            {
                handList.Remove(this);
            }
            var outList = new List<CardContent>();
            yield return CardSelectorManager.Ins.Register(handList,outList,count,count);
            BattleCardManager.Ins.Discard(outList);
            Damage.CalculateAndApply(BattleManager.Hero, BattleManager.selectedEnemy, damageRate, DamageTrigger.ByCard);
            yield break;
        }
    }


}