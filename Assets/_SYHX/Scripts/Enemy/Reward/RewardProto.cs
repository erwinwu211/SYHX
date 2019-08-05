using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class RewardProto : MonoBehaviour
{
    public List<DungeonResourceReward> dungeonResourceReward;
    public List<DungeonCardRewardSource> dungeonCardRewardGroup;
}

[Serializable]
public class DungeonResourceReward
{
    public DungeonResourceSource item;
    public int count;
}
