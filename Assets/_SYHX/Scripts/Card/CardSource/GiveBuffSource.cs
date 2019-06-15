using SYHX.Buff;

public class GiveBuffSource : CardSource<GiveBuffContent>
{
    // public BuffSource source;
    // public int count;
    // public override void Effect(CardContent cc)
    // {
    //     BattleManager.Ins.hero.buffs.Increase(source, count);
    // }
}

[System.Serializable]
public class GiveBuffContent : CardContent
{
    [CloneField] public BuffSource source;
    [CloneField] public int count;

    public override void OnUse()
    {
        BattleManager.Ins.hero.buffs.Increase(source, count);
    }
}
