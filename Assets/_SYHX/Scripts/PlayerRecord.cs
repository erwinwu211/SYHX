using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerRecord : SingletonMonoBehaviour<PlayerRecord>
{
    public string playerName { get; private set; }
    public int playerLv { get; private set; }
    public int currentExp { get; private set; }
    public int luntCount { get => itemDict[101]; }
    public int coreCount { get => itemDict[102]; }
    public GameObject CharacterManager;
    public CharacterContent Umirika { get; private set; }
    public Dungeon guildDungeon;
    public Dictionary<int, int> itemDict;//第一个int是id，第二个int是数量
    public List<ItemSource> itemList;


    protected override void UnityAwake()
    {
        Umirika = CharacterManager.GetComponent<Umirika>();
        //搜索并获得存档
        if (FindRecord() != null)
        {
            //将存档进行格式化
            //从格式化后的存档获取数据并赋值
        }
        else
        {
            //初始化数据
            InitNewRecord();

            //创建新存档
            OverwriteRecord();
        }
    }

    /// <summary>
    /// 建立新存档
    /// </summary>
    public void InitNewRecord()
    {
        playerLv = 1;
        currentExp = 0;
        Umirika.currentGrade = Umirika.gradeList[0];
        itemDict = new Dictionary<int, int>();
        foreach (ItemSource item in itemList)
        {
            itemDict.Add(item.id, 999);
        }
    }

    /// <summary>
    /// 保存时覆盖存档数据
    /// </summary>
    public void OverwriteRecord()
    {
        // DungeonStatus ds = new DungeonStatus(SceneStatusManager.Ins);

        // ds.Dungeon = this.guildDungeon;
        // ds.cc = this.Umirika;
        // SceneStatusManager.Ins.SetSceneStatus(ds);
    }

    /// <summary>
    /// 搜索存档,并将之格式化
    /// </summary>
    /// <returns></returns>
    public string FindRecord()
    {
        return null;
    }

    public int GetItemCountByID(int itemID)
    {
        return itemDict[itemID];
    }
}


public struct DungeonInfo
{

}
