using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABattleState
{

    public virtual void Start() { }
    public virtual void Update() { }
    public virtual void End() { }
    protected BattleManager context;

    public ABattleState(BattleManager context)
    {
        this.context = context;
    }
}


/// <summary>
/// 1.初始化阶段
/// </summary>
public class InitializationState : ABattleState
{
    public InitializationState(BattleManager context) : base(context) { }
    private bool EndFlag = false;

    public override void Start()
    {
        //数据加载
        LoadData();

        //初始化回合数
        context.RountCount = 0;
    }

    public override void Update()
    {
        //当数据加载完成，打开Flag
        if (EndFlag)
        {
            End();
        }
    }

    public override void End()
    {
        context.ChangeStatus(new PlayerTurnStartState(context));
    }

    //数据加载的方法
    private void LoadData()
    {
        EndFlag = true;
    }
}




/// <summary>
/// 玩家回合开始阶段
/// </summary>
public class PlayerTurnStartState : ABattleState
{
    public PlayerTurnStartState(BattleManager context) : base(context) { }
    private bool EndFlag = false;

    public override void Start()
    {
        //回合数加1
        context.RountCount++;

        //触发BUFF
        context.BuffTrigger();

        //回复能量
        context.EnergyPointRegain();

        //发牌
        context.Draw(context.DrawCountPerTurn);
    }

    public override void Update()
    {
        //当发牌动画播放完成，打开EndFlag
        Check();

        if (EndFlag)
        {
            End();
        }
    }

    public override void End()
    {
        if (context.IsBattleOver() == BattleResult.Continue)
        {
            context.ChangeStatus(new PlayerTurnGoingState(context));
        }
    }

    private void Check()
    {
        EndFlag = true;
    }

}




/// <summary>
/// 玩家回合进行阶段
/// </summary>
public class PlayerTurnGoingState : ABattleState
{
    public PlayerTurnGoingState(BattleManager context) : base(context) { }

    public override void Update()
    {
        //每帧刷新，检测玩家的点击目标
        //1.点击卡牌：使用卡牌
        //2.点击回合结束按钮：进入End()
        //3.点击道具/技能切换按钮，切换卡牌列表
        //4.点击弃牌堆，弹出弃牌堆列表
        //5.点击敌人，将敌人选中为目标
        if (Input.GetMouseButtonDown(0))
        {
            Camera c = GameObject.Find("UI Camera").GetComponent<Camera>();
            Ray ray = c.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            Physics.Raycast(ray, out hitInfo);
            if (Physics.Raycast(ray, out hitInfo))
            {
                Debug.DrawLine(ray.origin, hitInfo.point);
                GameObject gameObj = hitInfo.collider.gameObject;
                //当射线碰撞目标为卡牌类型，使用卡牌
                if (gameObj.tag == "Card")
                {
                    Debug.Log("Use Card");
                }
                if (gameObj.name == "TurnEndBtn")
                {
                    End();
                }
                if (gameObj.name == "")
                {

                }
            }
        }
    }

    public override void End()
    {
        if (context.IsBattleOver() == BattleResult.Continue)
        {
            context.ChangeStatus(new PlayerTurnEndState(context));
        }
    }

    public void OnTurnEndBtnClicked()
    {
        End();
    }
}




/// <summary>
/// 玩家回合结束阶段
/// </summary>
public class PlayerTurnEndState : ABattleState
{
    public PlayerTurnEndState(BattleManager context) : base(context) { }
    private bool EndFlag = false;

    public override void Start()
    {
        //触发BUFF
        context.BuffTrigger();

        //将牌移入弃牌堆

    }

    public override void Update()
    {
        //当各种动画完成后打开Flag
        Check();

        if (EndFlag)
        {
            End();
        }
    }

    public override void End()
    {
        if (context.IsBattleOver() == BattleResult.Continue)
        {
            context.ChangeStatus(new EnemyTurnStartState(context));
        }
    }

    private void Check()
    {
        EndFlag = true;
    }
}




/// <summary>
/// 敌方回合开始阶段
/// </summary>
public class EnemyTurnStartState : ABattleState
{
    public EnemyTurnStartState(BattleManager context) : base(context) { }
    private bool EndFlag = false;

    public override void Start()
    {
        //触发BUFF
        context.BuffTrigger();

        //执行AI
    }

    public override void Update()
    {
        //当AI动作播放完成后，打开Flag
        Check();

        if (EndFlag)
        {
            End();
        }
    }

    public override void End()
    {
        if (context.IsBattleOver() == BattleResult.Continue)
        {
            context.ChangeStatus(new EnemyTurnEndState(context));
        }
    }

    private void Check()
    {
        EndFlag = true;
    }
}



/// <summary>
/// 敌方回合结束阶段
/// </summary>
public class EnemyTurnEndState : ABattleState
{
    public EnemyTurnEndState(BattleManager context) : base(context) { }
    private bool EndFlag = false;

    public override void Start()
    {
        //触发BUFF
        context.BuffTrigger();
    }

    public override void Update()
    {
        //当各种动画播放完成后，打开Flag
        Check();

        if (EndFlag)
        {
            End();
        }
    }


    public override void End()
    {
        if (context.IsBattleOver() == BattleResult.Continue)
        {
            context.ChangeStatus(new PlayerTurnStartState(context));
        }
    }


    private void Check()
    {
        EndFlag = true;
    }
}




public class ResultState : ABattleState
{
    public ResultState(BattleManager context) : base(context) { }

    public override void Start()
    {
    }

    public void OnConfirmBtnClicked()
    {
        context.BattleEnd();
    }
}
