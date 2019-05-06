using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : ScriptableObject
{
    protected Enemy enemy;
    public virtual void Execute() { }

    public void SetEnemy(Enemy enemy)
    {
        if (this.enemy == null) this.enemy = enemy;
    }
}
