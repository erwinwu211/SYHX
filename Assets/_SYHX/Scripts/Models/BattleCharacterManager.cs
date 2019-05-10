using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleCharacterManager : Singleton<BattleCharacterManager>
{
    public BattleHero hero { get; private set; }
    public List<Enemy> enemyList { get; private set; }
    public Enemy selectedEnemy { get; private set; }

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

}
