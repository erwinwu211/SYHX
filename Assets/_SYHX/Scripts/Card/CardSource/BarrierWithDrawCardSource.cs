using System.Collections;
using UnityEngine;
namespace SYHX.Cards
{
    [SourceName("护盾")]
    public class BarrierWithDrawCardSource : CardSource<BarrierWithDrawCardContent> { }

    [System.Serializable]
    public class BarrierWithDrawCardContent : CardContent
    {
        [Header("防御率")] [CloneField] public float defenceRate;
        [Header("抽牌数量")] [CloneField] public int drawNumber;

        [CustomDesc("defenceRate")] public string dRateString { get => (defenceRate * 100).ToString() + "%"; }
        [CustomDesc("drawNumber")] public string dDraw { get => drawNumber.ToString(); }

        protected override IEnumerator UseEffect(CardUseTrigger trigger)
        {
            BattleCharacterManager.Ins.hero.GetBarrier(defenceRate);
            BattleManager.sDraw(drawNumber);
            yield break;
        }
    }


}
