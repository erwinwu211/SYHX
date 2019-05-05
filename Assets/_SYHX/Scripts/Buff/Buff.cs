using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SYHX.Buff
{
    public abstract class Buff
    {
        public BuffSource source { get; protected set; }
        public bool isActive { get; protected set; }
        protected BattleCharacter owner;
        protected Dictionary<string, float> optionalValue { get; private set; } = new Dictionary<string, float>();
        public int count { get; protected set; }
        public virtual void Increase(int count)
        {
            this.count += count;
            if (count == 0)
            {
                isActive = false;
                this.OnClear(owner);
            }
        }
        public virtual void Decrease(int count)
        {
            this.count -= count;
            if (count <= 0)
            {
                count = 0;
                isActive = false;
                this.OnClear(owner);
            }
        }
        public virtual void Clear(BattleCharacter owner)
        {
            this.count = 0;
            this.isActive = false;
            this.OnRemove(owner);
        }
        public virtual void OnAdd(BattleCharacter owner) { }
        public virtual void OnRemove(BattleCharacter owner) { }
        public virtual void OnClear(BattleCharacter owner) { }
        public virtual void OnGenerate(BattleCharacter owner) { }
        public abstract void Init(BuffSource source, BattleCharacter owner, object constant);
    }

    public abstract class Buff<T1, T2, T3> : Buff
    where T1 : Buff<T1, T2, T3>, new()
    where T2 : BuffSource
    where T3 : struct
    {
        public T3 options;
        public override void Init(BuffSource source, BattleCharacter owner, object constant)
        {
            this.owner = owner;
            this.source = source;
            var initOption = constant.GetType().GetFields();
            foreach (var option in initOption)
            {
                if (option.GetType() == typeof(float))
                {
                    optionalValue.Add(option.Name, (float)option.GetValue(constant));
                }
            }
        }
    }
}


