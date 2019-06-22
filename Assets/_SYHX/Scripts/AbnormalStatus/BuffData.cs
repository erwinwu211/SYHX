using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SYHX.AbnormalStatus;

public class BuffData : SingletonMonoBehaviour<BuffData>
{
    public List<AbnormalStatusSource> buffSources;
    protected override void UnityAwake() { }
    public T Get<T>()
    where T : AbnormalStatusSource
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
