using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseRestEffect : Talent
{
    public float IncreaseRestEfficiency;
    public int IncreaseForceExpPerTraining;
    public int IncreaseAgileExpPerTraining;
    public int IncreaseConstitutionExpPerTraining;

    public override void DoEffect()
    {
        DungeonManager.Ins.RestEfficiency += IncreaseRestEfficiency;
        DungeonManager.Ins.ForceExp_Per_Training += IncreaseForceExpPerTraining;
        DungeonManager.Ins.AgileExp_Per_Training += IncreaseAgileExpPerTraining;
        DungeonManager.Ins.ConstitutionExp_Per_Training += IncreaseConstitutionExpPerTraining;
    }
}
