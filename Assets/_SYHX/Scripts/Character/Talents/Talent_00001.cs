using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talent_00001 : Talent
{
    public Talent_00001()
    {
        Name = "测试天赋";
        Desc = "生命最大值+5";
    }

    protected override void Effect()
    {
        owner.HpMax += 5;
    }
}
