using System.Collections;
namespace SYHX.Cards

{
    [SourceName("剑柄打击")]
    public class AttackWithDrawCardSource : CardSource<AttackWithDrawCardContent> { }

    [System.Serializable]
    public class AttackWithDrawCardContent : CardContent
    {
        [CloneField] public float damageRate;
        [CloneField] public int drawCount;
        [CloneField] public bool isAOE;
        [CustomDesc("damageRate")] public string dRateString { get => (damageRate * 100).ToString() + "%"; }
        [CustomDesc("drawCount  ")] public string dDraw { get => drawCount.ToString(); }
        protected override IEnumerator UseEffect(CardUseTrigger trigger)
        {
            if (isAOE)
            {
                foreach (var enemy in BattleManager.enemyList)
                {
                    Damage.CalculateAndApply(BattleManager.Hero, enemy, damageRate, DamageTrigger.ByCard);
                }
            }
            else
            {
                Damage.CalculateAndApply(BattleManager.Hero, BattleManager.selectedEnemy, damageRate, DamageTrigger.ByCard);
            }
            BattleManager.sDraw(drawCount);
            yield break;
        }
    }

}

