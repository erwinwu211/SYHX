using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneState : Assitant<SceneStateManager>
{
    public SceneState(SceneStateManager owner) : base(owner) { }

    public virtual void Enter() { }
    public virtual void Update() { owner.StateUpdate(); }
    public virtual void Exit() { }
    public abstract string SceneName();
    public bool IsCurrent()
    {
        return base.owner.current == this;
    }
}


public class MainState : SceneState
{
    public MainState(SceneStateManager owner) : base(owner) { }

    public override string SceneName() => "Main";
}

public class ChooseState : SceneState
{
    public ChooseState(SceneStateManager owner) : base(owner) { }

    public override string SceneName() => "Dungeon Choose";
}

public class DungeonState : SceneState
{
    public DungeonState(SceneStateManager owner) : base(owner) { }

    public override string SceneName() => "Dungeon";
}

public class BattleState : SceneState
{
    public BattleState(SceneStateManager owner) : base(owner) { }

    public override string SceneName() => "Battle Scene";
}
