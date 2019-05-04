using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffData : SingletonMonoBehaviour<BuffData>
{
    public List<BuffSource> buffSources;
    protected override void UnityAwake() { }
}
