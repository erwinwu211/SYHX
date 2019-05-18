using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterInDungeon : MonoBehaviour
{
    public int hp_max_from_job;
    public int hp_max_from_dungeon;
    public int attack_from_job;
    public int attack_from_dungeon;
    public int defend_from_job;
    public int defend_from_dungeon;
    public int hp_currect { get; private set; }
    public int Energy_max { get; private set; }
    public int Draw_count { get; private set; }
    public List<CardContent> Deck { get; private set; }
    public List<CardContent> Job_Deck { get; private set; }
    public List<CardContent> Equip_Deck { get; private set; }
    public List<EquipmentContent> Equipments { get; private set; }

    /// <summary>
    /// 获取生命最大值
    /// </summary>
    /// <returns></returns>
    public int GetHpMax()
    {
        int hp_max = hp_max_from_job + hp_max_from_dungeon;
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
        int attack = attack_from_job + attack_from_dungeon;
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
        int defend = defend_from_job + defend_from_dungeon;
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
    public void RefreshDeck()
    {
        Deck.Clear();
        Job_Deck.Clear();
        Equip_Deck.Clear();
        foreach (EquipmentContent ec in Equipments)
        {
            foreach (CardContent card in ec.Cards())
            {
                Equip_Deck.Add(card);
                Deck.Add(card);
            }
        }
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
    /// 移除一张职业卡牌
    /// </summary>
    /// <param name="card"></param>
    public void RemoveJobCard(CardContent card)
    {
        if (Job_Deck.Contains(card))
        {
            Job_Deck.Remove(card);
            Deck.Remove(card);
        }
    }

    /// <summary>
    /// 加入一张职业卡牌
    /// </summary>
    /// <param name="card"></param>
    public void JoinCard(CardContent card)
    {
        Job_Deck.Add(card);
        Deck.Add(card);
    }

    /// <summary>
    /// 装备一个装备
    /// </summary>
    /// <param name="ec"></param>
    public void Equip(EquipmentContent ec)
    {
        Equipments.Add(ec);
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
    }
}
