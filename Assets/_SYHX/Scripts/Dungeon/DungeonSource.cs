using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSource : ScriptableObject
{
    public string Name { get; protected set; }
    public string Desc { get; protected set; }
    public Vector3 Pos { get; protected set; }
    public string Icon { get; protected set; }
    //public List<RoomSource> rooms { get; protected set; }

    public virtual DungeonContent GenerateDungeon()
    {
        var dc = new DungeonContent();
        dc.SetOwner(this);
        return dc;
    }

}
