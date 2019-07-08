using System.Collections;
using SYHX.AbnormalStatus;
namespace SYHX.Cards

{
    [SourceName("攻击加状态")]
    public class AttackWithBuffSource : CardSource<AttackCarAttackWithBuffContent> { }

    [System.Serializable]
    public class AttackCarAttackWithBuffContent : CardContent
    {
        [CloneField] public float damageRate;
        [CloneField] public AbnormalStatusSource source;
        [CloneField] public int count;
        [CustomDesc("damageRate")] public string dRateString { get => (damageRate * 100).ToString() + "%"; }
        [CustomDesc("turn")] public string dTurn{get => count.ToString();}
        [CustomDesc("status")] public string dStatus {get => source.Name;}
        protected override IEnumerator UseEffect(CardUseTrigger trigger)
        {

            Damage.CalculateAndApply(BattleManager.Hero, BattleManager.selectedEnemy, damageRate, DamageTrigger.ByCard);
            BattleManager.selectedEnemy.AddAbnormalStatus(source, count);
            yield break;
        }
    }

}

