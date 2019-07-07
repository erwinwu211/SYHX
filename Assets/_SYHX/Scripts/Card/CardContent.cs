using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace SYHX.Cards
{
    public abstract class CardContent : SAssitant<CardSource>, Content
    {
        public string name { get; private set; }

        public Dictionary<string, PropertyInfo> descOption;
        private List<CardKeyWord> keyWords;
        public BattleCardUI bUI;
        public void RefreshUI() => bUI?.RefreshUI();
        public CardContent() { }



        public void SetOwnerWithDic(CardSource owner, Dictionary<string, PropertyInfo> descOption = null)
        {
            this.owner = owner;
            this.EP = owner.EP;
            this.tempEP = owner.EP;
            this.cardType = owner.cardType;
            this.name = owner.Name;
            this.desc = owner.Desc;
            this.descOption = descOption;
            this.keyWords = new List<CardKeyWord>(owner.keyWords);
        }
        public CardType cardType { get; private set; }

        private int ep;
        public int EP
        {
            get => ep;
            private set => ep = value >= 0 ? value : 0;
        }

        private int tempEP;
        public int TempEP
        {
            get => tempEP;
            private set => tempEP = value >= 0 ? value : 0;
        }
        public void OnDraw() { }

        public bool CanUse() => TurnManager.Ins.stateManager.playerTurnState.IsCurrent() && BattleManager.sGetEP() >= this.EP && UseOption();
        protected virtual bool UseOption() => true;
        /// <summary>
        /// ❌事件：当卡牌在打出后，经过选择之后的效果
        /// </summary>
        public virtual void OnUse(CardUseTrigger trigger)
        {
            if (CanUse())
            {
                BattleManager.ManagedCoroutine(UseCard(trigger));
            }

        }

        public IEnumerator UseCard(CardUseTrigger trigger)
        {
            BattleManager.sChangeEnergy(-this.EP);
            yield return UseEffect(trigger);
            BattleCardManager.Ins.Used(this);
            yield break;
        }

        protected abstract IEnumerator UseEffect(CardUseTrigger trigger);
        public void OnFold() { }
        public void OnExiled() { }
        public void OnOtherCardUse(CardSource context) { }
        private string desc;
        public string Desc => GetDesc(desc);
        public virtual string GetDesc(string desc)
        {
            foreach (KeyValuePair<string, PropertyInfo> pairs in descOption)
            {
                desc = desc.Replace("{" + pairs.Key + "}", (string)pairs.Value.GetValue(this));
            }
            return desc;
        }
    }

}
public enum CardUseTrigger
{
    ByUser, ByCard
}

