﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : ScriptableObject
{
    public GameObject go;
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
            var enemyGo = go.Create(enemyParent);
            enemyGo.transform.localPosition += new Vector3(0f, 0f, i * -0.3f);
            var thisEnemy = enemyGo.GetComponent<Enemy>();
            thisEnemy.SetEnemy(enemy);
            generatedEnemyList.Add(thisEnemy);
            i++;
        }
        return generatedEnemyList;
    }

}