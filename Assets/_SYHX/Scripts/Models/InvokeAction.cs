using System;
using HutongGames.PlayMaker;

[ActionCategory("SYHX")]
[Tooltip("Action Invoker（激活敌方行动）")]
public class InvokeAction : FsmStateAction
{
    private EnemyAIHandler handler;
    public override void Awake()
    {
        handler = Fsm.GameObject.GetComponent<EnemyAIHandler>();
    }
    public override void OnEnter()
    {

    }
}
