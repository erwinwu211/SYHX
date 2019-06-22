using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : ScriptableObject
{
    [SerializeField] public List<EnemySource> enemyList;

    private float difficultLevel;
    public EnemyGroup(int id, float difficultLevel)
    {
        this.difficultLevel = difficultLevel;
    }
    public List<Enemy> CreateEnemyGroup()
    {
        var generatedEnemyList = new List<Enemy>();
        var i = 0;
        var enemyParent = EnemyGroupManager.Ins.EnemyParent;
        foreach (var enemy in enemyList)
        {
            var enemyGo = enemy.enemyObject.Create(enemyParent);
            enemyGo.transform.localPosition += new Vector3(0f, 0f, i * -60f);
            var thisEnemy = enemyGo.GetComponent<Enemy>();
            thisEnemy.SetEnemy(enemy);
            generatedEnemyList.Add(thisEnemy);
            i++;
        }
        return generatedEnemyList;
    }

}
