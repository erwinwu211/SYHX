using SYHX.AbnormalStatus;
using HutongGames.PlayMaker;

namespace SYHX.EnemyAI
{

    [ActionCategory("SYHX")]
    [ActionTarget(typeof(AbnormalStatusSource), "异常状态", false)]
    [Tooltip("Action Invoker（激活敌方行动）")]
    public class StartWithBuff : FsmStateAction
    {
        public AbnormalStatusSource source;
        public int count;
        private EnemyAIHandler handler;
        public override void Awake()
        {
            handler = Fsm.GameObject.GetComponent<EnemyAIHandler>();
        }

        public override void OnEnter()
        {
            handler.enemy.AddAbnormalStatus(source, count);
            Finish();
        }
    }
}