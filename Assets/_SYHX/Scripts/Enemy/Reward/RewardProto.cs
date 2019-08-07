using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class RewardProto : MonoBehaviour
{
    public List<ItemSourceAndCount> dungeonResourceReward;
    public DungeonCardRewardSource dungeonCardRewardGroup;
}

/// <summary>
/// 包含了item和count两个属性
/// </summary>
[Serializable]
public class ItemSourceAndCount
{
    public ItemSource item;
    public int count;
}
