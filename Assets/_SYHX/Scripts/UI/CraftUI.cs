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
    public GameObject CloseBtn;
    public GameObject PriceLabel;
    public Transform UpgradeCardPos;
    public Transform UpgradeChooseListPos;
    public Text DataChipCount;
    public Color Unenough;
    public Color Enough;
    public ButtonEffect ReturnBtn;

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
    public void ShowCraftUI(List<CardContent> cardContents, CraftMode mode)
    {
        //设置各界面的显示关系
        gameObject.SetActive(true);
        UpgradePanel.SetActive(false);
        CloseBtn.GetComponent<ButtonEffect>().ResetFillBg();
        DataChipCount.text = DungeonManager.Ins.dataFrag.count + "";
        //清空列表上的内容
        foreach (Transform tf in CardChooseGroup.transform)
        {
            Destroy(tf.gameObject);
        }
        //根据模式为卡牌挂载不同的事件
        foreach (CardContent cc in cardContents)
        {
            int myLevel = 0;
            switch (cc.cardType)
            {
                case CardType.攻击:
                    myLevel = CharacterInDungeon.Ins.Force.currentLv;
                    break;
                case CardType.防御:
                    myLevel = CharacterInDungeon.Ins.Agile.currentLv;
                    break;
                case CardType.技能:
                    myLevel = CharacterInDungeon.Ins.Constitution.currentLv;
                    break;
                case CardType.诅咒:
                    myLevel = CharacterInDungeon.Ins.Fortune.currentLv;
                    break;
            }
            if (cc.needAttrLevel > myLevel) continue;
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
    }

    public void ShowUpgradeList(CardContent cc)
    {
        //进行重置
        UpgradePanel.SetActive(true);
        foreach (Transform tf in UpgradeCardPos) Destroy(tf.gameObject);
        foreach (Transform tf in UpgradeChooseListPos) Destroy(tf.gameObject);
        ReturnBtn.ResetFillBg();
        //显示选中卡牌信息
        {
            GameObject go = Instantiate(CardPrefab, UpgradeCardPos);
            go.GetComponent<CraftableCardUI>().SetCard(cc, false, false);
        }
        //显示升级分支信息
        foreach (CardSource cs in cc.owner.upgradeList)
        {
            CardContent cardcontent = cs.GenerateCard();
            GameObject go = Instantiate(CardPrefab, UpgradeChooseListPos);
            go.GetComponent<CraftableCardUI>().SetCard(cardcontent, true, false);
            GameObject price = Instantiate(PriceLabel, go.transform);
            //判断是否有足够的金钱来显示不同的字体颜色
            string colorString;
            if (DungeonManager.Ins.dataFrag.count > cs.upgradeCost)
            {
                colorString = ColorUtility.ToHtmlStringRGB(Enough);
            }
            else
            {
                colorString = ColorUtility.ToHtmlStringRGB(Unenough);
            }
            price.GetComponentInChildren<Text>().text = "数据碎片 <color=#" + colorString + ">" + cs.upgradeCost + "</color>";
            go.GetComponent<Button>().onClick.AddListener(delegate ()
            {
                //CraftManager.Ins.SetTargetCard(cs);
                OnCardSelected(go.GetComponent<CraftableCardUI>(), cs);
            });
        }
    }

    public void OnCardSelected(CraftableCardUI selected, CardSource cs)
    {
        //已被选的话则取消选中
        //Debug.Log("CUIList的数量是"+CUIList.Count);
        if (selected.IsSelect)
        {
            selected.EnhanceSelected();
            CraftManager.Ins.targetCard = null;
            //Debug.Log("你取消选中了"+selected.name);
            return;
        }
        //为被选的话把其它卡牌都取消选中，然后选中自己
        foreach (Transform tf in UpgradeChooseListPos)
        {
            tf.GetComponent<CraftableCardUI>().EnhanceSelected();
        }
        selected.ShowSelected();
        //Debug.Log("你选中了"+selected.name);
        CraftManager.Ins.SetTargetCard(cs);
    }


    public void OnConfirmBtnClick()
    {
        CraftManager.Ins.UpgradeCard();
        CraftManager.Ins.LeaveCraft();
    }

    public void OnLeaveBtnClick()
    {
        CraftManager.Ins.LeaveCraft();
    }

    public void OnReturnListBtnClick()
    {
        CraftManager.Ins.ClearChoose();
        EnhanceUpgradePanel();
    }

    public void EnhanceUpgradePanel()
    {
        UpgradePanel.SetActive(false);
    }
}
