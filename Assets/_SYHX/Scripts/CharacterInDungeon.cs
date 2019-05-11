using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterInDungeon : MonoBehaviour
{
    protected int hp_max;
    protected int hp_currect;
    protected int attack;
    protected int energy_max;
    protected int draw_count;
    protected List<CardSource> job_deck;
    protected List<CardSource> equip_deck;

    public int GetHpMax() => hp_max;
    public int GetHpCurrent() => hp_currect;
    public int GetAttack() => attack;
    public int GetEnergyMax() => energy_max;
    public int GetDrawCount() => draw_count;
    public List<CardSource> GetDeck() => job_deck.Concat(equip_deck).ToList();

    public void IncreaseHpMax(int count)
    {
        if (count < 0) return;
        hp_max += count;
    }
    public void DecreaseHpMax(int count)
    {
        if (count < 0) return;
        hp_max -= count;
        if (hp_currect < hp_max)
        {
            hp_currect = hp_max;
        }
    }
    public void IncreaseHpCurrect(int count)
    {
        if (count < 0) return;
        hp_currect += count;
        if (hp_currect > hp_max) hp_currect = hp_max;
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
}
