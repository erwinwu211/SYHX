using SYHX.AbnormalStatus;
namespace SYHX.Cards
{
    public class GiveBuffSource : CardSource<GiveBuffContent> { }

    [System.Serializable]
    public class GiveBuffContent : CardContent
    {
        [CloneField] public AbnormalStatusSource source;
        [CloneField] public int count;

        protected override void UseEffect(CardUseTrigger trigger)
        {
            BattleManager.Ins.hero.asManager.Increase(source, count);
        }
    }


}