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
            BattleProgressEvent.Ins.onPlayerTurnStart += DecreaseByTurn; 
            owner.onDeath += Release;
        }
        public void Release()
        {
            BattleProgressEvent.Ins.onPlayerTurnStart -= DecreaseByTurn;
        }
        private List<AbnormalStatusContent> asList = new List<AbnormalStatusContent>();



        public void Add(AbnormalStatusSource source,int count)
        {
            if(source.canRepeat)
            {
                Creat(source);
            }
            else
            {
                Increase(source,count);
            }
        }
        public void Increase(AbnormalStatusSource source, int count)
        {
            if (source == null) return;
            var abnormalStatus = GetOrCreate(source);
            abnormalStatus.Increase(count);
        }
        public void Increase(AbnormalStatusContent content,int count)
        {
            content.Increase(count);
        }

        public AbnormalStatusContent Get(AbnormalStatusSource source)
        {
            var id = source.id;
            return asList.FirstOrDefault(ac => ac.id == source.id);
        }
        public AbnormalStatusContent Creat(AbnormalStatusSource source)
        {
            var content = source.Generate(owner);
            asList.Add(content);
            return content;
        }
        public AbnormalStatusContent GetOrCreate(AbnormalStatusSource source)
        {
            var result = Get(source);
            if (result == null)
            {
                result = Creat(source);
            }
            return result;
        }

        public List<AbnormalStatusContent> GetAll(AbnormalStatusSource source)
        {
            var id = source.id;
            return asList.Where(ac => ac.id == source.id).ToList();
        }

        public void Clear(AbnormalStatusSource source)
        {

        }
        public void Remove(AbnormalStatusSource source)
        {

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
            foreach(var content in asList)
            {
                text += content.Desc + "\n";
            }
            return text;
        }
    }
}


public partial class BattleCharacter
{
    public void AddAbnormalStatus(AbnormalStatusSource source,int count = 0)
    {
        asManager.Add(source,count);
        RefreshUI();
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


