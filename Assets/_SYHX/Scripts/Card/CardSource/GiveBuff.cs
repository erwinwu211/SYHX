using SYHX.Buff;

public class GiveBuff : CardSource
{
    public BuffSource source;
    public int count;
    public override void Effect(CardContent cc)
    {
        BattleManager.Ins.hero.buffs.Increase(source, count);
    }
}
