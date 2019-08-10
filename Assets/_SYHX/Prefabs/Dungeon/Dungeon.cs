using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using System;

public class Dungeon : MonoBehaviour
{
    public string DungeonIndex;
    public string DungeonName;
    public string DungeonName_EN;
    public Chapter Chapter;
    public Vector3 Pos;
    public List<GameObject> DungeonRooms;
    [TableList]public List<ScoreReward> RewardList;
    public GameObject StartRoom;
    public GameObject EndRoom;
    public DungeonProceed DungeonProceed;
    public int mapWidth;
    public int mapHeight;
}

[Serializable]
public class ScoreReward
{
    public int score;
    public ItemSource item;
    public int itemCount;
}

public enum DungeonProceed
{
    Default,
    New,
    Going,
    Finished,
    Lock,
}
