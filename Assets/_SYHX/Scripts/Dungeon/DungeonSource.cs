using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSource
{
    public string Name { get; protected set; }
    public string Desc { get; protected set; }
    public Vector3 Pos { get; protected set; }
    public string Icon { get; protected set; }
    public bool IsLocked { get; protected set; }
    public bool IsCompleted { get; protected set; }
    //public List<RoomSource> rooms { get; protected set; }

}


public class Dungeon01 : DungeonSource
{
    public Dungeon01()
    {
        Name = "first";
        Desc = "all right";
        Pos = new Vector3(-320, -70);
    }
}


public class Dungeon02 : DungeonSource
{
    public Dungeon02()
    {
        Name = "second";
        Desc = "all right";
        Pos = new Vector3(-78, 252);
    }
}


public class Dungeon03 : DungeonSource
{
    public Dungeon03()
    {
        Name = "third";
        Desc = "all right";
        Pos = new Vector3(407, 67);
    }
}


public class Dungeon04 : DungeonSource
{
    public Dungeon04()
    {
        Name = "forth";
        Desc = "all right";
        Pos = new Vector3(162, -259);
    }
}