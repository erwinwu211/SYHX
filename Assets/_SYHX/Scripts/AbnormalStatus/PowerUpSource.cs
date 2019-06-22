// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System;

namespace SYHX.AbnormalStatus
{
    public class PowerUpSource : AbnormalStatusSource<PowerUp>
    {
    }
    public class PowerUp : AbnormalStatusContent
    {
        [CloneField] public float attackRate;
        public override void OnAdd()
        {
            owner.ChangeAttackRate(attackRate);
        }
        public override void OnClear()
        {
            owner.ChangeAttackRate(-attackRate);
        }
        public override void OnRemove()
        {
            owner.ChangeAttackRate(-attackRate);
        }
    }
}
