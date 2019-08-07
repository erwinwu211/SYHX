using SYHX.Cards;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class Umirika : CharacterContent
{
    public override string Name { get => Initializer.Ins.umirika.Name; }

    //[TableList] public DungeonLvInfo[] lvInfos;
    
    public Umirika()
    {
        Hp_max = 70;
        Attack = 6;
        Defend = 5;
        Draw_count = 5;
        Energy_max = 3;
        STR = 3;
        AGI = 3;
        INT = 3;
        FOR = 3;
        Talents = new List<Talent>();
        Talents.Add(new Talent_00001());
        Talents[0].IsEffect = true;
        Words = new CharacterWords()
        {
            Welcome = "欢迎回来",
            Touch = new string[]
            {
                "摸摸头1",
                "摸摸头2",
                "摸摸头3",
            },
            Home = "终于能休息啦",
        };
    }

    /// <summary>
    /// TODO:这个方法里要读取存档然后返回一个content
    /// </summary>
    /// <returns></returns>

}
