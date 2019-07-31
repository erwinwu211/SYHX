using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class SceneStatus : Assitant<SceneStatusManager>
{
    public SceneStatus(SceneStatusManager owner) : base(owner) { }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
    public abstract string SceneName();
    public bool IsCurrent()
    {
        return base.owner.current == this;
    }
}


public class MainStatus : SceneStatus
{
    public MainStatus(SceneStatusManager owner) : base(owner) { }

    public override string SceneName() => "Main";
    public CharacterContent cc;

    public override void Enter()
    {

        cc = PlayerRecord.Ins.Umirika;
        MainUI.Ins.RefreshCoreCount(PlayerRecord.Ins.coreCount);
        MainUI.Ins.RefreshLuntCount(PlayerRecord.Ins.luntCount);
        MainUI.Ins.RefreshLvInfo(PlayerRecord.Ins.playerLv,PlayerRecord.Ins.currentExp);
        MainUI.Ins.RefreshTime();
        MainUI.Ins.ShowDialogueBox(cc.Words.Welcome,5);
    }

    public override void Update()
    {
        MainUI.Ins.RefreshTime();
    }
}

public class ChooseStatus : SceneStatus
{
    public ChooseStatus(SceneStatusManager owner) : base(owner) { }

    public override string SceneName() => "Dungeon Choose";

    public Dungeon Dungeon { get; set; }
    public CharacterContent cc = PlayerRecord.Ins.Umirika;

    public override void Enter()
    {
        MapUI.Ins.LoadChaptersInfo(Initializer.Ins.chapters);
    }

    public void ChangeSelectedDungeon(Dungeon dungeon)
    {
        Dungeon = dungeon;
    }

    public void GoToDungeonStatus()
    {
        ES3.DeleteKey("dungeonObject");
        DungeonStatus ds = new DungeonStatus(SceneStatusManager.Ins);

        ds.Dungeon = this.Dungeon;
        ds.cc = this.cc;
        SceneStatusManager.Ins.SetSceneStatus(ds);


    }
}

public class DungeonStatus : SceneStatus
{
    public DungeonStatus(SceneStatusManager owner) : base(owner) { }

    public Dungeon Dungeon { get; set; }
    public CharacterContent cc { get; set; }

    public override string SceneName() => "Dungeon";

    public override void Enter()
    {
    }
}

public class BattleStatus : SceneStatus
{
    public BattleStatus(SceneStatusManager owner) : base(owner) { }
    public override string SceneName() => "Battle Scene";
    public override void Enter()
    {
        // SceneStatusManager.Ins.SetSceneStatus(SceneStatusManager.Ins.Record);
    }

    public override void Exit()
    {
        SceneManager.UnloadSceneAsync(SceneName());
    }
}

public class CharacterStatus : SceneStatus
{
    public CharacterStatus(SceneStatusManager owner) : base(owner) { }

    public override string SceneName() => "Character";
    public CharacterContent cc;
    public override void Enter()
    {
        cc = PlayerRecord.Ins.Umirika;
        CharacterUI.Ins.RefreshCharacterInfo(cc);
    }
}
