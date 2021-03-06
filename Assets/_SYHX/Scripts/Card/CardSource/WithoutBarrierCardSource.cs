﻿using System.Collections;
using UnityEngine;
namespace SYHX.Cards
{
    [SourceName("护盾")]
    public class WithoutBarrierCardSource : CardSource<WithoutBarrierCardContent> { }

    [System.Serializable]
    public class WithoutBarrierCardContent : CardContent
    {
        [Header("防御率")] [CloneField] public float defenceRate;
        [CustomDesc("defenceRate")] public string dRateString { get => (defenceRate * 100).ToString() + "%"; }
        protected override IEnumerator UseEffect(CardUseTrigger trigger)
        {
            if (BattleManager.Hero.barrier == 0)
                BattleCharacterManager.Ins.hero.GetBarrier(defenceRate);
            yield break;
        }
    }


}
