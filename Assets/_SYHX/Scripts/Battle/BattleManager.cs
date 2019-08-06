using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using SYHX.Cards;
using DG.Tweening;

public enum BattleResult
{
    Continue,
    Win,
    Lose,
}

public partial class BattleManager : SingletonMonoBehaviour<BattleManager>
{

    private BattleHero hero;
    [SerializeField] public CardSelectorManager cardSelectorManager;
    [SerializeField] public BattleInfoManager biManager;
    [SerializeField] public GameObject heroParent;
    [SerializeField] public BattleResultPanelUI battleResultPanelUI;
    public static PassedBattleInformation information;

    #region 外部调用
    public static Enemy selectedEnemy => BattleCharacterManager.Ins.selectedEnemy;
    public static List<Enemy> enemyList => BattleCharacterManager.Ins.enemyList;
    public static BattleCharacter Hero => BattleCharacterManager.Ins.hero;
    public TurnManager turnManager => TurnManager.Ins;
    public static bool canExeNextEnemy = true;
    public static bool finishEnemyAction = false;
    private PassedResultInformation resultInformation;
    #endregion


    #region  UI
    public GameObject resultPanel;
    public GameObject rewardPanel;
    //public TextMeshProUGUI resultUI;
    #endregion
    protected override void UnityAwake()
    {
        CardSelectorManager.SetIns(cardSelectorManager);
        resultPanel.gameObject.SetActive(false);
        BattleHero tempHero;
        if (CharacterInDungeon.Ins == null)
        {
            tempHero = Initializer.Ins.defaultHero;
        }
        else
        {
            tempHero = Initializer.Ins.GetHero(CharacterInDungeon.Ins.character);
        }
        hero = tempHero.gameObject.CreateWithoutChange(heroParent).GetComponent<BattleHero>();
        hero.Init();
        BattleCharacterManager.Ins.SetHero(hero);
        this.signals = new SignalController();
    }
    void Start()
    {
        biManager.Initial(hero.maxEp, hero.maxEp);
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
        BattleCharacterManager.Ins.GenerateEnemyGroup(information.enemyGroup, information.difficultLevel);
        //创建新的战斗结果数据
        resultInformation = new PassedResultInformation();
        resultInformation.cardSourceRward = new List<CardSource>();
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
        //写入部分结果传递
        resultInformation.currentHp = hero.currentHp;
        resultInformation.win = true;
        resultInformation.resourceReward = information.enemyGroup.rewardList.dungeonResourceReward;
        battleResultPanelUI.ShowWinPanel(information.enemyGroup.rewardList);
    }
    private void Lose()
    {
        resultInformation.currentHp = 0;
        resultInformation.win = false;
        battleResultPanelUI.ShowLosePanel();
    }


    /// <summary>
    /// 返回地宫界面并将信息传给DungeonManager
    /// </summary>
    public void ReturnToDungeon()
    {
        DungeonManager.Ins.DealWithBattleResult(resultInformation);
        SceneStatusManager.Ins.SetSceneStatus(SceneStatusManager.Ins.Record);
    }

    /// <summary>
    /// 奖励界面获取下一个奖励
    /// </summary>
    public void NextReward()
    {
        DungeonCardRewardSource dcrs = information.enemyGroup.rewardList.dungeonCardRewardGroup;
        //如果敌人群的奖励列表中有卡牌奖励的话
        if (dcrs)
        {
            battleResultPanelUI.ShowCardChoosePanel(dcrs.GetCardSourceInPool(3));
        }
        else
        {
            ReturnToDungeon();
        }
    }

    /// <summary>
    /// 设置玩家所选的要加入构筑中的奖励卡牌
    /// </summary>
    /// <param name="cs"></param>
    public void SetRewardCardSource(CardSource cs)
    {
        if (cs) resultInformation.cardSourceRward.Add(cs);
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
        yield return BattleCardManager.Ins.IDraw(outlist, count);
        yield break;
    }
    public static void sShuffle() => BattleCardManager.Ins.IShuffle();
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

public struct PassedBattleInformation
{
    public EnemyGroup enemyGroup;
    public float difficultLevel;
}

/// <summary>
/// 传递战斗结果的结构体。包含是否胜利，当前血量，奖励资源与奖励卡牌
/// </summary>
public struct PassedResultInformation
{
    public int currentHp;
    public bool win;
    public List<DungeonResourceReward> resourceReward;
    public List<CardSource> cardSourceRward;
}
