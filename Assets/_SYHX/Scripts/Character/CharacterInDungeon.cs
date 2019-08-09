using System.Collections.Generic;
using UnityEngine;
using SYHX.Cards;

public enum BasicAttribute
{
    Force, Aglie, Constitution, Fortune
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
    public int Force { get; private set; }
    public int Agile { get; private set; }
    public int Constitution { get; private set; }
    public int Fortune { get; private set; }
    public int AttrMax = 10;
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
        this.Force = cc.STR;
        this.Agile = cc.AGI;
        this.Constitution = cc.INT;
        this.Fortune = cc.FOR;
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
    /// 增加基础属性
    /// </summary>
    /// <param name="attr"></param>
    /// <param name="count"></param>
    public void IncreaseBasicAttribute(BasicAttribute attr, int count)
    {
        switch (attr)
        {
            case BasicAttribute.Force:
                Force += count;
                if (Force > AttrMax)
                {
                    Force = AttrMax;
                }
                break;
            case BasicAttribute.Aglie:
                Agile += count;
                if (Agile > AttrMax)
                {
                    Agile = AttrMax;
                }
                break;
            case BasicAttribute.Constitution:
                Constitution += count;
                if (Constitution > AttrMax)
                {
                    Constitution = AttrMax;
                }
                break;
            case BasicAttribute.Fortune:
                Fortune += count;
                if (Fortune > AttrMax)
                {
                    Fortune = AttrMax;
                }
                break;
        }
    }

    /// <summary>
    /// 减少基础属性
    /// </summary>
    /// <param name="attr"></param>
    /// <param name="count"></param>
    public void DecreaseBasicAttribute(BasicAttribute attr, int count)
    {
        switch (attr)
        {
            case BasicAttribute.Force:
                Force -= count;
                if (Force < 1)
                {
                    Force = 1;
                }
                break;
            case BasicAttribute.Aglie:
                Agile -= count;
                if (Agile < 1)
                {
                    Agile = 1;
                }
                break;
            case BasicAttribute.Constitution:
                Constitution -= count;
                if (Constitution < 1)
                {
                    Constitution = 1;
                }
                break;
            case BasicAttribute.Fortune:
                Fortune -= count;
                if (Fortune < 1)
                {
                    Fortune = 1;
                }
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

