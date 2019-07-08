using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Diagnostics;
using System;



namespace SYHX.AbnormalStatus
{
    public abstract class AbnormalStatusSource : ScriptableObject, Source
    {
        public string Name;
        public int id;
        public bool isTurnBased;
        public bool showCount;
        public bool canRepeat;
        public string desc;
        public AbnormalStatusType type;

        public abstract void Init();

        public abstract AbnormalStatusContent Generate(BattleCharacter owner);
    }


    public class AbnormalStatusSource<T> : AbnormalStatusSource
    where T : AbnormalStatusContent, new()
    {
        [SerializeField] public T origin;
        private Dictionary<string, PropertyInfo> descOption;
        public override void Init() => descOption = this.InitDescOption<AbnormalStatusSource, T>();
        public override AbnormalStatusContent Generate(BattleCharacter owner)
        {
            var abnormalStatus = this.GenerateContent<AbnormalStatusSource, T>(origin);
            abnormalStatus.Init(this, owner, descOption);
            return abnormalStatus;
        }
    }

    public enum AbnormalStatusType
    {
        Nerf, Buff
    }
}

