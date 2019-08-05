using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyGroup : ScriptableObject
{
    [SerializeField] public List<EnemySource> enemyList;
    [SerializeField] public List<RewardProto> rewardList;

    private float difficultLevel;
    public EnemyGroup(int id, float difficultLevel)
    {
        this.difficultLevel = difficultLevel;
    }
    public List<Enemy> CreateEnemyGroup(float difficultLevel)
    {
        this.difficultLevel = difficultLevel;
        var generatedEnemyList = new List<Enemy>();
        var i = 0;
        var enemyParent = EnemyGroupManager.Ins.EnemyParent;
        foreach (var enemy in enemyList)
        {
            AdaptDifficultLevel(enemy);
            var enemyGo = enemy.enemyObject.Create(enemyParent);
            enemyGo.transform.localPosition += new Vector3((i + 1) * -200f, 0f, 0f);
            var thisEnemy = enemyGo.GetComponent<Enemy>();
            thisEnemy.SetEnemy(enemy);
            generatedEnemyList.Add(thisEnemy);
            i++;
        }
        return generatedEnemyList;
    }

    private void AdaptDifficultLevel(EnemySource enemy)
    {
        enemy.attack = (int)(enemy.attack * difficultLevel);
        enemy.defence = (int)(enemy.defence * difficultLevel);
        enemy.maxHp = (int)(enemy.maxHp * difficultLevel);
        enemy.currentHp = enemy.maxHp;
    }

}
