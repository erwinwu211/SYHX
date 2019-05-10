using UnityEngine;

public abstract class EnemyAction : ScriptableObject
{
    protected Enemy enemy;
    public abstract void Execute();

    public void SetEnemy(Enemy enemy)
    {
        if (this.enemy == null) this.enemy = enemy;
    }
}
