using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnStateManager : Assitant<TurnManager>
{
    public FirstState firstState;
    public PlayerStartState playerStartState;
    public PlayerTurnState playerTurnState;
    public PlayerEndState playerEndState;
    public EnemyStartState enemyStartState;
    public EnemyTurnState enemyTurnState;
    public EnemyEndState enemyEndState;
    public ResultState resultState;

    public TurnState current;
    public TurnStateManager(TurnManager owner) : base(owner)
    {
        firstState = new FirstState(this);
        playerStartState = new PlayerStartState(this);
        playerTurnState = new PlayerTurnState(this);
        playerEndState = new PlayerEndState(this);
        enemyStartState = new EnemyStartState(this);
        enemyTurnState = new EnemyTurnState(this);
        enemyEndState = new EnemyEndState(this);
        resultState = new ResultState(this);
        current = firstState;
    }
    public void OnEnterFsmState(TurnState state)
    {
        Enter(state);
    }
    private void Enter(TurnState next)
    {
        current.Exit();
        current = next;
        next.Enter();
    }
}
