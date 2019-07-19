using System.Collections.Generic;
using UnityEngine;
using SYHX.Cards;

public enum BasicAttribute
{
    Force,Aglie,Constitution,Fortune
}

/// <summary>
/// 当玩家进入地宫时，根据所选择的人物及其设定所生成的对象
/// 用于记录玩家在地宫中的信息
/// </summary>
public class CharacterInDungeon : SingletonMonoBehaviour<CharacterInDungeon>
{
    public CharacterContent character;
    public int Hp_max { get; private set; }
    public int Hp_currect { get; private set; }
    public int Attack { get; private set; }
    public int Defend { get; private set; }
    public int Energy_max { get; private set; }
    public int Draw_count { get; private set; }
    public int Force { get; private set; }
    public int Aglie { get; private set; }
    public int Constitution { get; private set; }
    public int Fortune { get; private set; }
    public int AttrMax = 10;
    public List<CardContent> Deck { get; private set; }

    public void Start()
    {
        //这里先暂时用一下预设好的人物
        Umirika umirika = new Umirika();
        Init(umirika);
    }

    public void Init(CharacterContent cc)
    {
        character = cc;
        this.Hp_max = cc.Hp_max;
        this.Hp_currect = cc.Hp_max;
        this.Attack = cc.Attack;
        this.Defend = cc.Defend;
        this.Energy_max = cc.Energy_max;
        this.Draw_count = cc.Draw_count;
        this.Force = cc.STR;
        this.Aglie = cc.AGI;
        this.Constitution = cc.INT;
        this.Fortune = cc.FOR;
        this.Deck = cc.Deck;
    }


    /// <summary>
    /// 增加生命最大值
    /// </summary>
    /// <param name="count"></param>
    public void IncreaseHpMax(int count)
    {
        if (count < 0) return;
        Hp_max += count;
    }

    /// <summary>
    /// 减少生命最大值
    /// </summary>
    /// <param name="count"></param>
    public void DecreaseHpMax(int count)
    {
        if (count < 0) return;
        Hp_max -= count;
        if (Hp_max < Hp_currect)
        {
            Hp_currect = Hp_max;
        }
        if (Hp_max <= 0)
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
        Hp_currect += count;
        if (Hp_currect > Hp_max) Hp_currect = Hp_max;
    }

    /// <summary>
    /// 削减当前生命值
    /// </summary>
    /// <param name="count"></param>
    public void DecreaseHpCurrect(int count)
    {
        if (count < 0) return;
        Hp_currect -= count;
        if (Hp_currect <= 0)
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
    public void IncreaseBasicAttribute(BasicAttribute attr,int count)
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
                Aglie += count;
                if (Aglie > AttrMax)
                {
                    Aglie = AttrMax;
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
                Aglie -= count;
                if (Aglie < 1)
                {
                    Aglie = 1;
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


    /// <summary>
    /// 移除一张卡牌
    /// </summary>
    /// <param name="card"></param>
    public void RemoveJobCard(CardContent card)
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
    public void ChangeCard(CardContent origin,CardContent now)
    {
        Deck.Remove(origin);
        Deck.Add(now);
    }

    protected override void UnityAwake()
    {
    }
}
