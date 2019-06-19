using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SYHX.Buff
{

    public class BuffManager : Assitant<BattleCharacter>
    {
        public BuffManager(BattleCharacter owner) : base(owner) { }
        private Dictionary<int, BuffContent> buffDisc = new Dictionary<int, BuffContent>();
        public void Increase(BuffSource buffSource, int count)
        {
            if (buffSource == null) return;
            var buff = GetOrCreate(buffSource);
            buff.Increase(count);
        }
        public BuffContent GetOrCreate(BuffSource buffSource)
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

        public void DecreaseByTurn(BuffContent buff)
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
        public BuffContent GetBuff(BuffSource source)
        {
            if (ContainBuff(source)) return buffDisc[source.id];
            return null;
        }
    }


}

