using System.Collections;
using System.Collections.Generic;
using SYHX.Cards;
using UnityEngine;
using UnityEngine.UI;

public class DeckInformationUI : MonoBehaviour {
    public GameObject CardChooseGroup;
    public GameObject CardPrefab;
    public GameObject CardDetailPanel;
    public Transform CardDetailPos;
    public Button PrevBtn;
    public Button NextBtn;
    public Text DataChipCount;

    private List<CardContent> cardContents;

    /// <summary>
    /// 根据当前所在场景，设置不同的显示
    /// </summary>
    /// <param name="cardContents">卡牌列表</param>
    /// <param name="InDungeon">是否在地宫场景里，是的话还会显示资源</param>
    public void LoadDeckInfomation (List<CardContent> cardContents, bool InDungeon = false) {
        //设置各界面的显示关系
        this.cardContents = cardContents;
        gameObject.SetActive (true);
        DataChipCount.gameObject.SetActive (false);
        if (InDungeon) {
            DataChipCount.gameObject.SetActive (true);
            DataChipCount.text = DungeonManager.Ins.dataChip.count + "";
        }
        //清空列表上的内容
        foreach (Transform tf in CardChooseGroup.transform) {
            Destroy (tf.gameObject);
        }
        //根据模式为卡牌挂载不同的事件
        foreach (CardContent cc in cardContents) {
            GameObject go = Instantiate (CardPrefab, CardChooseGroup.transform);
            go.GetComponent<CraftableCardUI> ().SetCard (cc);
            go.GetComponent<Button> ().onClick.AddListener (delegate () {
                //TODO
                //这里应该会放大显示它的升级信息
                ShowCardDetail (cc);
            });
        }
    }

    /// <summary>
    /// 显示单张卡牌的放大详情
    /// </summary>
    /// <param name="cc"></param>
    public void ShowCardDetail (CardContent cc) {
        //进行界面的重置
        CardDetailPanel.SetActive (true);
        PrevBtn.gameObject.SetActive (true);
        NextBtn.gameObject.SetActive (true);
        PrevBtn.onClick.RemoveAllListeners ();
        NextBtn.onClick.RemoveAllListeners ();
        foreach (Transform tf in CardDetailPos) Destroy (tf.gameObject);

        //刷新界面
        GameObject go = Instantiate (CardPrefab, CardDetailPos);

        //绑定Prev按钮事件
        if (cardContents.IndexOf (cc) == 0) {
            PrevBtn.gameObject.SetActive (false);
        } else {
            PrevBtn.onClick.AddListener (delegate () {
                ShowCardDetail (cardContents[cardContents.IndexOf (cc) - 1]);
            });
        }

        //绑定Next按钮事件
        if (cardContents.IndexOf (cc) == cardContents.Count - 1) {
            NextBtn.gameObject.SetActive (false);
        } else {
            NextBtn.onClick.AddListener (delegate () {
                ShowCardDetail (cardContents[cardContents.IndexOf (cc) + 1]);
            });
        }
    }

    public void EnhanceCardDetail () {
        CardDetailPanel.SetActive (false);
    }

    public void CloseUI () {
        Destroy (this.gameObject);
    }

}