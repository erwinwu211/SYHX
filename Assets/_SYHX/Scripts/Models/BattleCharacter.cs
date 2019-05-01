using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleCharacter : MonoBehaviour
{
    protected bool isAlive;
    protected int maxHp;
    public int currentHp { get; protected set; }
    protected int attack;
    protected int defence;

    protected int AI_id;

    public event Action<BattleCharacter, int> onTakeDamage = delegate { };
    public event Action<BattleCharacter> onGiveDamage = delegate { };
    public event Action onDeath = delegate { };

    /// <summary>
    /// 死亡
    /// </summary>
    public virtual void Death()
    {
        onDeath();
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

    public virtual void GiveDamage(BattleCharacter bc, int damage)
    {
        onGiveDamage(bc);
        bc.TakeDamage(this, damage);
    }

    public virtual void TakeDamage(BattleCharacter bc, int damage)
    {
        onTakeDamage(bc, damage);
        this.DecreaseHp(damage);
    }
    public virtual void TakeNoSourceDamage(int damage)
    {
        this.DecreaseHp(damage);
    }
}
