using SYHX.AbnormalStatus;
using System.Collections;
namespace SYHX.Cards
{
    [SourceName("给与buff")]
    public class GiveBuffSource : CardSource<GiveBuffContent> { }

    [System.Serializable]
    public class GiveBuffContent : CardContent
    {
        [CloneField] public AbnormalStatusSource source;
        [CloneField] public int count;

        protected override IEnumerator UseEffect(CardUseTrigger trigger)
        {
            BattleManager.Hero.AddAbnormalStatus(source, count);
            yield break;
        }
    }


}