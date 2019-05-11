using UnityEngine;
using System.Collections;

public abstract class EnemyActionSource : ScriptableObject
{
    public abstract EnemyAction GenerateAction();
}

public abstract class EnemyAction
{
    protected Enemy enemy;
    public abstract IEnumerator Execute();
    public void SetEnemy(Enemy enemy)
    {
        if (this.enemy == null)
        {
            this.enemy = enemy;
            enemy.SetAction(this);
        }
    }
}
