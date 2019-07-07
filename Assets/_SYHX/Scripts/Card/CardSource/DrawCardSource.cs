using System.Collections;

namespace SYHX.Cards
{
    [SourceName("抽牌")]
    public class DrawCardSource : CardSource<DrawCardContent> { }

    [System.Serializable]
    public class DrawCardContent : CardContent
    {
        [CloneField] public int drawNumber;
        [CustomDesc("number")] public string dRateString { get => drawNumber.ToString(); }
        protected override IEnumerator UseEffect(CardUseTrigger trigger)
        {
            BattleCardManager.Ins.Draw(drawNumber);
            yield break;
        }
    }

}