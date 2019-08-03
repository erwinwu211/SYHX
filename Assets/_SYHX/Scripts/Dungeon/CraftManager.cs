using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SYHX.Cards;

public class CraftManager : SingletonMonoBehaviour<CraftManager>
{
    protected override void UnityAwake() { }

    public CraftUI mUI;
    public CardContent selectedCard = null;
    public CardContent targetCard = null;
    public CraftMode craftMode;
    public bool canUseFlag = true;
    private CraftRoomEvent roomEvent;

    /// <summary>
    /// 接收传来的事件
    /// </summary>
    /// <param name="_event"></param>
    public void ReceiveEvent(CraftRoomEvent _event)
    {
        roomEvent = _event;
        canUseFlag = true;
        selectedCard = null;
        targetCard = null;
        OpenCraft(CraftMode.Upgrade);
    }


    /// <summary>
    /// 打开工坊面板
    /// </summary>
    /// <param name="mode"></param>
    public void OpenCraft(CraftMode mode)
    {
        craftMode = mode;
        mUI.ShowCraftUI(CharacterInDungeon.Ins.Deck, mode);
    }


    /// <summary>
    /// 离开工坊
    /// </summary>
    public void LeaveCraft()
    {
        mUI.EnhanceCraftUI();
        roomEvent.Finished();
    }


    /// <summary>
    /// 将一张牌选为被选中牌
    /// </summary>
    /// <param name="cc"></param>
    public void SetSelectedCard(CardContent cc)
    {
        selectedCard = cc;
    }


    /// <summary>
    /// 将一张牌选为目标牌
    /// </summary>
    /// <param name="cc"></param>
    public void SetTargetCard(CardContent cc)
    {
        targetCard = cc;
    }


    /// <summary>
    /// 升级一张牌
    /// </summary>
    public void UpgradeCard()
    {
        if (canUseFlag)
        {
            CharacterInDungeon.Ins.ChangeCard(selectedCard, targetCard);
            canUseFlag = false;
            LeaveCraft();
        }
        else
        {
            Debug.Log("已经升级过一次无法再升了");
        }
    }

    /// <summary>
    /// 清空选中牌与目标牌
    /// </summary>
    public void ClearChoose()
    {
        selectedCard = null;
        targetCard = null;
    }


    /// <summary>
    /// 从选目标牌处回退到选选中牌处
    /// </summary>
    public void ReturnToCardList()
    {
        ClearChoose();
        mUI.EnhanceUpgradePanel();
    }

    public void ExhaustCard()
    {
        if (canUseFlag)
        {
            CharacterInDungeon.Ins.RemoveCard(selectedCard);
        }
        else
        {
            Debug.Log("已经升级过一次无法再升了");
        }
    }

    

    public void CraftFinished()
    {
        craftMode = CraftMode.Default;
        canUseFlag = true;
        selectedCard = null;
        targetCard = null;
    }
}

public enum CraftMode
{
    Default = 0,
    Upgrade = 1,
    ChangeColor = 2,
    Exhaust = 3,
    Information =4,
}