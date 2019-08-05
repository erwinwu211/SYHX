using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SYHX.Cards;

public class CraftManager : SingletonMonoBehaviour<CraftManager>
{
    protected override void UnityAwake() { }

    public CraftUI mUI;
    public CardContent selectedCard = null;
    public CardSource targetCard = null;
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
        ClearChoose();
        mUI.ShowCraftUI(CharacterInDungeon.Ins.Deck, mode);
    }


    /// <summary>
    /// 离开工坊，结束本次工坊事件
    /// </summary>
    public void LeaveCraft()
    {
        craftMode = CraftMode.Default;
        mUI.EnhanceCraftUI();
        ClearChoose();
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
    public void SetTargetCard(CardSource cs)
    {
        targetCard = cs;
    }


    /// <summary>
    /// 升级一张牌
    /// </summary>
    public void UpgradeCard()
    {
        if (canUseFlag && selectedCard != null && targetCard != null)
        {
            CardContent cc = targetCard.GenerateCard();
            CharacterInDungeon.Ins.ChangeCard(selectedCard, cc);
            DungeonManager.Ins.DecreaseDataChip(targetCard.upgradeCost);
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


    public void ExhaustCard()
    {
        if (canUseFlag && selectedCard != null)
        {
            CharacterInDungeon.Ins.RemoveCard(selectedCard);
        }
        else
        {
            Debug.Log("已经升级过一次无法再升了");
        }
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