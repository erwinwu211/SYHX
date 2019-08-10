using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetResourceTalent : Talent
{
    public int ChipFormCount;
    public int ExpCount; 
    public int ForceExp;
    public int AgileExp;
    public int ConstitutionExp;
    public int FortuneExp;
    public override void DoEffect()
    {
        DungeonManager.Ins.IncreaseDataFrag(ChipFormCount);
        CharacterInDungeon.Ins.GainExp(ExpCount);
        CharacterInDungeon.Ins.Force.GainExp(ForceExp);
        CharacterInDungeon.Ins.Agile.GainExp(AgileExp);
        CharacterInDungeon.Ins.Constitution.GainExp(ConstitutionExp);
        CharacterInDungeon.Ins.Fortune.GainExp(FortuneExp);
    }
}
