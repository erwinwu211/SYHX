using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : ScriptableObject
{
    public GameObject go;
    public List<Enemy> enemyList;

    private float difficultLevel;
    public EnemyGroup(int id, float difficultLevel)
    {
        this.difficultLevel = difficultLevel;
    }
    public void CreateEnemyGroup()
    {
        foreach (var enemy in enemyList)
        {
            var enemyGo = GameObject.Instantiate(go);
            var thisEnemy = enemyGo.GetComponent<Enemy>();
            thisEnemy = enemy;
        }
    }

}
