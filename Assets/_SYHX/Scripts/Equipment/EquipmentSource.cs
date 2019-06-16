using System.Collections.Generic;
using UnityEngine;
using SYHX.Cards;

public abstract class EquipmentSource : ScriptableObject
{
    [SerializeField] protected int mID;
    [SerializeField] protected string mName;
    [SerializeField] protected string mDesc;
    [SerializeField] protected int mAttack;
    [SerializeField] protected int mHp;
    [SerializeField] protected int mDefend;
    [SerializeField] protected List<CardSource> mCards;

    public int ID { get => mID; }
    public string Name { get => mName; }
    public string Desc { get => mDesc; }
    public int Attack { get => mAttack; }
    public int Hp { get => mHp; }
    public int Defend { get => mDefend; }
    public List<CardSource> Cards { get => mCards; }
}
