using System;
using HutongGames.PlayMaker;

[ActionCategory("SYHX")]
[ActionTarget(typeof(EnemyActionSource), "action", false)]
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
            actionSource.ParseEnemy(handler.enemy);
            handler.enemy.SetAction(actionSource.GeneratedAction());
            Finish();
        }
    }
}
