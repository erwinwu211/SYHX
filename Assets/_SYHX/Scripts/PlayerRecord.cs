using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecord : SingletonMonoBehaviour<PlayerRecord>
{
    public string playerName { get; private set; }
    public int playerLv { get; private set; }
    public int currentExp { get; private set; }
    public int luntCount { get; private set; }
    public int coreCount { get; private set; }
    public CharacterInfo Umirika { get; private set; }


    protected override void UnityAwake()
    {

        //搜索并获得存档
        if (FindRecord()!=null)
        {
            //将存档进行格式化
            //从格式化后的存档获取数据并赋值
        }
        else
        {
            //初始化数据
            playerLv = 1;
            currentExp = 0;
            luntCount = 0;
            coreCount = 0;
            Umirika = new CharacterInfo()
            {
                character = new Umirika(),
                isLock = false,
                characterLv = 1,
            };
            

            //创建新存档
            OverwriteRecord();
        }
    }

    /// <summary>
    /// 保存时覆盖存档数据
    /// </summary>
    public void OverwriteRecord()
    {

    }

    /// <summary>
    /// 搜索存档,并将之格式化
    /// </summary>
    /// <returns></returns>
    public string FindRecord()
    {
        return null;
    }
}

public struct CharacterInfo
{
    public CharacterContent character;
    public bool isLock;
    public int characterLv;
}

public struct DungeonInfo
{

}
