using System.Collections;
using System.Collections.Generic;
using System;

public class SignalController
{
    private Dictionary<string, object> signals = new Dictionary<string, object>();
    public T CreateSignal<T>(string signal)
    where T : Signal, new()
    {
        var sig = new T();
        signals.Add(signal, sig);
        return sig;
    }
    public T GetSignal<T>(string signal)
    where T : Signal
    {
        T obj;
        if (signals.ContainsKey(signal) && (obj = signals[signal] as T) != null)
        {
            return obj;
        }
        return null;
    }
}

public class Signal
{
    public Signal() { }

}

public class Signal<T> : Signal
{
    public bool stopChange;
    private bool isAction = false;
    private T subject;
    public T signalValue
    {
        get => subject;
        set
        {
            if (stopChange) return;
            subject = value;
            if (!isAction)
            {
                isAction = true;
                foreach (var action in actionPool.Values)
                {
                    action(subject);
                }
                isAction = false;
            }
        }
    }
    protected Dictionary<int, Action<T>> actionPool = new Dictionary<int, Action<T>>();
    protected int index = 0;
    public int Attach(Action<T> action)
    {
        var key = index;
        actionPool.Add(index++, action);
        return key;
    }
    public void DeAttach(int index)
    {
        actionPool.Remove(index);
    }
}
