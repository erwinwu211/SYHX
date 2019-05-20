using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon01 : DungeonSource
{
    public Dungeon01()
    {
        Name = "first";
        Desc = "all right";
        Pos = new Vector3(-320, 0);
    }

    public override DungeonContent GenerateDungeon()
    {
        return base.GenerateDungeon();
    }
}
