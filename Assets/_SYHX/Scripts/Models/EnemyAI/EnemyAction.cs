using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System.Diagnostics;


public abstract class EnemyActionSource : ScriptableObject, Source
{
    public string desc;
    protected Enemy enemy;
    public abstract void Init();
    public abstract EnemyActionContent GeneratedAction();
    public void ParseEnemy(Enemy enemy) => this.enemy = enemy;

}

public class EnemyActionSource<T> : EnemyActionSource
where T : EnemyActionContent, new()
{
    public T origin;
    private Dictionary<string, PropertyInfo> descOption;

    public override void Init() => descOption = this.InitDescOption<EnemyActionSource, T>();
    public override EnemyActionContent GeneratedAction()
    {
        var action = this.GenerateContent<EnemyActionSource, T>(origin);
        action.SetInformation(enemy, desc, descOption);
        return action;
    }
}

public abstract class EnemyActionContent : Content
{
    protected Enemy enemy;
    public virtual void Execute()
    {
        BattleManager.ManagerCoroutine(Decorator());
    }

    IEnumerator Decorator()
    {
        yield return execute();
        BattleManager.canExeNextEnemy = true;
        yield break;
    }
    protected abstract IEnumerator execute();

    public void SetInformation(Enemy enemy, string desc, Dictionary<string, PropertyInfo> descOption = null)
    {
        this.desc = desc;
        this.enemy = enemy;
        this.descOption = descOption;
    }
    private Dictionary<string, PropertyInfo> descOption;
    private string desc;
    public string Desc => GetDesc(desc);
    public virtual string GetDesc(string desc)
    {
        foreach (KeyValuePair<string, PropertyInfo> pairs in descOption)
        {
            desc = desc.Replace("{" + pairs.Key + "}", (string)pairs.Value.GetValue(this));
        }
        return desc;
    }
}
