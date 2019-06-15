using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

public abstract class CardSource : ScriptableObject
{
    #region 序列化数据
    [SerializeField] protected int mID;
    [SerializeField] protected string mName;
    [SerializeField] protected string mDesc;
    [SerializeField] protected int mEP;
    [SerializeField] public CardType cardType;
    #endregion

    #region 获取用数据
    public int ID { get => mID; }
    public string Name { get => mName; }
    public string Desc { get => mDesc; }
    public int EP { get => mEP; }
    #endregion

    public abstract void Init();

    #region 以后会都废弃

    /// <summary>
    /// ❌事件：当抽到手上时
    /// </summary>
    public virtual void OnDraw() { }

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

    #endregion

    #region 卡牌生成相关

    //TODO : 获取被生成的卡牌
    public abstract CardContent GenerateCard();

#if UNITY_EDITOR
    public virtual void GenerateToDeck()
    {
        CardManager.Ins.AddToDeck(GenerateCard());
    }
#endif
    #endregion
}

public class CardSource<T> : CardSource
where T : CardContent, new()
{
    [SerializeField] T origin;

    private Dictionary<string, PropertyInfo> descOption;
    public override void Init()
    {
        UDebug.Log("==============");
        UDebug.Log("In Start");
        var dictionary = new Dictionary<string, PropertyInfo>();
        var properties = typeof(T).GetProperties();
        foreach (var property in properties)
        {
            var att = (CardDescAttribute)Attribute.GetCustomAttribute(property, typeof(CardDescAttribute));
            if (att != null)
            {
                dictionary.Add(att.descName, property);
                UDebug.Log(att.descName);
            }
        }
        this.descOption = dictionary;
        UDebug.Log("===============");
    }
    public override CardContent GenerateCard()
    {
        var cc = new T();
        var fields = typeof(T).GetFields();
        foreach (var field in fields)
        {
            if (field.IsDefined(typeof(CloneFieldAttribute), false))
            {
                var obj = field.GetValue(origin);
                field.SetValue(cc, obj);
            }
        }
        cc.SetOwnerWithDic(this, this.descOption);
        return cc;
    }

}
public enum CardType
{
    强袭技, 灵巧技, 神秘技, 连接技
}


/*
 *用来做卡牌原型模式初期化
 */
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public sealed class CloneFieldAttribute : Attribute { public CloneFieldAttribute() { } }