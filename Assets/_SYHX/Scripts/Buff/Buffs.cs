using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SYHX.Buff
{

    public class Buffs : Assitant<BattleCharacter>
    {
        public Buffs(BattleCharacter owner) : base(owner) { }
        private Dictionary<int, Buff> buffDisc = new Dictionary<int, Buff>();
        public void Increase(BuffSource buffSource, int count)
        {
            if (buffSource == null) return;
            var buff = GetOrCreate(buffSource);
            buff.Increase(count);
        }
        public Buff GetOrCreate(BuffSource buffSource)
        {
            if (buffDisc.ContainsKey(buffSource.id))
            {
                return buffDisc[buffSource.id];
            }
            var buff = buffSource.Generate(owner);
            buffDisc.Add(buffSource.id, buff);
            if (buffSource.isTurnBased)
            {
                BattleProgressEvent.Ins.onPlayerTurnStart += () => DecreaseByTurn(buff);
            }
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

        public void DecreaseByTurn(Buff buff)
        {
            buff.Decrease(1);
        }

        public T GetSource<T>()
        where T : BuffSource
        {
            var res = BuffData.Ins.Get<T>();
            if (res != null) return GetBuff(res) as T;
            return null;
        }
        public Buff GetBuff(BuffSource source)
        {
            if (ContainBuff(source)) return buffDisc[source.id];
            return null;
        }
    }


}

