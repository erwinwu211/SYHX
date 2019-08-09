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
        Force = 3;
        Agile = 3;
        Constitution = 3;
        Fortune = 3;
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
