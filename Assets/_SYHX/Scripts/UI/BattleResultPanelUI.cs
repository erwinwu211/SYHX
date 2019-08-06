using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SYHX.Cards;

public class BattleResultPanelUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject winPanel;
    public GameObject losePanel;
    public Transform rewardList;
    public GameObject cardChoosePanel;
    public ToggleGroup cardChooseList;
    public GameObject cardPrefab;
    public CardSource selectedCardSource;
    private List<CraftableCardUI> CUIList;


    public void ShowWinPanel(RewardProto reward)
    {
        //显示界面
        this.gameObject.SetActive(true);
        winPanel.SetActive(true);
        losePanel.SetActive(false);

        //设置奖励信息
        for (int i = 0;i<reward.dungeonResourceReward.Count;i++)
        {
            Transform tf = rewardList.Find("item"+i);
            Transform itemBg = tf.Find("itemBg");
            Image icon = itemBg.Find("Icon").GetComponent<Image>();
            Text info = itemBg.Find("Info").GetComponent<Text>();

            itemBg.gameObject.SetActive(true);
            icon.sprite = reward.dungeonResourceReward[i].item.icon;
            info.text = reward.dungeonResourceReward[i].count+"";
        }
    }

    public void ShowLosePanel()
    {
        this.gameObject.SetActive(true);
        winPanel.SetActive(false);
        losePanel.SetActive(true);
    }

    public void ShowCardChoosePanel(List<CardSource> cardSources)
    {
        CUIList = new List<CraftableCardUI>();
        cardChoosePanel.SetActive(true);
        foreach (CardSource cs in cardSources)
        {
            CardContent cc = cs.GenerateCard();
            GameObject go = Instantiate(cardPrefab,cardChooseList.transform);
            CraftableCardUI CUI = go.GetComponent<CraftableCardUI>();
            CUI.SetCard(cc,true);
            CUIList.Add(CUI);
            go.GetComponent<Button>().onClick.AddListener(delegate{
                OnCardSelected(CUI,cs);
            });
            // Toggle toggle = go.AddComponent<Toggle>();
            // toggle.group = cardChooseList;
            // toggle.isOn = false;
            // toggle.onValueChanged.AddListener((bool value) => OnCardSelected(cs,go,value));
        }
    }

    public void OnCardSelected(CraftableCardUI selected,CardSource cs)
    {
        //已被选的话则取消选中
        //Debug.Log("CUIList的数量是"+CUIList.Count);
        if (selected.IsSelect)
        {
            selected.EnhanceSelected();
            selectedCardSource = null;
            //Debug.Log("你取消选中了"+selected.name);
            return;
        }
        //为被选的话把其它卡牌都取消选中，然后选中自己
        foreach(CraftableCardUI CUI in CUIList)
        {
            CUI.EnhanceSelected();
        }
        selected.ShowSelected();
        //Debug.Log("你选中了"+selected.name);
        selectedCardSource = cs;
    }

    public void OnConfirmBtnClick()
    {
        BattleManager.Ins.SetRewardCardSource(selectedCardSource);
        BattleManager.Ins.ReturnToDungeon();
    }

    public void OnReturnBtnClick()
    {
        BattleManager.Ins.ReturnToDungeon();
    }

    public void OnNextRewardBtnClick()
    {
        BattleManager.Ins.NextReward();
    }


}
