public class AttackCard : CardSource
{
    public int damageRate;
    public bool isAOE;
    public override void Effect(CardContent cc)
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
