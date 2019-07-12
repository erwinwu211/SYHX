using System.Collections.Generic;
using System.Reflection;
using System;

namespace SYHX.AbnormalStatus
{
    public abstract partial class AbnormalStatusContent : Content
    {
        #region meta数据
        public int id { get; private set; }
        public int count { get; protected set; } = 0;
        public bool isTurnBased { get; private set; }
        public bool showCount { get; private set; }
        public bool canRepeat { get; private set; }
        public AbnormalStatusType type { get; private set; }
        private string desc;

        public string Desc => GetDesc(desc);
        public Dictionary<string, PropertyInfo> descOption;

        #endregion

        public AbnormalStatusSource source { get; protected set; }
        protected BattleCharacter owner;

        public void Init(AbnormalStatusSource source, BattleCharacter owner, Dictionary<string, PropertyInfo> descOption, params object[] args)
        {
            this.source = source;
            id = source.id;
            this.isTurnBased = source.isTurnBased;
            this.showCount = source.showCount;
            this.canRepeat = source.canRepeat;
            this.type = source.type;
            this.owner = owner;
            this.descOption = descOption;
            this.desc = source.desc;
            Generate(args);
        }

        public virtual void ReceiveParameters(params object[] args) { }
        public virtual void Increase(int count)
        {
            this.count += count;
            if (count > 0)
            {
                this.OnIncrease();
            }
            if (this.count == 0)
            {
                owner.ClearAbnormalStatus(this);
            }
        }
        public virtual void Decrease(int count)
        {
            this.count -= count;
            if (this.count <= 0)
            {
                count = 0;
                this.OnDecrease();
                owner.ClearAbnormalStatus(this);
            }
            else
            {
                this.OnDecrease();
            }
        }

        //生成时
        public void Generate(params object[] args)
        {
            this.count = 0;
            if (isTurnBased)
            {
                AddEndAction(TurnDecrease);
            }
            this.OnGenerate(args);
        }
        //净化
        public void Remove()
        {
            this.count = 0;
            Release();
            this.OnRemove();
        }

        //自然消失
        public void Clear()
        {
            this.count = 0;
            Release();
            this.OnClear();
        }

        public virtual void Release()
        {
            if (isTurnBased)
            {
                RemoveEndAction(TurnDecrease);
            }
        }
        public void TurnDecrease()
        {
            this.Decrease(1);
        }

        #region 重写事件

        public virtual void OnDecrease() { }
        public virtual void OnIncrease() { }
        public virtual void OnRemove() => OnClear();
        public virtual void OnClear() { }
        public virtual void OnGenerate(params object[] args) { }

        #endregion

        public virtual string GetDesc(string desc)
        {
            foreach (KeyValuePair<string, PropertyInfo> pairs in descOption)
            {
                desc = desc.Replace("{" + pairs.Key + "}", (string)pairs.Value.GetValue(this));
            }
            return desc;
        }

    }


    //action controller
    public abstract partial class AbnormalStatusContent
    {
        public void AddEndAction(Action action)
        {
            if (owner.isEnemy)
            {
                BattleProgressEvent.Ins.onEnemyTurnEnd += action;
            }
            else
            {
                BattleProgressEvent.Ins.onPlayerTurnEnd += action;
            }
        }

        public void RemoveEndAction(Action action)
        {
            if (owner.isEnemy)
            {
                BattleProgressEvent.Ins.onEnemyTurnEnd -= action;
            }
            else
            {
                BattleProgressEvent.Ins.onPlayerTurnEnd -= action;
            }
        }

        public void AddStartAction(Action action)
        {
            if (owner.isEnemy)
            {
                BattleProgressEvent.Ins.onEnemyTurnStart += action;
            }
            else
            {
                BattleProgressEvent.Ins.onPlayerTurnStart += action;
            }
        }

        public void RemoveStartAction(Action action)
        {
            if (owner.isEnemy)
            {
                BattleProgressEvent.Ins.onEnemyTurnStart -= action;
            }
            else
            {
                BattleProgressEvent.Ins.onPlayerTurnStart -= action;
            }
        }
    }
}


