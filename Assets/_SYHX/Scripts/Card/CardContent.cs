using System;
using System.Collections.Generic;
using System.Reflection;

namespace SYHX.Cards
{
    public abstract class CardContent : SAssitant<CardSource>
    {
        public string name { get; private set; }
        private string desc;
        public string Desc => GetDesc(desc);
        public Dictionary<string, PropertyInfo> descOption;
        public BattleCardUI bUI;
        public void RefreshUI() => bUI?.RefreshUI();
        public CardContent()
        { }
        public void SetOwnerWithDic(CardSource owner, Dictionary<string, PropertyInfo> descOption = null)
        {
            this.owner = owner;
            this.EP = owner.EP;
            this.tempEP = owner.EP;
            this.cardType = owner.cardType;
            this.name = owner.Name;
            this.desc = owner.Desc;
            this.descOption = descOption;
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
        public void OnDraw() => owner.OnDraw();

        public bool CanUse() => TurnManager.Ins.stateManager.playerTurnState.IsCurrent() && BattleManager.SGetEP() >= this.EP && UseOption();
        protected virtual bool UseOption() => true;
        /// <summary>
        /// ❌事件：当卡牌在打出后，经过选择之后的效果
        /// </summary>
        public virtual void OnUse(CardUseTrigger trigger)
        {
            if (CanUse())
            {
                BattleManager.SChangeEnergy(-this.EP);
                UseEffect(trigger);
                BattleCardManager.Ins.Used(this);
            }

        }

        protected abstract void UseEffect(CardUseTrigger trigger);
        public void OnFold() => owner.OnFold();
        public void OnExiled() => owner.OnExiled();
        public void OnOtherCardUse(CardSource context) => owner.OnOtherCardUse(context);
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

