using System.Collections.Generic;
using UnityEngine;
using SYHX.Cards;

public enum BasicAttributeType
{
    Force, Aglie, Constitution, Fortune
}

public class BasicAttribute
{
    public BasicAttributeType type;
    public int currentLv;
    public int maxLv = 5;
    public int currentExp;
    public int maxExp => Initializer.Ins.AttrLvInfos[currentLv].RequireCount;
    public bool IsMaxLv() => currentLv>= maxLv;

    public void GainExp(int count)
    {
        if (currentLv >= maxLv) return;
        currentExp+= count;
        if (currentExp > maxExp) LvUp();
    }

    public void LvUp()
    {
        if (currentLv >= maxLv) return;
        currentExp -= maxExp;
        currentLv++;
    }


    public BasicAttribute(BasicAttributeType type,int initLv)
    {
        this.type = type;
        this.currentLv = initLv;
        this.currentExp = 0;
    }
}

/// <summary>
/// 当玩家进入地宫时，根据所选择的人物及其设定所生成的对象
/// 用于记录玩家在地宫中的信息
/// </summary>
public class CharacterInDungeon : SingletonMonoBehaviour<CharacterInDungeon>
{
    public CharacterContent character;
    public int maxLv { get; private set; }
    public int currentLv { get; set; }
    public int maxHp { get; set; }
    public int currentHp { get; set; }
    public int Attack { get; private set; }
    public int Defend { get; private set; }
    public int maxEp { get; private set; }
    public int Draw_count { get; private set; }
    public BasicAttribute Force { get; private set; }
    public BasicAttribute Agile { get; private set; }
    public BasicAttribute Constitution { get; private set; }
    public BasicAttribute Fortune { get; private set; }
    public List<CardContent> Deck { get; private set; }

    public void Start()
    {
        //这里先暂时用一下预设好的人物
        // Umirika umirika = new Umirika();
        // Init(umirika);
    }

    public void Init(CharacterContent cc)
    {
        character = cc;
        this.maxHp = cc.Hp_max;
        this.currentHp = cc.Hp_max;
        this.Attack = cc.Attack;
        this.Defend = cc.Defend;
        this.maxEp = cc.Energy_max;
        this.Draw_count = cc.Draw_count;
        this.Force = new BasicAttribute(BasicAttributeType.Force,cc.Force);
        this.Agile = new BasicAttribute(BasicAttributeType.Aglie,cc.Agile);
        this.Constitution = new BasicAttribute(BasicAttributeType.Constitution,cc.Constitution);
        this.Fortune = new BasicAttribute(BasicAttributeType.Fortune,cc.Fortune);
        this.Deck = new List<CardContent>();
        this.maxLv = cc.currentGrade.LvMax;
        this.currentLv = 1;
        foreach (var cs in cc.Deck)
        {
            this.Deck.Add(cs.GenerateCard());
        }
    }

    public LvUpCheck CheckCanLevelUp(int i)
    {
        Debug.Log("当前人物等级为" + currentLv + " 升级所需芯片核心*" + character.lvInfos[currentLv - 1].RequireCount);
        if (currentLv >= maxLv) return LvUpCheck.max;
        if (i < character.lvInfos[currentLv - 1].RequireCount) return LvUpCheck.cost_unenough;
        return LvUpCheck.yes;
    }

    public string LevelUp()
    {
        if (currentLv >= maxLv) return "已达到最高等级";
        string result = "升到了 " + (currentLv + 1) + " 级";
        DungeonLvInfo _info = character.lvInfos[currentLv - 1];
        if (_info.HpReward != 0)
        {
            result += "最大生命值 +" + _info.HpReward + "\n";
            this.IncreaseHpMaxWithCurrentHp(_info.HpReward);
        }
        if (_info.AttackReward != 0)
        {
            result += "攻击力 +" + _info.AttackReward + "\n";
            this.IncreaseAttack(_info.AttackReward);
        }
        if (_info.DefendReward != 0)
        {
            result += "防御力 +" + _info.DefendReward + "\n";
            this.IncreaseDefend(_info.DefendReward);
        }
        if (_info.EPReward != 0)
        {
            result += "防御力 +" + _info.DefendReward + "\n";
            this.maxEp += _info.EPReward;
        }
        if (_info.DrawCount != 0)
        {
            result += "防御力 +" + _info.DefendReward + "\n";
            this.Draw_count += _info.DrawCount;
        }
        DungeonManager.Ins.chipCore.count -= _info.RequireCount;
        this.currentLv++;
        DungeonManager.Ins.RefreshUI();
        return result;
    }

