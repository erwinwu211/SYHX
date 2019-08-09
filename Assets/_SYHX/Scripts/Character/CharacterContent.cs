using System.Collections.Generic;
using UnityEngine;
using SYHX.Cards;
using Sirenix.OdinInspector;
using System;

/// <summary>
/// 所有角色的父类
/// </summary>
public class CharacterContent : MonoBehaviour
{
    public bool isLock;
    public virtual string Name { get; }
    public Grade currentGrade;//角色当前的阶级
    public List<Grade> gradeList;
    public int Exp;
    public int Hp_max;
    public int Attack;
    public int Defend;
    public int Draw_count;
    public int Energy_max;
    public int Force;
    public int Agile;
    public int Constitution;
    public int Fortune;
    public int InitDataChip;
    public int InitDungeonExp;
    [SerializeField] public List<CardSource> Deck;
    [SerializeField] public List<TalentGroup> TalentGroups;
    public CharacterWords Words;
    [TableList] public CharacterSkill[] skills;
    [TableList] public DungeonLvInfo[] lvInfos;

    public CharacterContent() { }

    /// <summary>
    /// 升阶
    /// </summary>
    public void Upgrade()
    {
        if (CheckUpgradable() == false) return;
        if (currentGrade.nextGrade)
        {
            foreach (ItemSourceAndCount require in currentGrade.UpgradeRequireList)
            {
                PlayerRecord.Ins.itemDict[require.item.id] -= require.count;
            }
            currentGrade = currentGrade.nextGrade;
        }
    }

    /// <summary>
    /// 检测是否满足升阶条件
    /// </summary>
    /// <returns></returns>
    public bool CheckUpgradable()
    {
        foreach (ItemSourceAndCount require in currentGrade.UpgradeRequireList)
        {
            if (PlayerRecord.Ins.itemDict.ContainsKey(require.item.id) == false) return false;
            int count = PlayerRecord.Ins.itemDict[require.item.id];
            if (count < require.count) return false;
        }
        return true;
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

