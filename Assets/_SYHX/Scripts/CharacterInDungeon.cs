using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterInDungeon : MonoBehaviour
{
    protected int hp_max_from_job;
    protected int hp_max_from_dungeon;
    protected int hp_currect;
    protected int attack_from_job;
    protected int attack_from_dungeon;
    protected int energy_max;
    protected int draw_count;
    protected List<CardSource> job_deck;
    protected List<CardSource> equip_deck;
    protected List<EquipmentSource> equipments;

    public int GetHpMax()
    {
        int hp_max = hp_max_from_job + hp_max_from_dungeon;
        foreach (EquipmentSource equipment in equipments)
        {
            hp_max += equipment.Hp;
        }
        return hp_max;
    }
    public int GetHpCurrent() => hp_currect;
    public int GetAttack()
    {
        int attack = attack_from_job + attack_from_dungeon;
        foreach (EquipmentSource equipment in equipments)
        {
            attack += equipment.Attack;
        }
        return attack;
    }
    public int GetEnergyMax() => energy_max;
    public int GetDrawCount() => draw_count;
    public List<CardSource> GetDeck()
    {
        foreach (EquipmentSource equipment in equipments)
        {
            foreach (CardSource card in equipment.Cards)
            {
                equip_deck.Add(card);
            }
        }
        return job_deck.Concat(equip_deck).ToList();
    }

    public void IncreaseHpMax(int count)
    {
        if (count < 0) return;
        hp_max_from_dungeon += count;
    }
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
    public void IncreaseHpCurrect(int count)
    {
        if (count < 0) return;
        hp_currect += count;
        if (hp_currect > GetHpMax()) hp_currect = GetHpMax();
    }
    public void DecreaseHpCurrect(int count)
    {
        if (count < 0) return;
        hp_currect -= count;
        if (hp_currect <= 0)
        {
            //TODO:SendEvent("DungeonEnd");
        }
    }
    public void RemoveCard(CardSource card)
    {
        if (job_deck.Contains(card))
        {
            job_deck.Remove(card);
        }
    }
    public void JoinCard(CardSource card)
    {
        job_deck.Add(card);
    }
    public void Equip(EquipmentSource equipment)
    {
        equipments.Add(equipment);
    }
    public void Unequip(EquipmentSource equipment)
    {
        if (equipments.Contains(equipment))
        {
            equipments.Remove(equipment);
        }
    }
}
