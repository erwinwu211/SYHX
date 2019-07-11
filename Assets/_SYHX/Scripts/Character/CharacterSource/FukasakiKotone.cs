using SYHX.Cards;
using System.Collections.Generic;


public class Umirika : CharacterContent
{
    public Umirika()
    {
        Name = "尤米莉卡";
        Hp_max = 70;
        Attack = 6;
        Defend = 5;
        Draw_count = 5;
        Energy_max = 3;
        Deck = new List<CardContent>();
        Talents = new List<Talent>();
        Talents.Add(new Talent_00001());
        Talents[0].IsEffect = true;
    }

    /// <summary>
    /// TODO:这个方法里要读取存档然后返回一个content
    /// </summary>
    /// <returns></returns>
    
}
