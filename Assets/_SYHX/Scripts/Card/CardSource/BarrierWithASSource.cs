using System.Collections;
using SYHX.AbnormalStatus;
using UnityEngine;
namespace SYHX.Cards
{
    [SourceName("护盾")]
    public class BarrierWithASSource : CardSource<BarrierWithASContent> { }

    [System.Serializable]
    public class BarrierWithASContent : CardContent
    {
        [Header("防御率")] [CloneField] public float defenceRate;
        [Header("异常状态")] [CloneField] public AbnormalStatusSource source;
        [Header("回合数")] [CloneField] public int count;
        [CustomDesc("defenceRate")] public string dRateString { get => (defenceRate * 100).ToString() + "%"; }
        [CustomDesc("count")] public string dTurn { get => count.ToString(); }
        [CustomDesc("source")] public string dStatus { get => source.Name; }

        protected override IEnumerator UseEffect(CardUseTrigger trigger)
        {
            BattleManager.Hero.GetBarrier(defenceRate);
            BattleManager.Hero.AddAbnormalStatus(source, count);
            yield break;
        }
    }


}
