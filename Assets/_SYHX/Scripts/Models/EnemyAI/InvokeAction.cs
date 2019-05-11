using System;
using HutongGames.PlayMaker;

[ActionCategory("SYHX")]
[ActionTarget(typeof(EnemyAction), "action", false)]
[Tooltip("Action Invoker（激活敌方行动）")]
public class InvokeAction : FsmStateAction
{
    public EnemyActionSource actionSource;
    private EnemyAIHandler handler;
    public override void Awake()
    {
        handler = Fsm.GameObject.GetComponent<EnemyAIHandler>();
    }
    public override void OnEnter()
    {
        if (actionSource != null)
        {
            var action = actionSource.GenerateAction();
            handler.enemy.SetAction(action);
            action.SetEnemy(handler.enemy);
        }
        Finish();
    }
}
