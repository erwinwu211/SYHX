using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestPanelUI : MonoBehaviour
{
    public Button RestBtn;
    public Button LevelUpBtn;
    public Button TechLevelUpBtn;
    public Text CoinCount;
    public void ShowRestRoomUI(bool canRest, LvUpCheck canLvUpCheck, LvUpCheck canTechLvUp)
    {
        gameObject.SetActive(true);
        CoinCount.text = DungeonManager.Ins.chipCore.count + "";
        //设置按钮是否可点击
        RestBtn.interactable = canRest;
        switch (canLvUpCheck)
        {
            case LvUpCheck.yes:
                LevelUpBtn.interactable = true;
                break;
            case LvUpCheck.max:
                LevelUpBtn.interactable = false;
                break;
            case LvUpCheck.cost_unenough:
                LevelUpBtn.interactable = false;
                break;
        }
        switch (canTechLvUp)
        {
            case LvUpCheck.yes:
                TechLevelUpBtn.interactable = true;
                break;
            case LvUpCheck.max:
                TechLevelUpBtn.interactable = false;
                break;
            case LvUpCheck.cost_unenough:
                TechLevelUpBtn.interactable = false;
                break;
        }
    }

    public void OnRestBtnClick()
    {
        DungeonManager.Ins.Rest();
        OnLeaveBtnClick();
    }

    public void OnLevelUpBtn()
    {
        CharacterInDungeon.Ins.LevelUp();
        OnLeaveBtnClick();
    }

    public void OnTechLevelUpBtn()
    {
        DungeonManager.Ins.TechLevelUp();
        OnLeaveBtnClick();
    }

    public void OnLeaveBtnClick()
    {
        gameObject.SetActive(false);
        DungeonManager.Ins.currentEvent.Finished();
    }
}
