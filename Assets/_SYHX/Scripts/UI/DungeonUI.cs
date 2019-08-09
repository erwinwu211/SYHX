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
    public Text ChipCoreCount;
    public Text ScoreCount;
    public Text NextRewardScoreCount;

    public Image Avatar;

    public Slider HpSlider;
    public GameObject ForceInfo;
    public GameObject AgileInfo;
    public GameObject ConstitutionInfo;
    public GameObject FortuneInfo;

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
        HpCount.text = CharacterInDungeon.Ins.currentHp + " / " + CharacterInDungeon.Ins.maxHp;
        HpSlider.maxValue = CharacterInDungeon.Ins.maxHp;
        HpSlider.value = CharacterInDungeon.Ins.currentHp;
        FloorCount.text = "Area " + DungeonManager.Ins.Floor;
        DataFragCount.text = DungeonManager.Ins.dataFrag.count + "";
        ChipCoreCount.text = DungeonManager.Ins.chipCore.count + "";
        ScoreCount.text = DungeonManager.Ins.score + "";
        ForceInfo.transform.Find("LvSlider/Slider").GetComponent<Slider>().value = (float)CharacterInDungeon.Ins.Force.currentLv / 5;
        ForceInfo.transform.Find("Lv/Count").GetComponent<Text>().text = CharacterInDungeon.Ins.Force + "";
        AgileInfo.transform.Find("LvSlider/Slider").GetComponent<Slider>().value = (float)CharacterInDungeon.Ins.Agile.currentLv / 5;
        AgileInfo.transform.Find("Lv/Count").GetComponent<Text>().text = CharacterInDungeon.Ins.Agile + "";
        ConstitutionInfo.transform.Find("LvSlider/Slider").GetComponent<Slider>().value = (float)CharacterInDungeon.Ins.Constitution.currentLv / 5;
        ConstitutionInfo.transform.Find("Lv/Count").GetComponent<Text>().text = CharacterInDungeon.Ins.Constitution + "";
        FortuneInfo.transform.Find("LvSlider/Slider").GetComponent<Slider>().value = (float)CharacterInDungeon.Ins.Fortune.currentLv / 5;
        FortuneInfo.transform.Find("Lv/Count").GetComponent<Text>().text = CharacterInDungeon.Ins.Fortune + "";
        NextRewardScoreCount.text = "距下级奖励：" + "";
    }

    public void OnDeckBtnClick()
    {
        DungeonManager.Ins.Disable();
        GameObject go = Instantiate(DeckInformationUI, transform.parent);
        go.GetComponent<DeckInformationUI>().LoadDeckInfomation(CharacterInDungeon.Ins.Deck, true);
    }
}
