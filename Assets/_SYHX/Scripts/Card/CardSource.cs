using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class CardSource : ScriptableObject
{
    [SerializeField] protected int mID;
    [SerializeField] protected string mName;
    [SerializeField] protected string mDesc;
    [SerializeField] protected int mEP;

    public int ID { get => mID; }
    public string Name { get => mName; }
    public string Desc { get => mDesc; }
    public int EP { get => mEP; }

    public CardType cardType;

    public bool CanUse(CardContent cc) => TurnManager.Ins.stateManager.playerTurnState.IsCurrent() && BattleManager.Ins.GetEP() > cc.EP && UseOption(cc);
    protected virtual bool UseOption(CardContent cc) => true;

    /// <summary>
    /// ❌事件：当抽到手上时
    /// </summary>
    public virtual void OnDraw() { }

    /// <summary>
    /// ❌事件：当卡牌在打出后，经过选择之后的效果
    /// </summary>
    public void OnUse(CardContent cc)
    {
        if (!CanUse(cc)) return;
        BattleManager.Ins.ChangeEnergy(-EP);
        Effect(cc);
    }
    public virtual void Effect(CardContent cc) { }

    /// <summary>
    /// ❌事件：当弃牌时
    /// </summary>
    public virtual void OnFold() { }

    /// <summary>
    /// ❌事件：当被放逐时
    /// </summary>
    public virtual void OnExiled() { }

    /// <summary>
    /// 事件：当其它的卡牌被使用时触发
    /// </summary>
    public virtual void OnOtherCardUse(CardSource context) { }


    //TODO : 现在是加入抽牌堆，以后改进为加入牌堆
    public virtual void GenerateCard(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var cc = new CardContent();
            cc.SetOwner(this);
            CardManager.Ins.AddToDeck(cc);
        }
    }

}
public enum CardType
{
    强袭技, 灵巧技, 神秘技, 连接技
}