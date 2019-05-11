using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleCharacterManager : Singleton<BattleCharacterManager>
{
    public BattleHero hero { get; private set; }
    public List<Enemy> enemyList { get; private set; }
    public Enemy selectedEnemy { get; private set; }
    private BattleManager bm => BattleManager.Ins;
    public bool endAction { get; private set; }

    public void SetHero(BattleHero hero)
    {
        this.hero = hero;
    }

    public void GenerateEnemyGroup(int id)
    {
        enemyList = EnemyGroupManager.Ins.enemyGroup[id].CreateEnemyGroup();
        SelectEnemy(enemyList[0]);
        foreach (var enemy in enemyList)
        {
            enemy.StartAI();
        }
        GetNextAction();
    }

    public void SelectEnemy(Enemy enemy)
    {
        if (selectedEnemy != null) selectedEnemy.LeaveSelected();
        selectedEnemy = enemy;
        selectedEnemy.OnSelected();
    }

    public void GetNextAction()
    {
        foreach (var enemy in enemyList)
        {
            enemy.NextAI();
        }
    }

    public void ExecuteEnemyAction()
    {
        this.endAction = false;
        bm.CoroutineUtil(EnemyAction());
    }
    public IEnumerator EnemyAction()
    {
        foreach (var enemy in enemyList)
        {
            if (enemy != null && enemy.IsAlive())
                yield return bm.CoroutineUtil(enemy.Execute());
            yield return new WaitForSeconds(0.5f);
        }
        this.endAction = true;
        yield break;
    }
}
