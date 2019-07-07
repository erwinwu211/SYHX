using SYHX.Cards;

public class BattleInfoManager : Assitant<BattleManager>
{
    //可能弃用
    public BattleInfoManager(BattleManager bm, int currentEP, int maxEP) : base(bm)
    {
        this.currentEP = currentEP;
        this.maxEP = maxEP;
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
        owner.currentEPUI.text = $"{currentEP.ToString()}";
        owner.maxEPUI.text = $"{maxEP.ToString()}";
    }

    public void AddTurn()
    {
        TurnCount++;
        owner.roundText.text = $"第{TurnCount}回合";
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
    }


}
