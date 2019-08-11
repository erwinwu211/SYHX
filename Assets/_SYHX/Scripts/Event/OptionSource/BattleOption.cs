using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleOption : OptionSource
{
    // Start is called before the first frame update
    public EnemyGroup enemyGroup;

    public string result;

    public override void Effect(){
        DungeonManager.Ins.BattleHappen(enemyGroup,this);
    }

    public override void AfterBattleEffect()
    {
        EventManager.Ins.EUI.ShowResultPanel(result);
    }
}
