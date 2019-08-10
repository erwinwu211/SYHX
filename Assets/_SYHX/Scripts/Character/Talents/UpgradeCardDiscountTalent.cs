using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCardDiscountTalent : Talent
{
    public float discount;
    public override void DoEffect()
    {
        DungeonManager.Ins.Discount_For_UpgradeCard += discount;
    }
}
