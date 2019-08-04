using System.Collections;
using UnityEngine;
namespace SYHX.Cards

{
    [SourceName("消耗全部能量攻击")]
    public class AttackWithAllEPSource : CardSource<AttackWithAllEPContent> { }

    [System.Serializable]
    public class AttackWithAllEPContent : CardContent
    {
        [Header("伤害率")] [CloneField] public float damageRate;
        [CustomDesc("damageRate")] public string dRateString { get => (damageRate * 100).ToString() + "%"; }
        [Header("目标选择类型")] [CloneField] public AttackTargetType targetType;
        [Header("伤害次数")] [CloneField] public int attackFrequency = 1;
        [CustomDesc("frequency")] public string dFrequencyString { get => attackFrequency + ""; }


        public override int TempEP
        {
            get
            {
                var ep = base.TempEP;
                ep = BattleManager.sGetEP();
                if (ep < 0) ep = 0;
                attackFrequency = ep;
                return ep;
            }
        }
        protected override IEnumerator UseEffect(CardUseTrigger trigger)
        {
            for (int i = 0; i < attackFrequency; i++)
            {
                switch (targetType)
                {
                    case AttackTargetType.mark:
                        {
                            Damage.CalculateAndApply(BattleManager.Hero, BattleManager.selectedEnemy, damageRate, DamageTrigger.ByCard);
                            break;
                        }
                    case AttackTargetType.aoe:
                        foreach (var enemy in BattleManager.enemyList)
                        {
                            Damage.CalculateAndApply(BattleManager.Hero, enemy, damageRate, DamageTrigger.ByCard);
                        }
                        break;
                    case AttackTargetType.random:
                        int r = Random.Range(0, BattleManager.enemyList.Count - 1);
                        Damage.CalculateAndApply(BattleManager.Hero, BattleManager.enemyList[r], damageRate, DamageTrigger.ByCard);
                        break;
                }
                // if (targetType == AttackTargetType.aoe)
                // {
                //     foreach (var enemy in BattleManager.enemyList)
                //     {
                //         Damage.CalculateAndApply(BattleManager.Hero, enemy, damageRate, DamageTrigger.ByCard);
                //     }
                // }

                // else
                // {
                //     Damage.CalculateAndApply(BattleManager.Hero, BattleManager.selectedEnemy, damageRate, DamageTrigger.ByCard);
                // }
            }

            yield break;
        }
    }

}

