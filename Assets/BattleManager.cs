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

public class BattleManager : MonoBehaviour {

    public BattleStatus BattleStatus;

	// Use this for initialization
	void Start () {
        BattleStatus = BattleStatus.None;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BattleStart()
    {
        ChangeStatus(BattleStatus.Initialization);
    }

    public void BattleEnd()
    {
        ChangeStatus(BattleStatus.None);
    }

    public void ChangeStatus(BattleStatus status)
    {
        this.BattleStatus = status;

        switch (status)
        {
            //初始化阶段
            case BattleStatus.Initialization:
                Debug.Log("开始初始化");
                Debug.Log("加载场景、人物、怪物");
                ChangeStatus(BattleStatus.PlayerTurnStart);
                return;

            //我方回合开始阶段
            case BattleStatus.PlayerTurnStart:
                Debug.Log("回合开始");
                Debug.Log("计算BUFF效果");
                Debug.Log("发牌");
                ChangeStatus(BattleStatus.PlayerTurnGoing);
                return;

            //我方回合进行阶段
            case BattleStatus.PlayerTurnGoing:
                Debug.Log("回合进行中");
                return;

            //我方回合结束阶段
            case BattleStatus.PlayerTurnEnd:
                Debug.Log("回合结束");
                Debug.Log("计算BUFF效果");
                Debug.Log("清空手牌和当前使用区");
                ChangeStatus(BattleStatus.EnemyTurnStart);
                return;

            //敌方回合开始阶段
            case BattleStatus.EnemyTurnStart:
                Debug.Log("敌方回合开始");
                Debug.Log("计算BUFF效果");
                ChangeStatus(BattleStatus.EnemyTurnGoing);
                return;

            //敌方回合进行阶段
            case BattleStatus.EnemyTurnGoing:
                Debug.Log("敌方回合进行中");
                Debug.Log("根据AI进行行动");
                ChangeStatus(BattleStatus.EnemyTurnEnd);
                return;

            //敌方回合结束阶段
            case BattleStatus.EnemyTurnEnd:
                Debug.Log("敌方回合结束");
                Debug.Log("计算BUFF效果");
                ChangeStatus(BattleStatus.PlayerTurnStart);
                return;
        }

    }
}
