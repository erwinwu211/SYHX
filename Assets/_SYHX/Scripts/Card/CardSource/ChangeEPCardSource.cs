using System.Collections;
namespace SYHX.Cards
{
    [SourceName("能量")]
    public class ChangeEPCardSource : CardSource<ChangeEPCardContent> { }

    [System.Serializable]
    public class ChangeEPCardContent : CardContent
    {
        [CloneField] public int GainEP;
        [CustomDesc("GainEP")] public string dRateString { get => GainEP.ToString(); }
        protected override IEnumerator UseEffect(CardUseTrigger trigger)
        {
            BattleManager.sChangeEnergy(GainEP);
            yield break;
        }
    }

}
