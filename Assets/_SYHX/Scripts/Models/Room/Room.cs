using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public RoomType RoomType;
}

public enum RoomType
{
    Nothing,//无事件
    Battle_Normal,//普通怪房间
    Battle_Elite,//精英怪房间
    Battle_Boss,//BOSS房间
    Event,//事件房间
    Workshop,//工坊房间-升级卡牌
    Treasure,//宝物房间
    Gathering,//采集房间
    NextFloor,//下一层入口房间
    Exit,//出口房间
}