using UnityEngine;
using System.Collections;


namespace SYHX.EnemyAI
{
    public class GiveBuffSource : EnemyActionSource<GiveBuff> { }

    [System.Serializable]
    public class GiveBuff : EnemyActionContent
    {
        [Header("异常状态")] [CloneField] public SYHX.AbnormalStatus.AbnormalStatusSource source;
        [Header("回合数")] [CloneField] public int count;

        protected override IEnumerator execute()
        {
            enemy.AddAbnormalStatus(source, count);
            yield break;
        }
    }
}

