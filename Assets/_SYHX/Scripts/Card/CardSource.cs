using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SYHX.Cards
{

    public abstract class CardSource : ScriptableObject, Source
    {
        #region 序列化数据
        [SerializeField] protected int mID;
        [SerializeField] protected string mName;
        [Multiline] [SerializeField] protected string mDesc;
#if UNITY_EDITOR

        [ShowInInspector] protected string mname => LocalizationManager.EditorGetData("CardName", mID.ToString());
        protected string mdesc => LocalizationManager.EditorGetData("CardDesc", mID.ToString());
#endif

        [SerializeField] protected int mEP;
        [SerializeField] public int upgradeCost;
        // [SerializeField] public ConnectionType connectionType;
        [SerializeField] public CardType cardType;
        [SerializeField] public List<ConnectionType> connectionTypes;
        [SerializeField] public Rarity rarity;
        [SerializeField] public List<CardSource> upgradeList;
        [SerializeField] public List<CardKeyWord> keyWords;
        #endregion

        #region 获取用数据
        public int ID { get => mID; set => mID = value; }
        public string Name { get => mName; }
        public string Desc { get => mDesc; }
        public int EP { get => mEP; }
        #endregion

        public abstract void Init();

        #region 卡牌生成相关
        public abstract CardContent GenerateCard();
        public abstract CardContent GenerateCard(ConnectionType type);

        [Button(ButtonSizes.Large)]
        [Conditional("UNITY_EDITOR")]
        public virtual void GenerateToDeck()
        {
            var cc = GenerateCard();
            BattleCardManager.Ins.GenerateCardTo(cc, CardPosition.Deck);
            cc.RefreshUI();
        }

        [Button(ButtonSizes.Large)]
        [Conditional("UNITY_EDITOR")]
        public void DrawCard()
        {
            BattleCardManager.Ins.Draw(1);
        }
        #endregion
    }

    public class CardSource<T> : CardSource
    where T : CardContent, new()
    {
        [BoxGroup("原型")] [HideLabel] [SerializeField] T origin;

        private Dictionary<string, PropertyInfo> descOption;
        public override void Init() => descOption = this.InitDescOption<CardSource, T>();
        public override CardContent GenerateCard()
        {
            var count = connectionTypes.Count;
            var randomIndex = (new System.Random()).Next(count);
            return GenerateCard(connectionTypes[randomIndex]);
        }

        public override CardContent GenerateCard(ConnectionType type)
        {
            var cc = this.GenerateContent<CardSource, T>(origin);
            cc.SetOwnerWithDic(this, type, this.descOption);
            return cc;
        }

    }
    public enum ConnectionType
    {
        红,
        蓝,
        黄,
        无色,
        黑
    }

    public enum CardType
    {
        攻击,
        技能,
        防御
    }

    public enum Rarity
    {
        初始,
        初级,
        中级,
        高级
    }

    public enum AttackTargetType
    {
        mark,
        random,
        aoe
    }
}