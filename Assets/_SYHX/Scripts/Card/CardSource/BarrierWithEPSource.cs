using System.Collections;
using UnityEngine;
namespace SYHX.Cards
{
    [SourceName("护盾能量")]
    public class BarrierWithEPSource : CardSource<BarrierWithEPContent> { }

    [System.Serializable]
    public class BarrierWithEPContent : CardContent
    {
        [Header("防御率")] [CloneField] public float defenceRate;
        [Header("获得能量")] [CloneField] public int gainEP;
        [CustomDesc("defenceRate")] public string dRateString { get => (defenceRate * 100).ToString() + "%"; }
        [CustomDesc("gainEP")] public string dGainEP { get => gainEP.ToString(); }
        protected override IEnumerator UseEffect(CardUseTrigger trigger)
        {
            BattleManager.Hero.GetBarrier(defenceRate);
            BattleManager.sRegainMoreEnergyPointNextTurn(gainEP);
            yield break;
        }
    }


}
