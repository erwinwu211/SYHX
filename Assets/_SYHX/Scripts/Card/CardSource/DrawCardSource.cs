namespace SYHX.Cards
{
    public class DrawCardSource : CardSource<DrawCardContent> { }

    [System.Serializable]
    public class DrawCardContent : CardContent
    {
        [CloneField] public int drawNumber;
        [CustomDesc("number")] public string dRateString { get => drawNumber.ToString(); }
        protected override void UseEffect(CardUseTrigger trigger)
        {
            BattleCardManager.Ins.Draw(drawNumber);
        }
    }

}