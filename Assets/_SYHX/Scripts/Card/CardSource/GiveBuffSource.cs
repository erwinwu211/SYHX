using SYHX.Buff;
namespace SYHX.Cards
{
    public class GiveBuffSource : CardSource<GiveBuffContent> { }

    [System.Serializable]
    public class GiveBuffContent : CardContent
    {
        [CloneField] public BuffSource source;
        [CloneField] public int count;

        protected override void UseEffect(CardUseTrigger trigger)
        {
            BattleManager.Ins.hero.buffs.Increase(source, count);
        }
    }


}