using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnState_FSM : AssitantMonobehaviour<TurnManager>
{
    public enum State
    {
        First, PlayerStart, PlayerTurn, PlayerEnd, EnemyStart, EnemyTurn, EnemyEnd, Result
    }
    public PlayMakerFSM fsm;
    private State state;

    public void OnEnterState(State state)
    {
        this.state = state;
        owner.stateManager.OnEnterFsmState(GetCurrentState());
    }
    public TurnState GetCurrentState()
    {
        switch (state)
        {
            case State.First:
                return owner.stateManager.firstState;
            case State.PlayerStart:
                return owner.stateManager.playerStartState;
            case State.PlayerTurn:
                return owner.stateManager.playerTurnState;
            case State.PlayerEnd:
                return owner.stateManager.playerEndState;
            case State.EnemyTurn:
                return owner.stateManager.enemyTurnState;
            case State.EnemyStart:
                return owner.stateManager.enemyStartState;
            case State.EnemyEnd:
                return owner.stateManager.enemyEndState;
            default:
                return null;
        }
    }
    public void TryTransition(TurnState state)
    {
        fsm.SendEvent(state.FsmName());
    }
}
