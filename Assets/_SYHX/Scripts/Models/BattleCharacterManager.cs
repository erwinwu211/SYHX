using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleCharacterManager : SingletonMonoBehaviour<BattleCharacterManager>
{

    protected override void UnityAwake() { }
    public BattleHero hero { get; private set; }
    public List<Enemy> enemyList { get; private set; }
    public Enemy selectedEnemy { get; private set; }
    public BattleResult result { get; private set; } = BattleResult.Continue;

    public void SetHero(BattleHero hero)
    {
        this.hero = hero;
    }


    public void GenerateEnemyGroup(int id, float difficultLevel)
    {
        enemyList = EnemyGroupManager.Ins.enemyGroup[id].CreateEnemyGroup(difficultLevel);
        SelectEnemy(enemyList[0]);
    }

    public void GenerateEnemyGroup(EnemyGroup group, float difficultLevel)
    {
        enemyList = group.CreateEnemyGroup(difficultLevel);
        SelectEnemy(enemyList[0]);
    }

    public void SelectEnemy(Enemy enemy)
    {
        if (selectedEnemy != null) selectedEnemy.LeaveSelected();
        selectedEnemy = enemy;
        selectedEnemy.OnSelected();
    }

    public static void RemoveEnemy(Enemy enemy)
    {
        if (enemy == Ins.selectedEnemy)
        {
            if (Ins.enemyList.Count > 1)
            {
                Ins.SelectEnemy(Ins.enemyList[(Ins.enemyList.IndexOf(enemy) + 1) % Ins.enemyList.Count]);
            }
        }
        Ins.enemyList.Remove(enemy);
        if (Ins.enemyList.Count == 0)
        {
            Ins.result = BattleResult.Win;
            BattleManager.sResult();
        }
    }

    public static void RemoveHero()
    {
        Ins.result = BattleResult.Lose;
        BattleManager.sResult();
    }

    public void StartEnemyAction()
    {
        BattleManager.finishEnemyAction = false;
        BattleManager.ManagedCoroutine(AllEnemyAction());
    }
    IEnumerator AllEnemyAction()
    {
        foreach (var enemy in enemyList)
        {
            BattleManager.canExeNextEnemy = false;
            enemy.ExecuteAction();
            yield return new WaitUntil(() => BattleManager.canExeNextEnemy == true);
            enemy.NextAI();
            enemy.RefreshUI();
        }
        BattleManager.finishEnemyAction = true;
        yield break;
    }
}

