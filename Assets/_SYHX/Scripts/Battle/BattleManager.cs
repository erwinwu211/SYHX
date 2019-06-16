using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using SYHX.Cards;

public enum BattleResult
{
    Continue,
    Win,
    Lose,
}

public class BattleManager : SingletonMonoBehaviour<BattleManager>
{
    [SerializeField] public BattleHero hero;
    public Enemy selectedEnemy => BattleCharacterManager.Ins.selectedEnemy;
    public List<Enemy> enemyList => BattleCharacterManager.Ins.enemyList;
    public BattleCardManager cardManager;
    public BattleInfoManager biManager;
    public TurnManager turnManager => TurnManager.Ins;
    public TextMeshProUGUI currentEPUI;
    public TextMeshProUGUI maxEPUI;
    public Text roundText;

    //可能弃用
    public int currentEP;
    public int maxEP;
    protected override void UnityAwake()
    {
        BattleCharacterManager.Ins.SetHero(hero);
        biManager = new BattleInfoManager(this, currentEP, maxEP);
        biManager.RefreshUI();
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
        biManager.ResetCardType();
        biManager.ResetConnection();
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


    public void AddTurn() => biManager.AddTurn();
    public int GetEP() => biManager.currentEP;
    public void TurnStartDraw() => Draw(biManager.DrawCountPerTurn);
    public void Draw(int count) => BattleCardManager.Ins.Draw(count);
    public void Shuffle() => BattleCardManager.Ins.Shuffle();
    public void EnergyPointRegain() => biManager.EnergyPointRegain();
    public void ChangeEnergy(int ep) => biManager.ChangeEnergy(ep);
    public void CalculateConnection(CardType type, int count) => biManager.CalculateConnection(type, count);
    public void RegainMoreEnergyPointNextTurn(int count) => biManager.RegainMoreEnergyPointNextTurn(count);
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

