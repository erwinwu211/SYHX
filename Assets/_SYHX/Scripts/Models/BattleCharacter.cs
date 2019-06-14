using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SYHX.Buff;
using TMPro;

public class BattleCharacter : MonoBehaviour
{
    protected bool isAlive;
    [SerializeField] protected int maxHp;
    public int currentHp;
    public int attack;
    protected int defence;
    public int currentEP;
    public int maxEP;
    public float attackRate { get; protected set; }
    public float defenceRate { get; protected set; }
    // public Canvas canvas;
    public TextMeshProUGUI currentEPUI;
    public TextMeshProUGUI maxEPUI;

    public BattleInfoManager bInfo;

    public event Action<BattleCharacter, int> onTakeDamage = delegate { };
    public event Action<BattleCharacter> onGiveDamage = delegate { };
    public event Action onDeath = delegate { };
    public Buffs buffs;
    void Awake()
    {
        buffs = new Buffs(this);
        bInfo = new BattleInfoManager(this, currentEP, maxEP);
        ChildAwake();
    }


    //可能消失
    public virtual void ChildAwake()
    {
        // canvas.worldCamera = Camera.allCameras[0];
    }

    #region  HP相关



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
    /// 死亡
    /// </summary>
    public virtual void Death()
    {
        onDeath();
        isAlive = false;
    }

    /// <summary>
    /// 返回是否存活
    /// </summary>
    /// <returns></returns>
    public virtual bool IsAlive()
    {
        return isAlive;
    }

    public void ChangeAttackRate(float value)
    {
        attackRate += value;
    }
    public void ChangeDefenceRate(float value)
    {
        defenceRate += value;
    }

    #region 伤害相关

    public virtual void GiveDamage(BattleCharacter bc, int damage, DamageTrigger trigger)
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
    #endregion

    #endregion

    #region EP代理

    public void EnergyPointRegain() => bInfo.EnergyPointRegain();
    public void ChangeEnergy(int ep) => bInfo.ChangeEnergy(ep);
    public void RegainMoreEnergyPointNextTurn(int count) => bInfo.RegainMoreEnergyPointNextTurn(count);

    #endregion

    #region 卡牌颜色代理
    public void CalculateConnection(CardType type, int count) => bInfo.CalculateConnection(type, count);
    #endregion

}

