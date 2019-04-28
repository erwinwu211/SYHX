using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleModel {

    public Enemy Enemy_1 { get { return Enemy_1; } }
    public Enemy Enemy_2 { get { return Enemy_2; } }
    public Enemy Enemy_3 { get { return Enemy_3; } }
    public Enemy Enemy_4 { get { return Enemy_4; } }
    public Enemy Enemy_5 { get { return Enemy_5; } }
    public Enemy Enemy_6 { get { return Enemy_6; } }
    public List<Enemy> enemyList { get { return enemyList; } }

    private float difficultLevel;
	public BattleModel(int id,float difficultLevel)
    {
        this.difficultLevel = difficultLevel;
    }

}
