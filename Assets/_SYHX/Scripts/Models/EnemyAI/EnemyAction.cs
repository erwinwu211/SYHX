using UnityEngine;
using System.Collections;


public abstract class EnemyAction : ScriptableObject
{
    protected Enemy enemy;
    public virtual void Execute()
    {
        BattleManager.enemyOnGoing = true;
        BattleManager.ManagedCoroutine(Decorator());
    }

    IEnumerator Decorator()
    {
        yield return execute();
        BattleManager.enemyOnGoing = false;
        yield break;
    }
    protected abstract IEnumerator execute();

    public void SetEnemy(EnemyAIHandler handler)
    {
        this.enemy = handler.enemy;
    }
}

