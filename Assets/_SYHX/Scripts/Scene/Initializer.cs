using UnityEngine;
using System.Collections.Generic;
using SYHX.AbnormalStatus;
using SYHX.Cards;
using Sirenix.OdinInspector;
using SYHX.EnemyAI;

public class Initializer : SingletonMonoBehaviour<Initializer>
{
    [SerializeField] public List<AbnormalStatusSource> asSource;
    [SerializeField] public List<CardSource> cSource;
    [SerializeField] public List<EnemyActionSource> eaSource;
    [SerializeField] public DamageMono damageSource;
    [TableList] public ModificationFactor[] factors;
    [TableList] public LvInfo[] lvInfos;

    protected override void UnityAwake()
    {
        GameObject.DontDestroyOnLoad(this);
        foreach (var card in cSource)
        {
            card.Init();
        }
        foreach (var abnormal in asSource)
        {
            abnormal.Init();
        }
        foreach (var action in eaSource)
        {
            action.Init();
        }
    }
}