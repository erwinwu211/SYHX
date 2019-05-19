using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : SingletonMonoBehaviour<Characters>
{
    [SerializeField] protected string mName;
    [SerializeField] protected int mStr;
    [SerializeField] protected int mAgi;
    [SerializeField] protected int mInt;
    [SerializeField] protected List<CardSource> mCards;

    public string Name { get => mName; }
    public int Str { get => mStr; }
    public int Agi { get => mAgi; }
    public int Int { get => mInt; }
    public List<CardSource> Cards { get => mCards; }
}
