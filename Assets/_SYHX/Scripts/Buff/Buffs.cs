using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffs : Assitant<BattleCharacter>
{
    public Buffs(BattleCharacter owner) : base(owner) { }
    private Dictionary<int, Buff> buffDisc = new Dictionary<int, Buff>();
    public void Increase(BuffSource buffSource, int count)
    {
        if (buffSource == null) return;
        GetOrCreate(buffSource);
    }
    public Buff GetOrCreate(BuffSource buffSource)
    {
        if (buffDisc.ContainsKey(buffSource.id))
        {
            return buffDisc[buffSource.id];
        }
        var buff = buffSource.Generate(owner);
        buffDisc.Add(buffSource.id, buff);
        return buff;
    }

    public bool ContainBuff(BuffSource buffSource)
    {
        if (buffDisc.ContainsKey(buffSource.id))
        {
            return buffDisc[buffSource.id].isActive;
        }
        return false;
    }
}
