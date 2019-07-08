using SYHX.Cards;
using UnityEngine;
using UnityEngine.UI;

public class BattleInfoManager : MonoBehaviour
{
    public Text currentEPUI;
    public Text maxEPUI;

    public Text moreEPUI;
    public Text roundText;
    //可能弃用
    public void Initial(int currentEP, int maxEP)
    {
        this.TurnCount = 0;
        this.currentEP = currentEP;
        this.maxEP = maxEP;
        RefreshUI();
    }
    //每回合抽卡数量
    private int drawCountPerTurn = 5;
    public int DrawCountPerTurn => drawCountPerTurn;
    //回合数计数器
    public int TurnCount { get; set; }
    //能量值计数器
    public int currentEP { get; private set; }
    public int maxEP { get; private set; }
    public int moreEP { get; private set; }

    public void RefreshUI()
    {
        currentEPUI.text = $"{currentEP.ToString()}";
        maxEPUI.text = $"{maxEP.ToString()}";
        roundText.text = $"第{TurnCount}回合";
        if (moreEP > 0)
        {
            moreEPUI.text = $"+{moreEP}";
        }
        else if (moreEP < 0)
        {
            moreEPUI.text = $"{moreEP}";
        }
        else
        {
            moreEPUI.text = "";
        }
    }

    public void AddTurn()
    {
        TurnCount++;
        RefreshUI();
    }
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
        RefreshUI();
    }
}
