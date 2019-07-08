using UnityEngine;
using System.Collections.Generic;
using SYHX.AbnormalStatus;
using SYHX.Cards;
using Sirenix.OdinInspector;

public class Initializer : SingletonMonoBehaviour<Initializer>
{
    [SerializeField] public List<AbnormalStatusSource> asSource;
    [SerializeField] public List<CardSource> cSource;
    [SerializeField] public List<EnemyActionSource> eaSource;
    [TableList] public ModificationFactor[] factors;

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
