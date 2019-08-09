using System;

[Serializable]
public class LvInfo
{
    public string LvName;
    public int RequireCount;
}

[Serializable]
public class DungeonLvInfo:LvInfo
{
    public int HpReward;
    public int AttackReward;
    public int DefendReward;
    public int EPReward;
    public int DrawCount;
}

