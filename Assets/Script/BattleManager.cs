using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleStatus
{
    Initialization,
    PlayerTurnStart,
    PlayerTurnGoing,
    PlayerTurnEnd,
    EnemyTurnStart,
    EnemyTurnGoing,
    EnemyTurnEnd,
    Result,
    None
}

public class Hero
{
    public int lv { get; set; }
    public int maxHp { get; set; }
    public int correctHp { get; set; }
    public int maxEp { get; set; }
    public int correctEp { get; set; }
    public int atk { get; set; }
    public int atkAdd { get; set; }
    public int atkMult { get; set; }
    public int atkPara { get; set; }
    public int def { get; set; }
    public int defAdd { get; set; }
    public int defMult { get; set; }
    public int defPara { get; set; }

    public Hero()
    {
        lv = 1;
        maxHp = 100;
        correctHp = 100;
        maxEp = 5;
        correctEp = 5;
        atk = 10;
        def = 10;
    }
}

public class BattleManager : MonoBehaviour {

    public ABattleStatus mBattleStatus = null;
    public GameObject Card;
    public int RountCount;
    public Hero hero;


	void Start () {
	}
	

	void Update () {
		if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("现在是：" + mBattleStatus.ToString() + "状态");
        }
        if (mBattleStatus != null)
        {
            mBattleStatus.Update();
        }
	}

    public void BattleStart(int id,GameManager context)
    {
        ChangeStatus(new InitializationStatus(this));
    }

    public void BattleEnd()
    {
        mBattleStatus = null;
    }

    //状态切换的方法
    public void ChangeStatus(ABattleStatus status)
    {
        this.mBattleStatus = status;
        status.Start();
    }
}
