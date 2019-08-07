using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Assitant<T>
{
    [HideInInspector] public T owner;
    public Assitant(T owner)
    {
        this.owner = owner;
    }
}

public class SAssitant<T>
{
    [NonSerialized] [HideInInspector] public T owner;
    public SAssitant() { }
    public virtual void SetOwner(T owner)
    {
        this.owner = owner;
    }
}

public class AssitantMonobehaviour<T> : MonoBehaviour
where T : Component
{
    [SerializeField] private T _owner;
    public T owner { get => _owner; }
}
