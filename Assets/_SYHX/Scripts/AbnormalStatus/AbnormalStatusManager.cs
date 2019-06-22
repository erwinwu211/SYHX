using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace SYHX.AbnormalStatus
{

    public class AbnormalStatusManager : Assitant<BattleCharacter>
    {
        public AbnormalStatusManager(BattleCharacter owner) : base(owner) { }
        private List<AbnormalStatusContent> asList = new List<AbnormalStatusContent>();
        public void Increase(AbnormalStatusSource source, int count)
        {
            if (source == null) return;
            var abnormalStatus = GetOrCreate(source);
            abnormalStatus.Increase(count);
        }
        public AbnormalStatusContent GetOrCreate(AbnormalStatusSource source)
        {
            var id = source.id;
            var result = asList.FirstOrDefault(ac => ac.id == source.id);
            if (result == null)
            {
                result = source.Generate(owner);
            }
            return result;
        }

        public AbnormalStatusContent GetAbnormalStatus(AbnormalStatusSource source)
        {
            return asList.FirstOrDefault(ac => ac.id == source.id);
        }

        public bool ContainAbnormalStatus(AbnormalStatusSource source)
        {
            var result = asList.FirstOrDefault(ac => ac.id == source.id);
            return result == null ? false : true;
        }

        public void DecreaseByTurn()
        {
            asList
            .Where(content => content.isTurnBased)
            .ToList()
            .ForEach(content => content.Decrease(1));
        }

        public T GetSource<T>()
        where T : AbnormalStatusSource
        {
            var res = BuffData.Ins.Get<T>();
            if (res != null) return GetAbnormalStatus(res) as T;
            return null;
        }

    }


}

