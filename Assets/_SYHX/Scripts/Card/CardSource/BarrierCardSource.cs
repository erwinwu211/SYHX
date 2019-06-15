public class BarrierCardSource : CardSource<BarrierCardContent> { }

[System.Serializable]
public class BarrierCardContent : CardContent
{
    [CloneField] public float defenceRate;
    [CardDesc("defenceRate")] public string dRateString { get => (defenceRate * 100).ToString() + "%"; }
    public override void OnUse()
    {
        BattleCharacterManager.Ins.hero.GetBarrier(defenceRate);
    }
}
