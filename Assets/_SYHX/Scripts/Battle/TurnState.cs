using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurnState : Assitant<TurnStateManager>
{
    protected TurnState_FSM fsm => owner.owner.fsmManager;

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
    public abstract string FsmName();
    public void TrySetCurrent()
    {
        fsm.TryTransition(this);
    }
    public bool IsCurrent()
    {
        return base.owner.current == this;
    }
    protected BattleManager context;

    public TurnState(TurnStateManager owner) : base(owner) { }
}


/// <summary>
/// 1.初始化阶段
/// </summary>
public class FirstState : TurnState
{
    public FirstState(TurnStateManager owner) : base(owner) { }
    private bool EndFlag = false;

    public override void Enter()
    {
        //数据加载
        LoadData();

        //初始化回合数
        // context.RountCount = 0;
        owner.owner.fsmManager.TryTransition(owner.playerStartState);
    }

    public override void Update()
    {
        //当数据加载完成，打开Flag
        if (EndFlag)
        {
            Exit();
        }
    }

    public override void Exit() { }
    public override string FsmName() => "First";

    //数据加载的方法
    private void LoadData()
    {
        EndFlag = true;
    }
}




/// <summary>
/// 玩家回合开始阶段
/// </summary>
public class PlayerStartState : TurnState
{
    public PlayerStartState(TurnStateManager owner) : base(owner) { }
    private bool EndFlag = false;

    public override void Enter()
    {
        BattleManager.SAddTurn();
        BattleProgressEvent.Ins.OnPlayerTurnStart();
        BattleManager.SEnergyPointRegain();
        BattleManager.STurnStartDraw();
        owner.owner.fsmManager.TryTransition(owner.playerTurnState);

    }

    public override void Update()
    {
        //当发牌动画播放完成，打开EndFlag
        Check();

        if (EndFlag)
        {
            Exit();
        }
    }

    public override void Exit() { }
    public override string FsmName() => "PlayerStart";

    private void Check()
    {
        EndFlag = true;
    }

}




/// <summary>
/// 玩家回合进行阶段
/// </summary>
public class PlayerTurnState : TurnState
{
    public PlayerTurnState(TurnStateManager owner) : base(owner) { }

    public override void Update()
    {
        //每帧刷新，检测玩家的点击目标
        //1.点击卡牌：使用卡牌
        //2.点击回合结束按钮：进入End()
        //3.点击道具/技能切换按钮，切换卡牌列表
        //4.点击弃牌堆，弹出弃牌堆列表
        //5.点击敌人，将敌人选中为目标
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Camera c = GameObject.Find("UI Camera").GetComponent<Camera>();
        //     Ray ray = c.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit hitInfo;
        //     Physics.Raycast(ray, out hitInfo);
        //     if (Physics.Raycast(ray, out hitInfo))
        //     {
        //         Debug.DrawLine(ray.origin, hitInfo.point);
        //         GameObject gameObj = hitInfo.collider.gameObject;
        //         //当射线碰撞目标为卡牌类型，使用卡牌
        //         if (gameObj.tag == "Card")
        //         {
        //             Debug.Log("Use Card");
        //         }
        //         if (gameObj.name == "TurnEndBtn")
        //         {
        //             owner.owner.fsmManager.TryTransition(owner.playerEndState);

        //             Exit();
        //         }
        //         if (gameObj.name == "")
        //         {

        //         }
        //     }
        // }
    }

    public override void Exit() { }
    public override string FsmName() => "PlayerTurn";


    public void OnTurnEndBtnClicked()
    {
        Exit();
    }
}




/// <summary>
/// 玩家回合结束阶段
/// </summary>
public class PlayerEndState : TurnState
{
    public PlayerEndState(TurnStateManager owner) : base(owner) { }
    private bool EndFlag = false;

    public override void Enter()
    {
        BattleProgressEvent.Ins.OnPlayerTurnEnd();

        //将牌移入弃牌堆
        owner.owner.fsmManager.TryTransition(owner.enemyStartState);


    }

    public override void Update()
    {
        //当各种动画完成后打开Flag
        Check();

        if (EndFlag)
        {
            Exit();
        }
    }

    public override void Exit() { }
    public override string FsmName() => "PlayerEnd";


    private void Check()
    {
        EndFlag = true;
    }
}




/// <summary>
/// 敌方回合开始阶段
/// </summary>
public class EnemyStartState : TurnState
{
    public EnemyStartState(TurnStateManager owner) : base(owner) { }
    private bool EndFlag = false;

    public override void Enter()
    {
        BattleProgressEvent.Ins.OnEnemyTurnStart();
        owner.owner.fsmManager.TryTransition(owner.enemyEndState);
        //执行AI
    }

    public override void Update()
    {
        //当AI动作播放完成后，打开Flag
        Check();

        if (EndFlag)
        {
            Exit();
        }
    }

    public override void Exit() { }
    public override string FsmName() => "EnemyStart";


    private void Check()
    {
        EndFlag = true;
    }
}



/// <summary>
/// 敌方回合结束阶段
/// </summary>
public class EnemyEndState : TurnState
{
    public EnemyEndState(TurnStateManager context) : base(context) { }
    private bool EndFlag = false;

    public override void Enter()
    {
        BattleProgressEvent.Ins.OnEnemyTurnEnd();
        owner.owner.fsmManager.TryTransition(owner.playerStartState);

    }

    public override void Update()
    {
        //当各种动画播放完成后，打开Flag
        Check();

        if (EndFlag)
        {
            Exit();
        }
    }


    public override void Exit() { }

    public override string FsmName() => "EnemyEnd";



    private void Check()
    {
        EndFlag = true;
    }
}




public class ResultState : TurnState
{
    public ResultState(TurnStateManager context) : base(context) { }

    public override void Enter()
    {
        BattleManager.Ins.EndBattle();
    }

    public void OnConfirmBtnClicked()
    {
        context.BattleEnd();
    }
    public override string FsmName() => "Result";

}
