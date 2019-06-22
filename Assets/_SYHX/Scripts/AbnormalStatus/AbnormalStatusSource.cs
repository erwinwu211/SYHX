using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Diagnostics;
using System;



namespace SYHX.AbnormalStatus
{
    public abstract class AbnormalStatusSource : ScriptableObject
    {
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
        public override void Init()
        {
            UDebug.Log("==============");
            UDebug.Log("In Start");
            var dictionary = new Dictionary<string, PropertyInfo>();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var att = (CustomDescAttribute)Attribute.GetCustomAttribute(property, typeof(CustomDescAttribute));
                if (att != null)
                {
                    dictionary.Add(att.descName, property);
                    UDebug.Log(att.descName);
                }
            }
            this.descOption = dictionary;
            UDebug.Log("===============");
        }
        public override AbnormalStatusContent Generate(BattleCharacter owner)
        {
            var abnormalStatus = new T();
            var fields = typeof(T).GetFields();
            foreach (var field in fields)
            {
                if (field.IsDefined(typeof(CloneFieldAttribute), false))
                {
                    var obj = field.GetValue(origin);
                    field.SetValue(abnormalStatus, obj);
                }
            }
            abnormalStatus.Init(this, owner, descOption);
            return abnormalStatus;
        }
    }

    public enum AbnormalStatusType
    {
        Nerf, Buff
    }
}

