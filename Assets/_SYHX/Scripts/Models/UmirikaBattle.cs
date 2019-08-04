using SYHX.Cards;
using UnityEngine.UI;

public class UmirikaBattle : BattleHero
{
    public override string Name { get => Initializer.Ins.umirika.Name; }
    public override void ChildStart()
    {
        connectionSignal = BattleManager.Ins.signals.CreateSignal<ConnectionSignal>("connection");
        BattleProgressEvent.Ins.onCardUsed += CalculateConnection;
        BattleProgressEvent.Ins.onPlayerTurnEnd += ResetConnection;
        base.ChildStart();
    }
    public Text typeField;
    public Text numberField;
    public override void RefreshUI()
    {
        base.RefreshUI();
        this.typeField.text = currentType.ToString();
        this.numberField.text = connectionSignal.signalValue.ToString();
    }
    public ConnectionSignal connectionSignal;
    public ConnectionType currentType { get; private set; }
    public void CalculateConnection(CardContent card, CardUseTrigger trigger)
    {
        CalculateConnection(card.connectionType, card.keyWords.Exists(kw => kw.Name == "承接"));
    }
    public void CalculateConnection(ConnectionType type, bool isKeyWord)
    {
        if (type == ConnectionType.无色)
        {
            connectionSignal.signalValue += 1;
            return;
        }
        if (!isKeyWord || currentType == type)
        {
            connectionSignal.signalValue = currentType == type ? connectionSignal.signalValue + 1 : 1;
            currentType = type;
        }
        RefreshUI();
    }
    public void ResetCardType()
    {
        currentType = ConnectionType.黑;
    }

    public void ResetConnection()
    {
        connectionSignal.Reset();
        if (connectionSignal.signalValue == 0) ResetCardType();
        RefreshUI();
    }
}
public class ConnectionSignal : Signal<int>
{
    public int stopReset;
    public void Reset()
    {
        if (stopReset == 0) signalValue = 0;
    }
}
