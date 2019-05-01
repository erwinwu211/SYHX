using System;

public class BattleProgressEvent : Singleton<BattleProgressEvent>
{
    protected override void OnGenerate() { }
    public event Action onPlayerTurnStart = delegate { };
    public void OnPlayerTurnStart()
    {
        onPlayerTurnStart();
    }
    public event Action onPlayerTurnEnd = delegate { };
    public void OnPlayerTurnEnd()
    {
        onPlayerTurnEnd();
    }
    public event Action<CardContent> onCardUsed = delegate { };
    public void OnCardUsed(CardContent cc)
    {
        onCardUsed(cc);
    }
    public event Action onEnemyTurnStart = delegate { };
    public void OnEnemyTurnStart()
    {
        onEnemyTurnStart();
    }
    public event Action onEnemyTurnEnd = delegate { };
    public void OnEnemyTurnEnd()
    {
        onEnemyTurnEnd();
    }
}
