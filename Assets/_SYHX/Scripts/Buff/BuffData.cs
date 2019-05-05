using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SYHX.Buff;

public class BuffData : SingletonMonoBehaviour<BuffData>
{
    public List<BuffSource> buffSources;
    protected override void UnityAwake() { }
    public T Get<T>()
    where T : BuffSource
    {
        foreach (var b in buffSources)
        {
            var res = b as T;
            if (res != null)
            {
                return res;
            }
        }
        return null;
    }
}
