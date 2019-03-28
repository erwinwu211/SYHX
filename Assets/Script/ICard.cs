using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class ICard
{
    protected int mID;
    protected string mName;
    protected string mDesc;

    public int ID { get { return mID; } }
    public string Name { get { return mName; } }
    public string Desc { get { return mDesc; } }
    

    /// <summary>
    /// ❌事件：当抽到手上时
    /// </summary>
    public virtual void OnDraw() { }

    /// <summary>
    /// ❌事件：当卡牌在打出后，经过选择之后的效果
    /// </summary>
    public virtual void OnUse() { }

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
    public virtual void OnOtherCardUse(ICard context) { }

}