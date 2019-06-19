using UnityEngine;
using System;
using SYHX.Buff;

public class BattleCharacter : MonoBehaviour
{
    protected bool isAlive;
    [SerializeField] protected int maxHp;
    public int currentHp;
    public int attack;
    public int defence;
    public int barrier;
    public float attackRate { get; protected set; }
    public float defenceRate { get; protected set; }
    // public Canvas canvas;
    public event Action onDeath = delegate { };
    public BuffManager buffs;
    void Awake()
    {
        buffs = new BuffManager(this);
        ChildAwake();
    }

    public virtual void RefreshUI() { }

    //可能消失
    public virtual void ChildAwake() { }
    public void ChangeAttackRate(float value)
    {
        attackRate += value;
    }
    public void ChangeDefenceRate(float value)
    {
        defenceRate += value;
    }
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
        RefreshUI();
        if (currentHp <= 0)
        {
            currentHp = 0;
            RefreshUI();
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

    public virtual void GiveDamage(BattleCharacter bc, int damage, DamageTrigger trigger)
    {
        bc.TakeDamage(this, damage);
    }

    public virtual void TakeDamage(BattleCharacter bc, int damage)
    {
        this.DecreaseHp(damage);
    }
    public virtual void TakeNoSourceDamage(int damage)
    {
        this.DecreaseHp(damage);
    }

    public virtual void GetBarrier(float ratio)
    {
        barrier += (int)(defence * ratio * (1 + defenceRate));
        RefreshUI();
    }
}

