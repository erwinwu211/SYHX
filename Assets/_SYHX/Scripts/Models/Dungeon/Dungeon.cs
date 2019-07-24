using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public string DungeonIndex;
    public string DungeonName;
    public string DungeonName_EN;
    public Chapter Chapter;
    public Vector3 Pos;
    public List<GameObject> DungeonRooms;
    public GameObject StartRoom;
    public GameObject EndRoom;
    public DungeonProceed DungeonProceed;
}

public enum DungeonProceed
{
    Default,
    New,
    Going,
    Finished,
    Lock,
}
