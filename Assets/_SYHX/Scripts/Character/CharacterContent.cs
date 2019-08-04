using System.Collections.Generic;
using UnityEngine;
using SYHX.Cards;
using Sirenix.OdinInspector;

/// <summary>
/// 所有角色的父类
/// </summary>
public class CharacterContent : MonoBehaviour
{
    public const int LvMax = 30;
    public bool isLock;
    public virtual string Name { get; }
    public int Lv;
    public int Exp;
    public int Hp_max;
    public int Attack;
    public int Defend;
    public int Draw_count;
    public int Energy_max;
    public int STR;
    public int AGI;
    public int INT;
    public int FOR;
    public int InitDataChip;
    [SerializeField] public List<CardSource> Deck;
    [SerializeField] public List<Talent> Talents;
    public CharacterWords Words;
    [TableList] public LvInfo[] lvInfos;
    [TableList] public CharacterSkill[] skills;

    public CharacterContent() { }

    public void LevelUp()
    {
        if (Lv < LvMax)
        {
            Exp -= lvInfos[Lv - 1].Exp;
            Lv++;
            RefreshCharacterInfo();
        }
    }

    public void IncreaseExp(int count)
    {
        Exp += count;
        if (Exp >= lvInfos[Lv - 1].Exp)
        {
            LevelUp();
        }
    }

    public void RefreshCharacterInfo()
    {

    }

}

public struct CharacterWords
{
    public string Welcome;
    public string[] Touch;
    public string Home;
}