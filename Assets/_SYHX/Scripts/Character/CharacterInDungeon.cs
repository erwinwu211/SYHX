using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterInDungeon : SingletonMonoBehaviour<CharacterInDungeon>
{
    public CharacterContent character;
    public int hp_max_from_dungeon;
    public int attack_from_dungeon;
    public int defend_from_dungeon;
    public int hp_currect { get; private set; }
    public int Energy_max { get; private set; }
    public int Draw_count { get; private set; }
    public List<CardContent> deck { get; private set; }
    public List<CardContent> character_deck { get; private set; }
    public List<CardContent> equip_deck { get; private set; }
    public List<EquipmentContent> Equipments { get; private set; }

    public void Start()
    {
        //这里先暂时用一下预设好的人物
        FukasakiKotone c = new FukasakiKotone();
        CharacterContent cc = c.GenerateCharacter();
        Init(cc);
    }

    public void Init(CharacterContent cc)
    {
        character = cc;
        Energy_max = character.Energy;
        Draw_count = character.DrawCount;
        hp_max_from_dungeon = 0;
        attack_from_dungeon = 0;
        defend_from_dungeon = 0;
        deck = new List<CardContent>();
        character_deck = new List<CardContent>();
        equip_deck = new List<CardContent>();
        Equipments = new List<EquipmentContent>();
        InitDeck();
        Debug.Log("当前生命值：" + GetHpMax());
    }

    /// <summary>
    /// 获取生命最大值
    /// </summary>
    /// <returns></returns>
    public int GetHpMax()
    {
        int hp_max = character.HpMax + hp_max_from_dungeon;
        foreach (EquipmentContent ec in Equipments)
        {
            hp_max += ec.Hp;
        }
        return hp_max;
    }

    /// <summary>
    /// 获取攻击力
    /// </summary>
    /// <returns></returns>
    public int GetAttack()
    {
        int attack = character.Attack + attack_from_dungeon;
        foreach (EquipmentContent ec in Equipments)
        {
            attack += ec.Attack;
        }
        return attack;
    }

    /// <summary>
    /// 获取防御力
    /// </summary>
    /// <returns></returns>
    public int GetDefend()
    {
        int defend = character.Defend + defend_from_dungeon;
        foreach (EquipmentContent ec in Equipments)
        {
            defend += ec.Defend;
        }
        return defend;
    }


    /// <summary>
    /// 根据玩家的装备与天赋，刷新玩家的牌库
    /// </summary>
    /// <returns></returns>
    public void InitDeck()
    {
        deck.Clear();
        character_deck.Clear();
        equip_deck.Clear();
        foreach (EquipmentContent ec in Equipments)
        {
            foreach (CardContent cc in ec.Cards())
            {
                equip_deck.Add(cc);
                deck.Add(cc);
            }
        }
        foreach (CardContent cc in character.Cards)
        {
            character_deck.Add(cc);
            deck.Add(cc);
        }
    }

    /// <summary>
    /// 返回玩家的牌库
    /// </summary>
    /// <returns></returns>
    public List<CardContent> GetDeck()
    {
        return deck;
    }

    /// <summary>
    /// 增加生命最大值
    /// </summary>
    /// <param name="count"></param>
    public void IncreaseHpMax(int count)
    {
        if (count < 0) return;
        hp_max_from_dungeon += count;
    }

    /// <summary>
    /// 减少生命最大值
    /// </summary>
    /// <param name="count"></param>
    public void DecreaseHpMax(int count)
    {
        if (count < 0) return;
        hp_max_from_dungeon -= count;
        if (hp_currect < GetHpMax())
        {
            hp_currect = GetHpMax();
        }
        if (GetHpMax() <= 0)
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
        hp_currect += count;
        if (hp_currect > GetHpMax()) hp_currect = GetHpMax();
    }

    /// <summary>
    /// 削减当前生命值
    /// </summary>
    /// <param name="count"></param>
    public void DecreaseHpCurrect(int count)
    {
        if (count < 0) return;
        hp_currect -= count;
        if (hp_currect <= 0)
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
        attack_from_dungeon += count;
    }

    /// <summary>
    /// 减少攻击力
    /// </summary>
    /// <param name="count"></param>
    public void DecreaseAttack(int count)
    {
        if (count < 0) return;
        attack_from_dungeon -= count;
    }

    /// <summary>
    /// 增加防御力
    /// </summary>
    /// <param name="count"></param>
    public void IncreaseDefend(int count)
    {
        if (count < 0) return;
        defend_from_dungeon += count;
    }

    /// <summary>
    /// 减少防御力
    /// </summary>
    /// <param name="count"></param>
    public void DecreaseDefend(int count)
    {
        if (count < 0) return;
        defend_from_dungeon -= count;
    }

    /// <summary>
    /// 移除一张职业卡牌
    /// </summary>
    /// <param name="card"></param>
    public void RemoveJobCard(CardContent card)
    {
        if (character_deck.Contains(card))
        {
            character_deck.Remove(card);
            deck.Remove(card);
        }
    }

    /// <summary>
    /// 加入一张职业卡牌
    /// </summary>
    /// <param name="card"></param>
    public void JoinCard(CardContent card)
    {
        character_deck.Add(card);
        deck.Add(card);
    }

    /// <summary>
    /// 装备一个装备
    /// </summary>
    /// <param name="ec"></param>
    public void Equip(EquipmentContent ec)
    {
        Equipments.Add(ec);
        foreach (CardContent cc in ec.Cards())
        {
            equip_deck.Add(cc);
            deck.Add(cc);
        }
    }

    /// <summary>
    /// 卸除一个装备
    /// </summary>
    /// <param name="ec"></param>
    public void Unequip(EquipmentContent ec)
    {
        if (Equipments.Contains(ec))
        {
            Equipments.Remove(ec);
        }
        else
        {
            return;
        }
        foreach (CardContent cc in ec.Cards())
        {
            equip_deck.Remove(cc);
            deck.Remove(cc);
        }
    }

    protected override void UnityAwake()
    {
    }
}
