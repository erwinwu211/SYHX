using UnityEngine;
using System;
using SYHX.AbnormalStatus;
using TMPro;
using Sirenix.OdinInspector;


public partial class BattleCharacter : MonoBehaviour
{
    protected bool isAlive;
    [SerializeField] protected int maxHp;
    public int currentHp;
    public int attack;
    public int defence;
    public int barrier;
    public float attackRate { get; set; }
    public int optionAttack { get; set; }
    [ShowInInspector] public int Attack => attack + optionAttack;
    public float receiveRate { get; set; }
    public float defenceRate { get; set; }
    public virtual bool isEnemy { get; }
    // public Canvas canvas;
    public event Action onDeath = delegate { };
    public AbnormalStatusManager asManager;

    [SerializeField] public Transform damageTrans;

    public TextMeshProUGUI statusText;
    void Start()
    {
        asManager = new AbnormalStatusManager(this);
        ChildStart();
        RefreshUI();
    }
    public virtual void ShowStatus()
    {
        statusText.text = asManager.GetText();
    }

    public virtual void RefreshUI() { }

    //可能消失
    public virtual void ChildStart() { }




    #region 生命相关
    /// <summary>
    /// 死亡
    /// </summary>
    public virtual void Death()
    {
        onDeath();
        isAlive = false;
        var enemy = this as Enemy;
        if (enemy != null)
        {
            BattleCharacterManager.RemoveEnemy(enemy);
        }
        var hero = this as BattleHero;
        if (hero != null)
        {
            BattleCharacterManager.RemoveHero();
        }
        Destroy(this.gameObject);
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
        RefreshUI();
    }

    /// <summary>
    /// 掉血
    /// </summary>
    /// <param name="count"></param>
    public virtual int DecreaseHp(int count)
    {
        if (count <= 0) return 0;
        var temp = currentHp;
        currentHp -= count;
        RefreshUI();
        if (currentHp <= 0)
        {
            currentHp = 0;
            RefreshUI();
            Death();
        }
        return temp - currentHp;
    }

    /// <summary>
    /// 返回是否存活
    /// </summary>
    /// <returns></returns>
    public virtual bool IsAlive()
    {
        return isAlive;
    }

    #region 伤害相关

    //攻击 = 角色攻击力 * 卡牌百分比 * 我方buff属性
    public virtual int GiveDamage(BattleCharacter bc, float damageRate, DamageTrigger trigger)
    {
        return bc.TakeDamage(this, Attack * damageRate * (1 + attackRate));
    }

    //攻击 = 上述 * 敌方buff属性
    public virtual int TakeDamage(BattleCharacter bc, float damage)
    {
        damage = damage * (1 + receiveRate);
        var temp = (int)damage;
        temp -= barrier;
        barrier -= (int)damage;
        if (barrier < 0)
        {
            barrier = 0;
        }
        else if (temp <= 0)
        {
            temp = 0;
        }
        RefreshUI();
        return this.DecreaseHp(temp);
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
    public virtual void GetBarrier(int value)
    {
        barrier += value;
        RefreshUI();
    }
    #endregion
    #endregion

    public event Action onAddAStatus = delegate { };
    public void OnAddAStatus()
    {
        onAddAStatus();
    }
}

