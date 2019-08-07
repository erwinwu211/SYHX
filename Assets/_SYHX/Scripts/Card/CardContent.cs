using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System;

namespace SYHX.Cards
{
    public abstract class CardContent : SAssitant<CardSource>, Content
    {
        public string name { get; private set; }

        public Dictionary<string, PropertyInfo> descOption;
        public List<CardKeyWord> keyWords { get; private set; }
        protected List<string> battleKeyword = new List<string>();
        public void AddBattleAttribute(string keyword)
        {
            if (!battleKeyword.Contains(keyword))
            {
                battleKeyword.Add(keyword);
                RefreshUI();
            }
        }
        public void DeleteBattleAttribute(string keyword)
        {
            battleKeyword.Remove(keyword);
            RefreshUI();
        }
        [NonSerialized] [HideInInspector] public BattleCardUI bUI;
        public void RefreshUI() => bUI?.RefreshUI();
        public CardContent() { }



        public void SetOwnerWithDic(CardSource owner, ConnectionType type, Dictionary<string, PropertyInfo> descOption = null)
        {
            this.owner = owner;
            this.EP = owner.EP;
            this.tempEP = owner.EP;
            this.cardType = owner.cardType;
            this.rarity = owner.rarity;
            this.connectionType = type;
            this.name = owner.Name;
            this.desc = owner.Desc;
            this.descOption = descOption;
            this.keyWords = new List<CardKeyWord>(owner.keyWords);
        }
        public ConnectionType connectionType { get; private set; }
        public CardType cardType { get; private set; }
        public Rarity rarity { get; private set; }

        private int ep;
        public int EP
        {
            get => ep;
            private set => ep = value >= 0 ? value : 0;
        }

        private int tempEP;
        public virtual int TempEP
        {
            get
            {
                if (battleKeyword.Exists(s => s == "0")) return 0;
                return tempEP;
            }
            protected set
            {
                tempEP = value >= 0 ? value : 0;
                RefreshUI();
            }
        }
        public void OnDraw() { }

        public bool CanUse() => TurnManager.Ins.stateManager.playerTurnState.IsCurrent() && BattleManager.sGetEP() >= this.TempEP && UseOption();
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
            BattleCardManager.Ins.TempUse(this);
            yield return UseEffect(trigger);
            if (keyWords.Exists(kw => kw.Name == "移除"))
            {
                BattleCardManager.Ins.Exhaust(this);
            }
            else
            {
                BattleCardManager.Ins.Used(this);
            }
            BattleProgressEvent.Ins.OnCardUsed(this, trigger);
            yield break;
        }

        protected abstract IEnumerator UseEffect(CardUseTrigger trigger);
        public void OnDiscard() { }
        public void OnExhaust() { }
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