    #region 属性的增加减少
    /// <summary>
    /// 增加生命最大值
    /// </summary>
    /// <param name="count"></param>
    public void IncreaseHpMax(int count)
    {
        if (count < 0) return;
        maxHp += count;
    }

    /// <summary>
    /// 增加生命最大值的同时增加等量当前生命值
    /// </summary>
    /// <param name="count"></param>
    public void IncreaseHpMaxWithCurrentHp(int count)
    {
        if (count < 0) return;
        maxHp += count;
        currentHp += count;
    }

    /// <summary>
    /// 减少生命最大值
    /// </summary>
    /// <param name="count"></param>
    public void DecreaseHpMax(int count)
    {
        if (count < 0) return;
        maxHp -= count;
        if (maxHp < currentHp)
        {
            currentHp = maxHp;
        }
        if (maxHp <= 0)
        {
            //TODO:SendEvent("DungeonEnd");
        }
    }

    /// <summary>
    /// 增加当前生命值
    /// </summary>
    /// <param name="count"></param>
    public void IncreaseHpCurrect(int count)
    {
        if (count < 0) return;
        currentHp += count;
        if (currentHp > maxHp) currentHp = maxHp;
    }

    /// <summary>
    /// 削减当前生命值
    /// </summary>
    /// <param name="count"></param>
    public void DecreaseHpCurrect(int count)
    {
        if (count < 0) return;
        currentHp -= count;
        if (currentHp <= 0)
        {
            //TODO:SendEvent("DungeonEnd");
        }
    }

    /// <summary>
    /// 增加攻击力
    /// </summary>
    /// <param name="count"></param>
    public void IncreaseAttack(int count)
    {
        if (count < 0) return;
        Attack += count;
    }

    /// <summary>
    /// 减少攻击力
    /// </summary>
    /// <param name="count"></param>
    public void DecreaseAttack(int count)
    {
        if (count < 0) return;
        Attack -= count;
    }

    /// <summary>
    /// 增加防御力
    /// </summary>
    /// <param name="count"></param>
    public void IncreaseDefend(int count)
    {
        if (count < 0) return;
        Defend += count;
    }

    /// <summary>
    /// 减少防御力
    /// </summary>
    /// <param name="count"></param>
    public void DecreaseDefend(int count)
    {
        if (count < 0) return;
        Defend -= count;
    }

    /// <summary>
    /// 获得基础属性的经验
    /// </summary>
    /// <param name="attr"></param>
    /// <param name="count"></param>
    public void IncreaseBasicAttributeExp(BasicAttributeType attr, int count)
    {
        switch (attr)
        {
            case BasicAttributeType.Force:
                this.Force.GainExp(count);
                break;
            case BasicAttributeType.Aglie:
                this.Agile.GainExp(count);
                break;
            case BasicAttributeType.Constitution:
                this.Constitution.GainExp(count);
                break;
            case BasicAttributeType.Fortune:
                this.Fortune.GainExp(count);
                break;
        }
    }

    #endregion

    #region 卡牌的操作
    /// <summary>
    /// 移除一张卡牌
    /// </summary>
    /// <param name="card"></param>
    public void RemoveCard(CardContent card)
    {
        if (Deck.Contains(card))
        {
            Deck.Remove(card);
        }
    }

    /// <summary>
    /// 加入一张卡牌
    /// </summary>
    /// <param name="card"></param>
    public void JoinCard(CardContent card)
    {
        Deck.Add(card);
    }

    /// <summary>
    /// 替换一张卡牌
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="now"></param>
    public void ChangeCard(CardContent origin, CardContent now)
    {
        Deck.Remove(origin);
        Deck.Add(now);
    }
    #endregion

    protected override void UnityAwake()
    {
    }
}

