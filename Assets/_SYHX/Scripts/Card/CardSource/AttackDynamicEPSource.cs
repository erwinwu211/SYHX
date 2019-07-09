using System.Collections;
using UnityEngine;
namespace SYHX.Cards

{
    [SourceName("攻击")]
    public class AttackDynamicEPSource : CardSource<AttackDynamicEP> { }

    [System.Serializable]
    public class AttackDynamicEP : CardContent
    {
        [Header("伤害率")] [CloneField] public float damageRate;
        [Header("群伤？")] [CloneField] public bool isAOE;
        [CustomDesc("damageRate")] public string dRateString { get => (damageRate * 100).ToString() + "%"; }
        private ConnectionSignal signal;

        public override int TempEP
        {
            get
            {
                if (!battleKeyword.Contains("initial"))
                {
                    signal = BattleManager.Ins.signals.GetSignal<ConnectionSignal>("connection");
                    signal.Attach((i) => RefreshUI());
                    AddBattleAttribute("initial");
                }
                var ep = base.TempEP;
                if (signal != null)
                {
                    ep -= signal.signalValue;
                }
                if (ep < 0) ep = 0;
                return ep;
            }
        }
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
            yield break;
        }
    }

}
