using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleResult
{
    Continue,
    Win,
    Lose,
}

public class BattleManager : SingletonMonoBehaviour<BattleManager>
{
    private EnemyGroup mBattleModel;
    public EnemyGroup BattleModel { get { return mBattleModel; } }
    public Enemy selectedEnemy;

    //回合数计数器
    public int RountCount { get; set; }

    //能量值计数器
    public int correctEP;
    public int maxEP;
    public int moreEP;
    //卡牌管理器
    public CardManager cardManager;

    //每回合抽卡数量
    private int drawCountPerTurn = 5;
    public int DrawCountPerTurn => drawCountPerTurn;
    // public Hero hero = new Hero();
    private List<Enemy> enemyList;

    protected override void UnityAwake()
    {

    }


    // void Update() { }



    /// <summary>
    /// 战斗开始，战斗场景的入口
    /// </summary>
    /// <param name="id">战斗数据的键值</param>
    /// <param name="context">将GameManager作为上下文传入</param>
    public void BattleStart(int id, GameManager context)
    {
        //读取战斗数据
        // mBattleModel = new EnemyGroup(id, 1);
        enemyList = EnemyGroupManager.Ins.enemyGroup[id].CreateEnemyGroup();
        selectedEnemy = enemyList[0];


        //重置各卡牌存储空间
        //战斗状态切换为初始化阶段
        // ChangeStatus(new InitializationState(this));
    }



    /// <summary>
    /// 战斗结束，做BattleManager的收尾工作
    /// </summary>
    public void BattleEnd()
    {
        // mBattleStatus = null;
        mBattleModel = null;
        BattleCharacterManager.Ins.Destroy();
        BattleProgressEvent.Ins.Destroy();
    }




    /// <summary>
    /// 抽牌方法
    /// </summary>
    /// <param name="count">抽牌的张数</param>
    public void Draw(int count) => CardManager.Ins.Draw(count);

    /// <summary>
    /// 洗牌方法
    /// </summary>
    private void Shuffle() => CardManager.Ins.Shuffle();

    /// <summary>
    /// 回复能量值的方法
    /// </summary>
    public void EnergyPointRegain()
    {
        //当前能量=上限
        correctEP = maxEP;

        //若有额外恢复值，再继续添加
        if (moreEP > 0)
        {
            correctEP += moreEP;
            moreEP = 0;
        }
    }



    /// <summary>
    /// 在下一回合回复额外能量值的方法
    /// </summary>
    /// <param name="count"></param>
    public void RegainMoreEnergyPointNextTurn(int count)
    {
        moreEP = count;
    }

    /// <summary>
    /// 检测战斗是否结束
    /// 检测方式：
    /// 是否所有敌人均已死亡
    /// 是否角色死亡
    /// </summary>
    /// <returns></returns>
    public BattleResult IsBattleOver()
    {
#if COMPLETE
        BattleResult res = BattleResult.Win;
        foreach (Enemy enemy in enemyList)
        {
            if (enemy.IsAlive())
            {
                res = BattleResult.Continue;
            }
        }
        if (hero.currentHp <= 0)
        {
            res = BattleResult.Lose;
        }
#endif
        return BattleResult.Continue;

    }
}

// /// <summary>
// /// 战斗状态切换的方法
// /// </summary>
// /// <param name="status">实例化一个新的战斗状态出来</param>
// public void ChangeStatus(TurnState status)
// {
//     // this.mBattleStatus = status;
//     status.Enter();
// }
