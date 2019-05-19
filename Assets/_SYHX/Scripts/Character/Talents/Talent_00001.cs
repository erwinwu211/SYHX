using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talent_00001 : Talent
{
    protected override void Effect()
    {
        owner.HpMax += 5;
    }
}
