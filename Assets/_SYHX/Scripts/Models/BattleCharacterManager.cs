using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleCharacterManager : Singleton<BattleCharacterManager>
{
    public BattleHero hero { get; private set; }
    public List<Enemy> enemyList { get; private set; }
    public Enemy selectedEnemy { get; private set; }

    public BattleResult result {get; private set;} = BattleResult.Continue;

    public void SetHero(BattleHero hero)
    {
        this.hero = hero;
    }

    public void GenerateEnemyGroup(int id)
    {
        enemyList = EnemyGroupManager.Ins.enemyGroup[id].CreateEnemyGroup();
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
        if(enemy == Ins.selectedEnemy)
        {
            if(Ins.enemyList.Count > 1)
            {
                Ins.SelectEnemy(Ins.enemyList[ (Ins.enemyList.IndexOf(enemy) + 1) % Ins.enemyList.Count]);
            }
        }
        Ins.enemyList.Remove(enemy);
        if(Ins.enemyList.Count == 0)
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

}

