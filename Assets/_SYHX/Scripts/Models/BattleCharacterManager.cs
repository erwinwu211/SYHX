using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleCharacterManager : Singleton<BattleCharacterManager>
{

    #region 所有战斗角色
    public BattleHero hero { get; private set; }
    public List<Enemy> enemyList { get; private set; }
    public Enemy selectedEnemy { get; private set; }

    #endregion

    #region 人物初始化

    public void SetHero(BattleHero hero)
    {
        this.hero = hero;
    }

    public void GenerateEnemyGroup(int id)
    {
        enemyList = EnemyGroupManager.Ins.enemyGroup[id].CreateEnemyGroup();
        SelectEnemy(enemyList[0]);
        RefreshAllUI();
    }
    #endregion

    public void SelectEnemy(Enemy enemy)
    {
        if (selectedEnemy != null) selectedEnemy.LeaveSelected();
        selectedEnemy = enemy;
        selectedEnemy.OnSelected();
    }

    #region UI关联

    public void RefreshHeroUI()
    {
        hero.bInfo.RefreshUI();
    }
    public void RefreshEnemyUI()
    {
        foreach (var enemy in enemyList)
        {
            enemy.bInfo.RefreshUI();
        }
    }

    public void RefreshAllUI()
    {
        RefreshHeroUI();
        RefreshEnemyUI();
    }

    #endregion
}
