namespace SYHX.Cards
{
    public class BarrierCardSource : CardSource<BarrierCardContent> { }

    [System.Serializable]
    public class BarrierCardContent : CardContent
    {
        [CloneField] public float defenceRate;
        [CustomDesc("defenceRate")] public string dRateString { get => (defenceRate * 100).ToString() + "%"; }
        protected override void UseEffect(CardUseTrigger trigger)
        {
            BattleCharacterManager.Ins.hero.GetBarrier(defenceRate);
        }
    }


}
