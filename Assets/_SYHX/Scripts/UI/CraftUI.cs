using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SYHX.Cards;
using UnityEngine.UI;

public class CraftUI : MonoBehaviour
{
    public GameObject CardChooseGroup;
    public GameObject CardPrefab;
    public GameObject UpgradePanel;
    public GameObject DungeonUI;
    public GameObject CloseBtn;
    public Text DataChipCount;

    /// <summary>
    /// 根据模式显示工坊界面
    /// 模式1-升级模式：
    /// 卡牌上挂载的事件为【点击后显示该牌的可升级选项】
    /// 
    /// 模式2-换色模式
    /// 卡牌上挂载的事件为【点击后显示该牌的可换色选项】
    /// 
    /// 模式3-丢牌模式
    /// 卡牌上挂载的事件为【将该牌选中】
    /// 界面中有确定按钮，按钮事件只有在有选中的牌的情况下才可触发【将该牌弃置】
    /// 
    /// 模式4-详情模式
    /// 卡牌上挂载的事件为【将该牌放大居中显示】
    /// </summary>
    /// <param name="cardContents">卡牌列表</param>
    /// <param name="mode">模式</param>
    public void ShowCraftUI(List<CardContent> cardContents,CraftMode mode)
    {
        DungeonUI.SetActive(false);
        gameObject.SetActive(true);
        UpgradePanel.SetActive(false);
        CloseBtn.GetComponent<ButtonEffect>().ResetFillBg();
        DataChipCount.text = DungeonManager.Ins.dataChipCount+"";
        foreach (Transform tf in CardChooseGroup.transform)
        {
            Destroy(tf.gameObject);
        }
        foreach (CardContent cc in cardContents)
        {
            GameObject go = Instantiate(CardPrefab, CardChooseGroup.transform);
            go.GetComponent<CraftableCardUI>().SetCard(cc);
            go.GetComponent<Button>().onClick.AddListener(delegate ()
            {
                CraftManager.Ins.SetSelectedCard(cc);
                ShowUpgradeList(cc);
            });
        }
    }

    public void EnhanceCraftUI()
    {
        gameObject.SetActive(false);
        DungeonUI.SetActive(true);
    }

    public void ShowUpgradeList(CardContent cc)
    {
        UpgradePanel.SetActive(true);
        Transform selectedCardTF = UpgradePanel.transform.Find("SelectedCard");
        Transform chooseListTF = UpgradePanel.transform.Find("ChooseList");
        Text costText = UpgradePanel.transform.Find("Arrow/Cost/Text").GetComponent<Text>();
        foreach (Transform tf in selectedCardTF) Destroy(tf.gameObject);
        foreach (Transform tf in chooseListTF) Destroy(tf.gameObject);
        foreach (CardSource cs in cc.owner.upgradeList)
        {
            CardContent cardcontent = cs.GenerateCard();
            GameObject go = Instantiate(CardPrefab, chooseListTF);
            CraftableCardUI CUI = CardPrefab.GetComponent<CraftableCardUI>();
            CUI.SetCard(cardcontent);
            go.GetComponent<Button>().onClick.AddListener(delegate ()
            {
                CraftManager.Ins.SetTargetCard(cardcontent);
            });
        }
    }


    public void EnhanceUpgradePanel()
    {
        UpgradePanel.SetActive(false);
    }
}
