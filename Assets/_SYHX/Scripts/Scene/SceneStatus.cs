using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private CharacterContent cc;

    public override void Enter()
    {
        cc = PlayerRecord.Ins.Umirika.character;
        MainUI.Ins.RefreshCoreCount(PlayerRecord.Ins.coreCount);
        MainUI.Ins.RefreshLuntCount(PlayerRecord.Ins.luntCount);
        MainUI.Ins.RefreshLvInfo(PlayerRecord.Ins.playerLv,PlayerRecord.Ins.currentExp);
        MainUI.Ins.RefreshTime();
        MainUI.Ins.ShowDialogueBox(cc.Welcome,5);
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
}

public class DungeonStatus : SceneStatus
{
    public DungeonStatus(SceneStatusManager owner) : base(owner) { }

    public override string SceneName() => "Dungeon";
}

public class BattleStatus : SceneStatus
{
    public BattleStatus(SceneStatusManager owner) : base(owner) { }

    public override string SceneName() => "Battle Scene";
}

public class CharacterStatus : SceneStatus
{
    public CharacterStatus(SceneStatusManager owner) : base(owner) { }

    public override string SceneName() => "Character";
}
