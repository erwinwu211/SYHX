using System.Collections.Generic;
using System.Reflection;

namespace SYHX.AbnormalStatus
{
    public abstract class AbnormalStatusContent : Content
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

        public void Init(AbnormalStatusSource source, BattleCharacter owner, Dictionary<string, PropertyInfo> descOption)
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
        }


        public bool isActive { get; protected set; }
        public virtual void Increase(int count)
        {
            if (!this.isActive)
            {
                Generate();
            }
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
            if (this.isActive && this.count <= 0)
            {
                count = 0;
                this.OnDecrease();
                owner.ClearAbnormalStatus(this);
            }
            else if (this.isActive)
            {
                this.OnDecrease();
            }
        }

        //生成时
        public void Generate()
        {
            this.count = 0;
            this.isActive = true;
            this.OnGenerate();
        }
        //净化
        public void Remove()
        {
            this.count = 0;
            this.isActive = false;
            this.OnRemove();
        }

        //自然消失
        public void Clear()
        {
            this.count = 0;
            this.isActive = false;
            this.OnClear();
        }

        public virtual void OnDecrease() { }
        public virtual void OnIncrease() { }
        public virtual void OnRemove() => OnClear();
        public virtual void OnClear() { }
        public virtual void OnGenerate() { }

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


