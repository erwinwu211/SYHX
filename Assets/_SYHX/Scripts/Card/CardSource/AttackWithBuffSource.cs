using System.Collections;
using SYHX.AbnormalStatus;
using UnityEngine;
namespace SYHX.Cards

{
    [SourceName("攻击加状态")]
    public class AttackWithBuffSource : CardSource<AttackCarAttackWithBuffContent> { }

    [System.Serializable]
    public class AttackCarAttackWithBuffContent : CardContent
    {
        [Header("伤害率")] [CloneField] public float damageRate;
        [Header("异常状态")] [CloneField] public AbnormalStatusSource source;
        [Header("回合数")] [CloneField] public int count;
        [CustomDesc("damageRate")] public string dRateString { get => (damageRate * 100).ToString() + "%"; }
        [CustomDesc("count")] public string dTurn { get => count.ToString(); }
        [CustomDesc("source")] public string dStatus { get => source.Name; }
        protected override IEnumerator UseEffect(CardUseTrigger trigger)
        {

            Damage.CalculateAndApply(BattleManager.Hero, BattleManager.selectedEnemy, damageRate, DamageTrigger.ByCard);
            BattleManager.selectedEnemy.AddAbnormalStatus(source, count);
            yield break;
        }
    }

}

