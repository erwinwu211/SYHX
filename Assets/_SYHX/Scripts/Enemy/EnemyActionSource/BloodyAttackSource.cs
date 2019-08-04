using UnityEngine;
using System.Collections;
using SYHX.AbnormalStatus;
namespace SYHX.EnemyAI
{
    public class BloodyAttackSource : EnemyActionSource<BloodyAttackContent> { }

    [System.Serializable]
    public class BloodyAttackContent : EnemyActionContent
    {
        [Header("伤害率")] [CloneField] public float damageRate;
        [Header("回合数")] [CloneField] public int count;

        [CloneField] public AbnormalStatusSource asSource;
        [CustomDesc("damageRate")] public string damage { get => (damageRate * 100).ToString() + "%"; }
        [CustomDesc("count")] public string dTurn { get => count.ToString(); }


        protected override IEnumerator execute()
        {
            var damage = Damage.CalculateAndApply(enemy, BattleCharacterManager.Ins.hero, damageRate, DamageTrigger.ByEnemy);
            BattleManager.Hero.AddAbnormalStatus(asSource, count, enemy.Attack);
            yield return new WaitForSeconds(1f);
            yield break;
        }
    }

}


