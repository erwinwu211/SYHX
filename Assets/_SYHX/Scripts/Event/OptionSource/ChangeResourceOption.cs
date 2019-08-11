using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeResourceOption : OptionSource
{
    public int ChipFormCount_Increase;
    public int FoodCount_Increase;
    public int Hp_Increase;
    public int ChipFormCount_Decrease;
    public int FoodCount_Decrease;
    public int Hp_Decrease;
    public int ExpCount;
    public int ForceExp;
    public int AgileExp;
    public int ConstitutionExp;
    public int FortuneExp;
    public string result;
    public override void Effect()
    {
        DungeonManager.Ins.IncreaseDataFrag(ChipFormCount_Increase);
        DungeonManager.Ins.IncreaseFood(FoodCount_Increase);
        DungeonManager.Ins.DecreaseDataFrag(ChipFormCount_Decrease);
        DungeonManager.Ins.DecreaseFood(FoodCount_Decrease);
        CharacterInDungeon.Ins.GainExp(ExpCount);
        CharacterInDungeon.Ins.Force.GainExp(ForceExp);
        CharacterInDungeon.Ins.Agile.GainExp(AgileExp);
        CharacterInDungeon.Ins.Constitution.GainExp(ConstitutionExp);
        CharacterInDungeon.Ins.Fortune.GainExp(FortuneExp);
        EventManager.Ins.EUI.ShowResultPanel(result);
    }
}
