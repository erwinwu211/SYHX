using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseCardOption : Talent
{
    // Start is called before the first frame update
    public override void OnActive()
    {
        base.OnActive();
        DungeonManager.Ins.CardOptionCount ++;
    }
}
