using System.Collections;
namespace SYHX.Cards

{
    [SourceName("攻击防御")]
    public class AttackWithBarrierSource : CardSource<AttackWithBarrierContent> { }

    [System.Serializable]
    public class AttackWithBarrierContent : CardContent
    {
        [CloneField] public float damageRate;
        [CloneField] public bool isAOE;
        [CustomDesc("damageRate")] public string dRateString { get => (damageRate * 100).ToString() + "%"; }
        protected override IEnumerator UseEffect(CardUseTrigger trigger)
        {
            int defence = 0;
            if (isAOE)
            {
                foreach (var enemy in BattleManager.enemyList)
                {
                    defence += Damage.CalculateAndApply(BattleManager.Hero, enemy, damageRate, DamageTrigger.ByCard);
                }
            }
            else
            {
                defence += Damage.CalculateAndApply(BattleManager.Hero, BattleManager.selectedEnemy, damageRate, DamageTrigger.ByCard);
            }
            BattleManager.Hero.GetBarrier(defence);
            yield break;
        }
    }

}

