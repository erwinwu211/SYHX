using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using SYHX.AbnormalStatus;
namespace SYHX.AbnormalStatus
{

    public class AbnormalStatusManager : Assitant<BattleCharacter>
    {
        public AbnormalStatusManager(BattleCharacter owner) : base(owner)
        {
            owner.onDeath += Release;
        }
        public void Release()
        {
            foreach (var asC in asList)
            {
                asC.Release();
            }
        }
        private List<AbnormalStatusContent> asList = new List<AbnormalStatusContent>();



        public AbnormalStatusContent Add(AbnormalStatusSource source, int count, params object[] args)
        {
            if (source.canRepeat)
            {
                return Creat(source, count, args);
            }
            else
            {
                return Increase(source, count);
            }
        }
        public AbnormalStatusContent Increase(AbnormalStatusSource source, int count)
        {
            if (source == null) return null;
            var abnormalStatus = GetOrCreate(source, count);
            return abnormalStatus;
        }
        public void Increase(AbnormalStatusContent content, int count)
        {
            content.Increase(count);
        }

        public AbnormalStatusContent Get(AbnormalStatusSource source)
        {
            var id = source.id;
            return asList.FirstOrDefault(ac => ac.id == source.id);
        }
        public AbnormalStatusContent Creat(AbnormalStatusSource source, int count, params object[] args)
        {
            var content = source.Generate(owner, args);
            content.Increase(count);
            asList.Add(content);
            owner.OnAddAStatus();
            return content;
        }
        public AbnormalStatusContent GetOrCreate(AbnormalStatusSource source, int count)
        {
            var result = Get(source);
            if (result == null)
            {
                result = Creat(source, count);
            }
            return result;
        }

        public List<AbnormalStatusContent> GetAll(AbnormalStatusSource source)
        {
            var id = source.id;
            return asList.Where(ac => ac.id == source.id).ToList();
        }

        public void Clear(AbnormalStatusContent content)
        {
            content.Clear();
            asList.Remove(content);
        }
        public void Remove(AbnormalStatusContent content)
        {
            content.Remove();
            asList.Remove(content);
        }

        public bool ContainAbnormalStatus(AbnormalStatusSource source)
        {
            var result = asList.FirstOrDefault(ac => ac.id == source.id);
            return result == null ? false : true;
        }

        private void DecreaseByTurn()
        {
            asList
            .Where(content => content.isTurnBased)
            .ToList()
            .ForEach(content => content.Decrease(1));
            owner.RefreshUI();
        }

        public string GetText()
        {
            string text = "";
            foreach (var content in asList)
            {
                text += content.Desc + "\n";
            }
            return text;
        }
    }
}


public partial class BattleCharacter
{
    public AbnormalStatusContent AddAbnormalStatus(AbnormalStatusSource source, int count = 0, params object[] args)
    {
        var content = asManager.Add(source, count, args);
        RefreshUI();
        return content;
    }
    public void RemoveAbnormalStatus(AbnormalStatusContent content)
    {
        asManager.Remove(content);
        RefreshUI();
    }
    public void ClearAbnormalStatus(AbnormalStatusContent content)
    {
        asManager.Clear(content);
        RefreshUI();
    }

}


