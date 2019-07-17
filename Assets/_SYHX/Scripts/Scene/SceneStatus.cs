using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneStatus : Assitant<SceneStatusManager>
{
    public SceneStatus(SceneStatusManager owner) : base(owner) { }

    public virtual void Enter() { }
    public virtual void Update() { owner.StatusUpdate(); }
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
