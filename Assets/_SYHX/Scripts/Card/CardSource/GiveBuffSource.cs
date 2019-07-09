using SYHX.AbnormalStatus;
using System.Collections;
using UnityEngine;
namespace SYHX.Cards
{
    [SourceName("给与buff")]
    public class GiveBuffSource : CardSource<GiveBuffContent> { }

    [System.Serializable]
    public class GiveBuffContent : CardContent
    {
        [Header("异常状态")] [CloneField] public AbnormalStatusSource source;
        [Header("回合数")] [CloneField] public int count;

        protected override IEnumerator UseEffect(CardUseTrigger trigger)
        {
            BattleManager.Hero.AddAbnormalStatus(source, count);
            yield break;
        }
    }


}