using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SYHX.Cards;

public class DungeonUI : MonoBehaviour
{
    public GameObject DeckInformationUI;
    public Text HpCount;
    public Text FloorCount;
    public Text DataFragCount;
    public Text FoodCount;
    public Text NextLevelCount;
    public Text ScoreCount;
    public Text NextRewardScoreCount;

    #region 角色信息面板
    public Image Avatar;
    public Text LvCount;
    public Text AttackCount;
    public Text DefendCount;
    public Text EP;
    public Text DrawCount;
    public Slider HpSlider;
    public GameObject ForceInfo;
    public GameObject AgileInfo;
    public GameObject ConstitutionInfo;
    public GameObject FortuneInfo;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        RefreshUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RefreshUI()
    {
        LvCount.text = CharacterInDungeon.Ins.currentLv + "";
        HpCount.text = CharacterInDungeon.Ins.currentHp + " / " + CharacterInDungeon.Ins.maxHp;
        HpSlider.maxValue = CharacterInDungeon.Ins.maxHp;
        HpSlider.value = CharacterInDungeon.Ins.currentHp;
        AttackCount.text = CharacterInDungeon.Ins.Attack + "";
        DefendCount.text = CharacterInDungeon.Ins.Defend + "";
        EP.text = CharacterInDungeon.Ins.maxEp + "";
        DrawCount.text = CharacterInDungeon.Ins.Draw_count + "";

        FloorCount.text = "Area " + DungeonManager.Ins.Floor;
        DataFragCount.text = DungeonManager.Ins.dataFrag.count + "";
        FoodCount.text = DungeonManager.Ins.food+"";
        NextLevelCount.text = (CharacterInDungeon.Ins.maxExp - CharacterInDungeon.Ins.currentExp) + "";
        ScoreCount.text = DungeonManager.Ins.score + "";
        ForceInfo.transform.Find("Lv/Count").GetComponent<Text>().text = CharacterInDungeon.Ins.Force.currentLv + "";
        AgileInfo.transform.Find("Lv/Count").GetComponent<Text>().text = CharacterInDungeon.Ins.Agile.currentLv + "";
        ConstitutionInfo.transform.Find("Lv/Count").GetComponent<Text>().text = CharacterInDungeon.Ins.Constitution.currentLv + "";
        FortuneInfo.transform.Find("Lv/Count").GetComponent<Text>().text = CharacterInDungeon.Ins.Fortune.currentLv + "";
        NextRewardScoreCount.text = "距下级奖励：" + "";
    }

    public void OnDeckBtnClick()
    {
        DungeonManager.Ins.Disable();
        GameObject go = Instantiate(DeckInformationUI, transform.parent);
        go.GetComponent<DeckInformationUI>().LoadDeckInfomation(CharacterInDungeon.Ins.Deck, true);
    }
}
