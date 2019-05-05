using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SYHX.Buff
{
    public class PowerUpSource : BuffSource<PowerUp, PowerUpConstant>
    {
    }
    public class PowerUp : Buff<PowerUp, PowerUpSource, PowerUpConstant>
    {
        public override void OnAdd(BattleCharacter owner)
        {
            owner.ChangeAttackRate(optionalValue["attackRate"]);
        }
        public override void OnClear(BattleCharacter owner)
        {
            owner.ChangeAttackRate(-optionalValue["attackRate"]);
        }
        public override void OnRemove(BattleCharacter owner)
        {
            owner.ChangeAttackRate(-optionalValue["attackRate"]);
        }
    }

    [Serializable]
    public struct PowerUpConstant
    {
        public float attackRate;
    }
}
