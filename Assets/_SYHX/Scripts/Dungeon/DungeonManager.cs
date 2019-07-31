using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : SingletonMonoBehaviour<DungeonManager>
{

    public Dungeon mDungeon { get; private set; }
    public CharacterContent mCharacter { get; private set; }
    public CharacterInDungeon dungeonCharacter => CharacterInDungeon.Ins;
    public DungeonUI DungeonUI;
    public GenerateMap Generator;
    private int Floor = 1;


    public void LoadData(Dungeon dungeon, CharacterContent cc)
    {
        mDungeon = dungeon;
        mCharacter = cc;
        dungeonCharacter.Init(cc);
    }

    private CharacterInDungeon InitDungeonCharacter(CharacterContent character)
    {
        return dungeonCharacter;
    }

    private void Start()
    {
        //从场景中获取人物与地图信息
        DungeonStatus ds = SceneStatusManager.Ins.current as DungeonStatus;
        LoadData(ds.Dungeon, ds.cc);
        //读取地图信息
        Generator.LoadDungeonData(mDungeon, mCharacter);
        Generator.makeDictionary();
        Generator.loadMap();
        //读取人物信息
        InitDungeonCharacter(mCharacter);
        //刷新UI界面
        DungeonUI.RefreshUI();
    }



    protected override void UnityAwake()
    {
    }


    /// <summary>
    /// 进入下一层地图
    /// </summary>
    public void GotoNextFloor()
    {
        Generator.clearMap();
        Generator.loadMap();
        Floor++;
        DungeonUI.RefreshUI();
    }


    //TODO
    public void DealWithBattleResult(PassedResultInformation information)
    {

    }
}
