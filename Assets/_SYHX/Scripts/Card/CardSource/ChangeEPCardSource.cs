using System.Collections;
using UnityEngine;
namespace SYHX.Cards
{
    [SourceName("能量")]
    public class ChangeEPCardSource : CardSource<ChangeEPCardContent> { }

    [System.Serializable]
    public class ChangeEPCardContent : CardContent
    {
        [Header("获得能量")] [CloneField] public int gainEP;
        [CustomDesc("gainEP")] public string dRateString { get => gainEP.ToString(); }
        protected override IEnumerator UseEffect(CardUseTrigger trigger)
        {
            BattleManager.sChangeEnergy(gainEP);
            yield break;
        }
    }

}
