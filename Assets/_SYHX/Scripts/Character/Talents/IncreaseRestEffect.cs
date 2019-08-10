using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseRestEffect : Talent
{
    // Start is called before the first frame update
    public override void OnActive()
    {
        base.OnActive();
        DungeonManager.Ins.RestEfficiency = 0.25f;
    }
}
