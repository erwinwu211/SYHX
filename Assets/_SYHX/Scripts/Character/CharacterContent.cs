using System.Collections.Generic;
using SYHX.Cards;

/// <summary>
/// 所有角色的父类
/// </summary>
public abstract class CharacterContent
{
    public const int LvMax = 30;
    public string Name { get; protected set; }
    public int Lv { get; protected set; }
    public int Exp { get; protected set; }
    public int Exp_max { get; protected set; }
    public int Hp_max { get; protected set; }
    public int Attack { get; protected set; }
    public int Defend { get; protected set; }
    public int Draw_count { get; protected set; }
    public int Energy_max { get; protected set; }
    public int Force { get; protected set; }
    public int Aglie { get; protected set; }
    public int Constitution { get; protected set; }
    public int Fortune { get; protected set; }
    public List<CardContent> Deck { get; protected set; }
    public List<Talent> Talents { get; protected set; }

    public CharacterContent() { }
    
    public void LevelUp()
    {
        if (Lv < LvMax)
        {
            Lv++;
            Exp -= Exp_max;
            RefreshCharacterInfo();
        }
    }

    public void IncreaseExp(int count)
    {
        Exp += count;
        if (Exp >= Exp_max)
        {
            LevelUp();
        }
    }

    public void RefreshCharacterInfo()
    {

    }

}
