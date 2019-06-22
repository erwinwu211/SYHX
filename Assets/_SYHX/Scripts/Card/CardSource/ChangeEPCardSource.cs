namespace SYHX.Cards
{
    public class ChangeEPCardSource : CardSource<ChangeEPCardContent> { }

    [System.Serializable]
    public class ChangeEPCardContent : CardContent
    {
        [CloneField] public int GainEP;
        [CustomDesc("GainEP")] public string dRateString { get => GainEP.ToString(); }
        protected override void UseEffect(CardUseTrigger trigger)
        {
            BattleManager.Ins.ChangeEnergy(GainEP);
        }
    }

}
