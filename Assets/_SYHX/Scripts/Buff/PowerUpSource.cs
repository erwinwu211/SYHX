using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SYHX.Buff
{
    public class PowerUpSource : BuffSource<PowerUp, PowerUpConstant>
    {

    }
    public class PowerUp : Buff<PowerUp, PowerUpSource, PowerUpConstant>
    {
    }
    public struct PowerUpConstant
    {
        public float increaseAttack;
    }
}
