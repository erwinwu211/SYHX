using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SYHX.Buff
{
    public abstract class BuffSource : ScriptableObject
    {
        public int id;
        public bool isTurnBased;
        public bool showCount;
        public abstract Buff Generate(BattleCharacter owner);
    }

    public class BuffSource<T1, T2> : BuffSource
    where T1 : Buff, new()
    where T2 : struct
    {
        public T2 constant;
        public override Buff Generate(BattleCharacter owner)
        {
            var buff = new T1();
            buff.Init(this, owner, constant);
            buff.OnGenerate(owner);
            return buff;
        }
    }
}

