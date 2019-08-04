using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SYHX.Cards;
using UnityEngine.UI;

public class DeckInformationUI : MonoBehaviour
{
    public GameObject CardChooseGroup;
    public GameObject CardPrefab;
    public Text DataChipCount;

    /// <summary>
    /// 根据当前所在场景，设置不同的显示
    /// </summary>
    /// <param name="cardContents">卡牌列表</param>
    /// <param name="InDungeon">是否在地宫场景里，是的话还会显示资源</param>
    public void LoadDeckInfomation(List<CardContent> cardContents,bool InDungeon = false)
    {
        //设置各界面的显示关系
        gameObject.SetActive(true);
        DataChipCount.gameObject.SetActive(false);
        if (InDungeon)
        {
            DataChipCount.gameObject.SetActive(true);
            DataChipCount.text = DungeonManager.Ins.dataChipCount+"";
        }
        //清空列表上的内容
        foreach (Transform tf in CardChooseGroup.transform)
        {
            Destroy(tf.gameObject);
        }
        //根据模式为卡牌挂载不同的事件
        foreach (CardContent cc in cardContents)
        {
            GameObject go = Instantiate(CardPrefab, CardChooseGroup.transform);
            go.GetComponent<CraftableCardUI>().SetCard(cc);
            go.GetComponent<Button>().onClick.AddListener(delegate ()
            {
                //TODO
                //这里应该会放大显示它的升级信息
            });
        }
    }

    public void CloseUI()
    {
        Destroy(this.gameObject);
    }

}
