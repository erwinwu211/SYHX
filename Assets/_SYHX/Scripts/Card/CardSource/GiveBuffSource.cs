using SYHX.AbnormalStatus;
namespace SYHX.Cards
{
    [SourceName("给与buff")]
    public class GiveBuffSource : CardSource<GiveBuffContent> { }

    [System.Serializable]
    public class GiveBuffContent : CardContent
    {
        [CloneField] public AbnormalStatusSource source;
        [CloneField] public int count;

        protected override void UseEffect(CardUseTrigger trigger)
        {
            BattleManager.Hero.AddAbnormalStatus(source, count);
        }
    }


}