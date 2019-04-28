using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleResult
{
    Continue,
    Win,
    Lose,
}

public class BattleManager : MonoBehaviour
{

    public ABattleState mBattleStatus = null;
    public GameObject Card;

    private EnemyGroup mBattleModel;
    public EnemyGroup BattleModel { get { return mBattleModel; } }
    public Enemy selectedEnemy;

    //回合数计数器
    public int RountCount { get; set; }

    //能量值计数器
    public int correctEP;
    //  { get => this.hero.currentEP; set => this.hero.currentEP = value; }
    public int maxEP;
    //  { get => this.hero.maxEP; set => this.hero.maxEP = value; }
    public int moreEP;
    //  { get => this.hero.moreEP; set => this.hero.moreEP = value; }

    //卡牌存储区域
    //牌库
    private List<CardContent> deckPile;
    //手牌
    private List<CardContent> handPile;
    //当前使用区
    private List<CardContent> usedPile;
    //弃牌堆
    private List<CardContent> foldPile;

    //每回合抽卡数量
    private int drawCountPerTurn = 5;
    public int DrawCountPerTurn
    {
        get
        {
            return drawCountPerTurn;
        }
        set
        {
            drawCountPerTurn = value;
        }
    }

    // public Hero hero = new Hero();
    private List<Enemy> enemyList;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("现在是：" + mBattleStatus.ToString() + "状态");
        }
        if (mBattleStatus != null)
        {
            mBattleStatus.Update();
        }
    }



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
        deckPile = new List<CardContent>();
        handPile = new List<CardContent>();
        usedPile = new List<CardContent>();
        foldPile = new List<CardContent>();

        //战斗状态切换为初始化阶段
        ChangeStatus(new InitializationState(this));
    }



    /// <summary>
    /// 战斗结束，做BattleManager的收尾工作
    /// </summary>
    public void BattleEnd()
    {
        mBattleStatus = null;
        mBattleModel = null;
    }




    /// <summary>
    /// 抽牌方法
    /// </summary>
    /// <param name="count">抽牌的张数</param>
    public void Draw(int count)
    {
        if (deckPile.Count == 0)
        {
            if (foldPile.Count > 0)
            {
                Shuffle();
                Draw(count);
                return;
            }
            else
            {
                return;
            }
        }
        else
        {
            CardContent card = deckPile[0];
            deckPile.Remove(card);
            handPile.Add(card);
            count--;
            card.OnDraw();
            if (count > 0) { Draw(count); } else { return; }
        }
    }




    /// <summary>
    /// 洗牌方法
    /// </summary>
    private void Shuffle()
    {
        List<CardContent> temp = new List<CardContent>();
        //先将弃牌堆打乱
        foreach (CardContent card in foldPile)
        {
            temp.Insert(Random.Range(0, temp.Count), card);
        }
        //将所有弃牌放入牌堆中
        foreach (CardContent card in temp)
        {
            deckPile.Add(card);
        }
        //清空弃牌堆
        foldPile.Clear();
    }




    /// <summary>
    /// 战斗状态切换的方法
    /// </summary>
    /// <param name="status">实例化一个新的战斗状态出来</param>
    public void ChangeStatus(ABattleState status)
    {
        this.mBattleStatus = status;
        status.Start();
    }




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
    /// 触发BUFF效果
    /// </summary>
    public void BuffTrigger()
    {

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
