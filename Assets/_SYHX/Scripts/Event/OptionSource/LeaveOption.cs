using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveOption : OptionSource
{
    public override void Effect()
    {
        EventManager.Ins.ReleaseEvent();
    }
}
