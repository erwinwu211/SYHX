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

    //回合数计数器
    public int RountCount { get; set; }

    //能量值计数器
    public int correctEP { get; set; }
    public int maxEP { get; set; }
    public int moreEP { get; set; }

    //卡牌存储区域
    //牌库
    private List<ICard> deckPile;
    //手牌
    private List<ICard> handPile;
    //当前使用区
    private List<ICard> usedPile;
    //弃牌堆
    private List<ICard> foldPile;

    //每回合抽卡数量
    private int drawCountPerTurn = 5;
    public int DrawCountPerTurn {
        get
        {
            return drawCountPerTurn;
        }
        set
        {
            drawCountPerTurn = value;
        }
     }

    public Hero hero;


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
        //重置各卡牌存储空间
        deckPile = new List<ICard>();
        handPile = new List<ICard>();
        usedPile = new List<ICard>();
        foldPile = new List<ICard>();

        //战斗状态切换
        ChangeStatus(new InitializationStatus(this));
    }

    public void BattleEnd()
    {
        mBattleStatus = null;
    }

    //抽牌
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
            ICard card = deckPile[0];
            deckPile.Remove(card);
            handPile.Add(card);
            count--;
            card.OnDraw();
            if (count > 0) { Draw(count); } else { return; }
        }
    }

    //洗牌
    private void Shuffle()
    {
        List<ICard> temp = new List<ICard>();
        //先将弃牌堆打乱
        foreach (ICard card in foldPile)
        {
            temp.Insert(Random.Range(0, temp.Count), card);
        }
        //将所有弃牌放入牌堆中
        foreach (ICard card in temp)
        {
            deckPile.Add(card);
        }
        //清空弃牌堆
        foldPile.Clear();
    }



    //状态切换的方法
    public void ChangeStatus(ABattleStatus status)
    {
        this.mBattleStatus = status;
        status.Start();
    }

    //回复能量
    public void EnergyPointRegain()
    {
        //当前能量=上限
        correctEP = maxEP;

        //若有额外恢复值，再继续添加
        if (moreEP>0)
        {
            correctEP += moreEP;
        }
    }

    //触发BUFF效果
    public void BuffTrigger()
    {

    }

    //检测战斗是否结束
    public bool IsBattleOver()
    {
        if (false)
            return true;
        return false;
    }
}
