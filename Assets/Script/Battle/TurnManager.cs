public class TurnManager : SingletonMonoBehaviour<TurnManager>
{

    public TurnState_FSM fsmManager;
    public TurnStateManager stateManager;
    protected override void UnityAwake()
    {
        stateManager = new TurnStateManager(this);
    }

    private void Update()
    {
        stateManager.current.Update();
    }
}
