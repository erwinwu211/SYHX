public class AttackCardSource : CardSource<AttackCardContent>
{
    // public int damageRate;
    // public bool isAOE;
    // public override void Effect(CardContent cc)
    // {
    //     if (isAOE)
    //     {
    //         foreach (var enemy in BattleManager.Ins.enemyList)
    //         {
    //             Damage.CalculateAndApply(BattleManager.Ins.hero, enemy, damageRate, DamageTrigger.ByCard);
    //         }
    //     }
    //     else
    //     {
    //         Damage.CalculateAndApply(BattleManager.Ins.hero, BattleManager.Ins.selectedEnemy, damageRate, DamageTrigger.ByCard);
    //     }
    // }
}

[System.Serializable]
public class AttackCardContent : CardContent
{
    [CloneField] public float damageRate;
    [CloneField] public bool isAOE;
    [CardDesc("damageRate")] public string dRateString { get => (damageRate * 100).ToString() + "%"; }
    public override void OnUse()
    {
        if (isAOE)
        {
            foreach (var enemy in BattleManager.Ins.enemyList)
            {
                Damage.CalculateAndApply(BattleManager.Ins.hero, enemy, damageRate, DamageTrigger.ByCard);
            }
        }
        else
        {
            Damage.CalculateAndApply(BattleManager.Ins.hero, BattleManager.Ins.selectedEnemy, damageRate, DamageTrigger.ByCard);
        }
    }
}