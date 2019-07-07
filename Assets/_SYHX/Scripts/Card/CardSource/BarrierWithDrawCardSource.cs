using System.Collections;
namespace SYHX.Cards
{
    [SourceName("护盾")]
    public class BarrierWithDrawCardSource : CardSource<BarrierWithDrawCardContent> { }

    [System.Serializable]
    public class BarrierWithDrawCardContent : CardContent
    {
        [CloneField] public float defenceRate;
        [CloneField] public int drawCount;

        [CustomDesc("defenceRate")] public string dRateString { get => (defenceRate * 100).ToString() + "%"; }
        [CustomDesc("drawCount  ")] public string dDraw { get => drawCount.ToString(); }

        protected override IEnumerator UseEffect(CardUseTrigger trigger)
        {
            BattleCharacterManager.Ins.hero.GetBarrier(defenceRate);
            BattleManager.sDraw(drawCount);
            yield break;
        }
    }


}
