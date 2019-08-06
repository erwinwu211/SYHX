using System;
using SYHX.Cards;

public class BattleProgressEvent : SingletonMonoBehaviour<BattleProgressEvent>
{

    protected override void UnityAwake() { }
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

    public event Action<CardContent, CardUseTrigger> onCardUsed = delegate { };
    public void OnCardUsed(CardContent cc, CardUseTrigger trigger)
    {
        onCardUsed(cc, trigger);
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

    public event Action<BattleCharacter, BattleCharacter, DamageTrigger> onGiveDamage = delegate { };
    public void OnGiveDamage(BattleCharacter giver, BattleCharacter receiver, DamageTrigger trigger)
    {
        onGiveDamage(giver, receiver, trigger);
    }
    public event Action<BattleCharacter, BattleCharacter, DamageTrigger> onReceiveDamage = delegate { };
    public void OnReceiveDamage(BattleCharacter giver, BattleCharacter receiver, DamageTrigger trigger)
    {
        onReceiveDamage(giver, receiver, trigger);
    }

    public event Action<BattleCharacter, BattleCharacter, DamageTrigger> onGiveDamagePertime = delegate { };
    public void OnGiveDamagePertime(BattleCharacter giver, BattleCharacter receiver, DamageTrigger trigger)
    {
        onGiveDamagePertime(giver, receiver, trigger);
    }
    public event Action<BattleCharacter, BattleCharacter, DamageTrigger> onReceiveDamagePertime = delegate { };
    public void OnReceiveDamagePertime(BattleCharacter giver, BattleCharacter receiver, DamageTrigger trigger)
    {
        onReceiveDamagePertime(giver, receiver, trigger);
    }
}
