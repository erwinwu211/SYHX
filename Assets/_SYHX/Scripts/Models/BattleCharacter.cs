using UnityEngine;
using System;
using SYHX.AbnormalStatus;
using TMPro;


public partial class BattleCharacter : MonoBehaviour
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
    public AbnormalStatusManager asManager;

    public TextMeshProUGUI statusText;
    void Start()
    {
        asManager = new AbnormalStatusManager(this);
        ChildAwake();
    }
    public virtual void ShowStatus()
    {
        statusText.text = asManager.GetText();
    }

    public virtual void RefreshUI() { }

    //可能消失
    public virtual void ChildAwake() { }




    #region 生命相关
    /// <summary>
    /// 死亡
    /// </summary>
    public virtual void Death()
    {
        onDeath();
        isAlive = false;
        var enemy = this as Enemy;
        if(enemy != null)
        {
            BattleCharacterManager.RemoveEnemy(enemy);
        }
        var hero = this as BattleHero;
        if(hero != null)
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

    #region 伤害相关

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
    public void ChangeAttackRate(float value)
    {
        attackRate += value;
    }
    public void ChangeDefenceRate(float value)
    {
        defenceRate += value;
    }
    #endregion
    #endregion
}

