using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABattleStatus {

    public virtual void Start() { }
    public virtual void Update () { }
    public virtual void End() { }
    protected BattleManager context;

    public ABattleStatus(BattleManager context)
    {
        this.context = context;
    }
}


/// <summary>
/// 1.初始化阶段
/// </summary>
public class InitializationStatus : ABattleStatus
{
    public InitializationStatus(BattleManager context) : base(context) { }
    private bool EndFlag = false;

    public override void Start()
    {
        //数据加载
        LoadData();
    }

    public override  void Update()
    {
        //当数据加载完成，打开Flag
        if (EndFlag)
        {
            End();
        }
    }

    public override void End()
    {
        context.ChangeStatus(new PlayerTurnStartStatus(context));
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
public class PlayerTurnStartStatus : ABattleStatus
{
    public PlayerTurnStartStatus(BattleManager context) : base(context) { }
    private bool EndFlag = false;

    public override void Start()
    {
        //触发BUFF

        //回复能量

        //发牌
    }

    public override void Update()
    {
        //当发牌动画播放完成，打开EndFlag
        if (EndFlag)
        {
            End();
        }
    }

    public override void End()
    {
        context.ChangeStatus(new PlayerTurnGoingStatus(context));
    }

    private void BuffEffect() { }
}




/// <summary>
/// 玩家回合进行阶段
/// </summary>
public class PlayerTurnGoingStatus : ABattleStatus
{
    public PlayerTurnGoingStatus(BattleManager context) : base(context) { }

    public override void Update()
    {
        //每帧刷新，检测玩家的点击目标
        //1.点击卡牌：使用卡牌
        //2.点击回合结束按钮：进入End()
        //3.点击道具/技能切换按钮，切换卡牌列表
        //4.点击弃牌堆，弹出弃牌堆列表
    }

    public override void End()
    {
        context.ChangeStatus(new PlayerTurnEndStatus(context));
    }

    public void OnTurnEndBtnClicked()
    {
        End();
    }
}




/// <summary>
/// 玩家回合结束阶段
/// </summary>
public class PlayerTurnEndStatus : ABattleStatus
{
    public PlayerTurnEndStatus(BattleManager context) : base(context) { }
    private bool EndFlag = false;

    public override void Start()
    {
        //触发BUFF

        //将牌移入弃牌堆

    }

    public override void Update()
    {
        if (EndFlag)
        {
            End();
        }
    }

    public override void End()
    {
        context.ChangeStatus(new EnemyTurnStartStatus(context));
    }
}




/// <summary>
/// 敌方回合开始阶段
/// </summary>
public class EnemyTurnStartStatus : ABattleStatus
{
    public EnemyTurnStartStatus(BattleManager context) : base(context) { }
    private bool EndFlag = false;

    public override void Start()
    {
        //触发BUFF

        //执行AI
    }

    public override void Update()
    {
        //当AI动作播放完成后，打开Flag
        if (EndFlag)
        {
            End();
        }
    }

    public override void End()
    {
        context.ChangeStatus(new EnemyTurnEndStatus(context));
    }
}



/// <summary>
/// 敌方回合结束阶段
/// </summary>
public class EnemyTurnEndStatus : ABattleStatus
{
    public EnemyTurnEndStatus(BattleManager context) : base(context) { }
    private bool EndFlag = false;

    public override void Start()
    {
        //触发BUFF
    }

    public override void Update()
    {
        if (EndFlag)
        {
            End();
        }
    }

    public override void End()
    {
        context.ChangeStatus(new PlayerTurnStartStatus(context));
    }
}
