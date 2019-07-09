using System.Collections;
using UnityEngine;
namespace SYHX.Cards
{
    [SourceName("护盾")]
    public class BarrierCardSource : CardSource<BarrierCardContent> { }

    [System.Serializable]
    public class BarrierCardContent : CardContent
    {
        [Header("防御率")] [CloneField] public float defenceRate;
        [CustomDesc("defenceRate")] public string dRateString { get => (defenceRate * 100).ToString() + "%"; }
        protected override IEnumerator UseEffect(CardUseTrigger trigger)
        {
            BattleCharacterManager.Ins.hero.GetBarrier(defenceRate);
            yield break;
        }
    }


}
