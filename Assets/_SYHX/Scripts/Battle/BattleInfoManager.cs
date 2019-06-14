using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInfoManager : Assitant<BattleCharacter>
{
    //可能弃用
    public BattleInfoManager(BattleCharacter bc, int currentEP, int maxEP) : base(bc)
    {
        this.currentEP = currentEP;
        this.maxEP = maxEP;
    }
    //每回合抽卡数量
    private int drawCountPerTurn = 5;
    public int DrawCountPerTurn => drawCountPerTurn;

    public void RefreshUI()
    {
        owner.currentEPUI.text = $"{currentEP.ToString()}";
        owner.maxEPUI.text = $"{maxEP.ToString()}";
    }

    #region 能量相关

    //能量值计数器
    public int currentEP { get; private set; }
    public int maxEP { get; private set; }
    public int moreEP { get; private set; }
    /// <summary>
    /// 回复能量值的方法
    /// </summary>
    public void EnergyPointRegain()
    {
        //当前能量=上限
        currentEP = maxEP;

        //若有额外恢复值，再继续添加
        if (moreEP > 0)
        {
            currentEP += moreEP;
            moreEP = 0;
        }
        RefreshUI();
    }

    public void ChangeEnergy(int ep)
    {
        currentEP += ep;
        RefreshUI();
    }
    /// <summary>
    /// 在下一回合回复额外能量值的方法
    /// </summary>
    /// <param name="count"></param>
    public void RegainMoreEnergyPointNextTurn(int count)
    {
        moreEP = count;
    }
    #endregion

    #region  卡牌颜色相关
    public CardType currentType { get; private set; }
    public int cardConnectionCount { get; private set; }
    public void CalculateConnection(CardType type, int count)
    {
        if (type == CardType.连接技)
        {
            this.cardConnectionCount += count;
            return;
        }
        currentType = type;
        cardConnectionCount = currentType == type ? cardConnectionCount + count : 0;
    }
    public void ResetCardType()
    {
        currentType = CardType.连接技;
    }

    public void ResetConnection()
    {
        cardConnectionCount = 0;
    }

    #endregion

}
