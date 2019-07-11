using HutongGames.PlayMaker;

namespace SYHX.EnemyAI
{

    [ActionCategory("SYHX")]
    [Tooltip("循环行动")]
    public class LoopSwitcher : FsmStateAction
    {
        [RequiredField] public FsmInt turn;
        [RequiredField] public FsmEvent[] events;
        public override void OnEnter()
        {
            var index = turn.Value % events.Length;
            turn.Value++;
            Fsm.Event(events[index]);
            Finish();
        }
    }

}


