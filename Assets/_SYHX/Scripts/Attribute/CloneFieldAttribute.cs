using System;

/*
 *用来做卡牌原型模式初期化
 */
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public sealed class CloneFieldAttribute : Attribute { public CloneFieldAttribute() { } }