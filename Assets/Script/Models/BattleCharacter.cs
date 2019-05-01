using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacter : MonoBehaviour
{
    protected bool isAlive;
    protected int maxHp;
    public int currentHp { get; protected set; }
    protected int attack;
    protected int defence;

    protected int AI_id;


    // public BattleCharacter (int id)
    // {

    // }

    /// <summary>
    /// 死亡
    /// </summary>
    public virtual void Death()
    {
        isAlive = false;
    }

    /// <summary>
    /// 回血
    /// </summary>
    public virtual void IncreaseHp(int count)
    {
        currentHp += count;
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }

    /// <summary>
    /// 掉血
    /// </summary>
    /// <param name="count"></param>
    public virtual void DecreaseHp(int count)
    {
        currentHp -= count;
        if (currentHp <= 0)
        {
            currentHp = 0;
            Death();
        }
    }

    /// <summary>
    /// 返回是否存活
    /// </summary>
    /// <returns></returns>
    public virtual bool IsAlive()
    {
        return isAlive;
    }

    public virtual void GiveDamage(BattleCharacter bc)
    {
        var damage = this.attack - bc.defence;
        bc.TakeDamage(damage);
    }

    public virtual void TakeDamage(int damage)
    {
        this.DecreaseHp(damage);
    }
}
