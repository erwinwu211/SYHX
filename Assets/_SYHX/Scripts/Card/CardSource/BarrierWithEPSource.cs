using System.Collections;
namespace SYHX.Cards
{
    [SourceName("护盾能量")]
    public class BarrierWithEPSource : CardSource<BarrierWithEPContent> { }

    [System.Serializable]
    public class BarrierWithEPContent : CardContent
    {
        [CloneField] public float defenceRate;
        [CloneField] public int GainEP;
        [CustomDesc("defenceRate")] public string dRateString { get => (defenceRate * 100).ToString() + "%"; }
        [CustomDesc("EP")] public string gainEP{get=> GainEP.ToString();}
        protected override IEnumerator UseEffect(CardUseTrigger trigger)
        {
            BattleManager.Hero.GetBarrier(defenceRate);
            BattleManager.sRegainMoreEnergyPointNextTurn(GainEP);
            yield break;
        }
    }


}
