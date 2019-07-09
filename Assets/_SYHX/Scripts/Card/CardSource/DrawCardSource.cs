using System.Collections;
using UnityEngine;
namespace SYHX.Cards
{
    [SourceName("抽牌")]
    public class DrawCardSource : CardSource<DrawCardContent> { }

    [System.Serializable]
    public class DrawCardContent : CardContent
    {
        [Header("抽牌数量")] [CloneField] public int drawNumber;
        [CustomDesc("drawNumber")] public string dRateString { get => drawNumber.ToString(); }
        protected override IEnumerator UseEffect(CardUseTrigger trigger)
        {
            BattleCardManager.Ins.Draw(drawNumber);
            yield break;
        }
    }

}