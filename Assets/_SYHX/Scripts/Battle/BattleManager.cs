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

public partial class BattleManager : SingletonMonoBehaviour<BattleManager>
{
    #region 外部调用
    [SerializeField] private BattleHero hero;
    [SerializeField] public CardSelectorManager cardSelectorManager;
    [SerializeField] public BattleInfoManager biManager;

    public static Enemy selectedEnemy => BattleCharacterManager.Ins.selectedEnemy;
    public static List<Enemy> enemyList => BattleCharacterManager.Ins.enemyList;
    public static BattleCharacter Hero => BattleCharacterManager.Ins.hero;
    public TurnManager turnManager => TurnManager.Ins;
    public static bool canExeNextEnemy = true;
    public static bool finishEnemyAction = false;
    #endregion


    #region  UI
    public Button result;
    public TextMeshProUGUI resultUI;
    //可能弃用
    public int currentEP;
    public int maxEP;
    #endregion
    protected override void UnityAwake()
    {
        CardSelectorManager.SetIns(cardSelectorManager);
        result.gameObject.SetActive(false);
        BattleCharacterManager.Ins.SetHero(hero);
        this.signals = new SignalController();
    }
    void Start()
    {
        biManager.Initial(currentEP, maxEP);
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


    /// <summary>
    /// 检测战斗是否结束
    /// 检测方式：
    /// 是否所有敌人均已死亡
    /// 是否角色死亡
    /// </summary>
    /// <returns></returns>
    public void EndBattle()
    {
        StartCoroutine(Result());
    }
    private IEnumerator Result()
    {
        yield return new WaitForSeconds(0.1f);
        switch (BattleCharacterManager.Ins.result)
        {
            case BattleResult.Win:
                Win();
                break;
            case BattleResult.Lose:
                Lose();
                break;
            default:
                break;
        }
        yield break;
    }
    private void Win()
    {
        resultUI.text = "you win";
        result.gameObject.SetActive(true);
    }
    private void Lose()
    {
        resultUI.text = "you lose";
        result.gameObject.SetActive(true);
    }
}


public partial class BattleManager
{

    public static void sAddTurn() => Ins.biManager.AddTurn();
    public static int sGetEP() => Ins.biManager.currentEP;
    public static void sTurnStartDraw() => ManagedCoroutineWithLock(sIDraw(null, Ins.biManager.DrawCountPerTurn), CoroutineType.playerTurnStart);
    public static void sDraw(int count) => BattleCardManager.Ins.Draw(count);
    public static IEnumerator sIDraw(List<CardContent> outlist, int count)
    {
        yield return BattleCardManager.Ins.Draw(outlist, count);
        yield break;
    }
    public static void sShuffle() => BattleCardManager.Ins.Shuffle();
    public static void sEnergyPointRegain() => Ins.biManager.EnergyPointRegain();
    public static void sChangeEnergy(int ep) => Ins.biManager.ChangeEnergy(ep);
    public static void sRegainMoreEnergyPointNextTurn(int count) => Ins.biManager.RegainMoreEnergyPointNextTurn(count);
    public static void sTurnEnd() => Ins.turnManager.EndPlayerTurn();
    public static void sResult() => Ins.turnManager.Result();
    public static void sStartEnemyAction() => BattleCharacterManager.Ins.StartEnemyAction();
    public static void sDiscardAll() => BattleCardManager.Ins.DiscardAll();
    public void TurnEnd() => sTurnEnd();
}

//协程相关

public partial class BattleManager
{
    public static void ManagedCoroutine(IEnumerator enumarator) => Ins.StartCoroutine(enumarator);
    public static Dictionary<CoroutineType, List<IEnumerator>> coroutineLock = new Dictionary<CoroutineType, List<IEnumerator>>();



    //回合处理时使用
    public static void ManagedCoroutineWithLock(IEnumerator enumarator, CoroutineType type)
    {
        if (coroutineLock.ContainsKey(type))
        {
            coroutineLock[type].Add(enumarator);
        }
        else
        {
            var list = new List<IEnumerator>();
            list.Add(enumarator);
            coroutineLock.Add(type, list);
            Ins.StartCoroutine(Ins.LockedCoroutine(type));
        }
    }
    private IEnumerator LockedCoroutine(CoroutineType type)
    {
        while (coroutineLock[type].Count > 0)
        {
            yield return coroutineLock[type][0];
            coroutineLock[type].RemoveAt(0);
        }
        coroutineLock.Remove(type);
    }
}

public partial class BattleManager
{
    public SignalController signals;
}

public enum CoroutineType
{
    enemyOnGoing, canExeNextEnemy, finishEnemyAction, playerTurnStart
}
