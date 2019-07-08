using System.Collections;
using SYHX.AbnormalStatus;
namespace SYHX.Cards
{
    [SourceName("护盾")]
    public class BarrierWithASSource : CardSource<BarrierWithASContent> { }

    [System.Serializable]
    public class BarrierWithASContent : CardContent
    {
        [CloneField] public float defenceRate;
        [CloneField] public AbnormalStatusSource source;
        [CloneField] public int count;
        [CustomDesc("defenceRate")] public string dRateString { get => (defenceRate * 100).ToString() + "%"; }
        [CustomDesc("turn")] public string dTurn{get => count.ToString();}
        [CustomDesc("status")] public string dStatus {get => source.Name;}

        protected override IEnumerator UseEffect(CardUseTrigger trigger)
        {
            BattleManager.Hero.GetBarrier(defenceRate);
            BattleManager.Hero.AddAbnormalStatus(source,count);
            yield break;
        }
    }


}
