using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : SingletonMonoBehaviour<DungeonManager>
{

    public Dungeon mDungeon { get; private set; }
    public CharacterContent mCharacter { get; private set; }
    public CharacterInDungeon dungeonCharacter { get; private set; }
    public DungeonUI DungeonUI;
    public GenerateMap Generator;


    public void LoadData(Dungeon dungeon, CharacterContent cc)
    {
        mDungeon = dungeon;
        mCharacter = cc;
    }

    private  CharacterInDungeon InitDungeonCharacter(CharacterContent character)
    {
        return dungeonCharacter;
    }

    private void Start()
    {
        DungeonStatus ds = SceneStatusManager.Ins.current as DungeonStatus;
        LoadData(ds.Dungeon, ds.cc);
        Generator.LoadDungeonData(mDungeon, mCharacter);
        Generator.makeDictionary();
        Generator.loadMap();
        InitDungeonCharacter(mCharacter);
        DungeonUI.RefreshUI(this);
    }

    protected override void UnityAwake()
    {
    }
}
