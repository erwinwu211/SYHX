using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum BattleResult
{
    Continue,
    Win,
    Lose,
}

public class BattleManager : SingletonMonoBehaviour<BattleManager>
{
    #region 人物寻找
    [SerializeField] public BattleHero hero;
    public Enemy selectedEnemy => BattleCharacterManager.Ins.selectedEnemy;
    public List<Enemy> enemyList => BattleCharacterManager.Ins.enemyList;
    #endregion
    public CardManager cardManager;
    // public BattleInfoManager biManager;
    public TurnManager turnManager => TurnManager.Ins;

    public Text roundText;
    public int TurnCount { get; set; }

    protected override void UnityAwake()
    {
        BattleCharacterManager.Ins.SetHero(hero);
        BattleCharacterManager.Ins.RefreshHeroUI();
    }
    void Start()
    {
        BattleStart(0, null);
    }

    /// <summary>
    /// 战斗开始，战斗场景的入口
    /// </summary>
    /// <param name="id">战斗数据的键值</param>
    /// <param name="context">将GameManager作为上下文传入</param>
    public void BattleStart(int id, GameManager context)
    {
        //读取战斗数据
        BattleCharacterManager.Ins.GenerateEnemyGroup(id);
        BattleCharacterManager.Ins.hero.bInfo.ResetCardType();
        BattleCharacterManager.Ins.hero.bInfo.ResetConnection();
        BattleCharacterManager.Ins.RefreshEnemyUI();
    }



    /// <summary>
    /// 战斗结束，做BattleManager的收尾工作
    /// </summary>
    public void BattleEnd()
    {
        // mBattleStatus = null;
        BattleCharacterManager.Ins.Destroy();
        BattleProgressEvent.Ins.Destroy();
    }


    public void AddTurn()
    {
        TurnCount++;
        roundText.text = $"第{TurnCount}回合";
    }
    public int GetEP() => BattleCharacterManager.Ins.hero.bInfo.currentEP;
    public void TurnStartDraw() => Draw(BattleCharacterManager.Ins.hero.bInfo.DrawCountPerTurn);
    public void Draw(int count) => CardManager.Ins.Draw(count);
    public void Shuffle() => CardManager.Ins.Shuffle();
    public void EnergyPointRegain() => BattleCharacterManager.Ins.hero.bInfo.EnergyPointRegain();
    public void ChangeEnergy(int ep) => BattleCharacterManager.Ins.hero.bInfo.ChangeEnergy(ep);
    public void CalculateConnection(CardType type, int count) => BattleCharacterManager.Ins.hero.bInfo.CalculateConnection(type, count);
    public void RegainMoreEnergyPointNextTurn(int count) => BattleCharacterManager.Ins.hero.bInfo.RegainMoreEnergyPointNextTurn(count);
    public void TurnEnd() => turnManager.EndPlayerTurn();
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
