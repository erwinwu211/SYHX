using System;
using UnityEngine;

/// <summary>
/// 单例对象
/// </summary>
public abstract class Singleton<T>
where T : Singleton<T>, new()
{
    private static T ins;
    public static T Ins
    {
        get
        {
            if (ins == null)
            {
                ins = new T();
            }
            return ins;
        }
    }
    public virtual void Destroy() => Reset();
    public static void Reset() => ins = null;
}

/// <summary>
/// 单例behaviour对象，主要是用于Manager之类的
/// </summary>
public abstract class SingletonMonoBehaviour<T> : MonoBehaviour
     where T : SingletonMonoBehaviour<T>
{
    public static T Ins { get; private set; }

    protected abstract void UnityAwake();

    private void Awake()
    {
        if (enabled == false)
        {
            return;
        }
        if (Ins == null)
        {
            //ゲーム開始時にGameManagerをinstanceに指定ß
            Ins = this as T;
            UnityAwake();
        }
        else if (Ins != this)
        {
        }
        else
        {
            // Do Nothing
        }
    }

    protected virtual void OnDestroy()
    {
        Ins = null;
    }
}

