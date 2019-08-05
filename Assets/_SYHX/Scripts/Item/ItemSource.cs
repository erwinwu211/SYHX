using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemSource : ScriptableObject
{
    // Start is called before the first frame update
    public int id;
    public string name;
    public string desc;
    public int count;
    public ItemType type;
    public Sprite icon;
}


public enum ItemType
{
    Resource,
    DungeonResource,
    DungeonItem,
    DungeonCardGroup,
    Equip,
}