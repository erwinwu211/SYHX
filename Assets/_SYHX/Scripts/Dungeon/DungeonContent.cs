using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonContent : SAssitant<DungeonSource>
{
    public string Name { get; protected set; }
    public string Desc { get; protected set; }
    public Vector3 Pos { get; protected set; }
    public string Icon { get; protected set; }

    public DungeonContent() { }
    public override void SetOwner(DungeonSource owner)
    {
        this.owner = owner;
        this.Name = owner.Name;
        this.Desc = owner.Desc;
        this.Pos = owner.Pos;
        this.Icon = owner.Icon;
    }
}
