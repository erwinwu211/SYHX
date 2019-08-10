using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SYHX.Cards;

public class RestPanelUI : MonoBehaviour
{
    public Button RestBtn;
    public Text RestDesc;
    public Button ForceTrainingBtn;
    public Text ForceLevelCount;
    public Text ForceExp;
    public Text ForceRewardDesc;
    public GameObject ForceTrainingDesc;
    public Slider ForceSlider;

    public Button AgileTrainingBtn;
    public Text AgileLevelCount;
    public Text AgileExp;
    public Text AgileRewardDesc;
    public GameObject AgileTrainingDesc;
    public Slider AgileSlider;

    public Button ConstitutionTrainingBtn;
    public Text ConstitutionLevelCount;
    public Text ConstitutionExp;
    public Text ConstitutionRewardDesc;
    public GameObject ConstitutionTrainingDesc;
    public Slider ConstitutionSlider;
    public void ShowRestRoomUI(BasicAttribute force, BasicAttribute agile, BasicAttribute constitution)
    {
        RestDesc.text = "恢复" + DungeonManager.Ins.RestEfficiency * 100 + "%生命值";

        gameObject.SetActive(true);
        ForceLevelCount.text = "力量强化Lv." + force.currentLv + "";
        AgileLevelCount.text = "敏捷强化Lv." + agile.currentLv + "";
        ConstitutionLevelCount.text = "智力强化Lv." + constitution.currentLv + "";

        ForceTrainingBtn.interactable = !force.IsMaxLv();
        AgileTrainingBtn.interactable = !agile.IsMaxLv();
        ConstitutionTrainingBtn.interactable = !constitution.IsMaxLv();


        ForceRewardDesc.text = "当前效果\n最高可以将" + CardType.攻击 + "卡升至" + Initializer.Ins.AttrLvInfos[CharacterInDungeon.Ins.Force.currentLv - 1].LvName;
        AgileRewardDesc.text = "当前效果\n最高可以将" + CardType.防御 + "卡升至" + Initializer.Ins.AttrLvInfos[CharacterInDungeon.Ins.Agile.currentLv - 1].LvName;
        ConstitutionRewardDesc.text = "当前效果\n最高可以将" + CardType.技能 + "卡升至" + Initializer.Ins.AttrLvInfos[CharacterInDungeon.Ins.Constitution.currentLv - 1].LvName;


        if (force.IsMaxLv())
        {
            ForceExp.text = "MAX";
            ForceSlider.value = 1;
            ForceTrainingDesc.SetActive(!force.IsMaxLv());
        }
        else
        {
            ForceExp.text = force.currentExp + " / " + force.maxExp;
            ForceSlider.value = (float)force.currentExp / force.maxExp;
            ForceTrainingDesc.SetActive(true);
            ForceTrainingDesc.GetComponent<Text>().text = "提升<color=#FFC000>" + DungeonManager.Ins.ForceExp_Per_Training + "</color>点强化点数";
        }

        if (agile.IsMaxLv())
        {
            AgileExp.text = "MAX";
            AgileSlider.value = 1;
            AgileTrainingDesc.SetActive(!agile.IsMaxLv());
        }
        else
        {
            AgileExp.text = agile.currentExp + " / " + agile.maxExp;
            AgileSlider.value = (float)agile.currentExp / agile.maxExp;
            AgileTrainingDesc.SetActive(true);
            AgileTrainingDesc.GetComponent<Text>().text = "提升<color=#FFC000>" + DungeonManager.Ins.AgileExp_Per_Training + "</color>点强化点数";
        }

        if (constitution.IsMaxLv())
        {
            ConstitutionExp.text = "MAX";
            ConstitutionSlider.value = 1;
            ConstitutionTrainingDesc.SetActive(!constitution.IsMaxLv());
        }
        else
        {
            ConstitutionExp.text = constitution.currentExp + " / " + constitution.maxExp;
            ConstitutionSlider.value = (float)constitution.currentExp / constitution.maxExp;
            ConstitutionTrainingDesc.SetActive(true);
            ConstitutionTrainingDesc.GetComponent<Text>().text = "提升<color=#FFC000>" + DungeonManager.Ins.ConstitutionExp_Per_Training + "</color>点强化点数";
        }

    }

    public void OnRestBtnClick()
    {
        DungeonManager.Ins.Rest();
        HideRestRoomUI();
    }

    public void OnForceTrainingBtnClick()
    {
        CharacterInDungeon.Ins.IncreaseBasicAttributeExp(BasicAttributeType.Force, DungeonManager.Ins.ForceExp_Per_Training);
        HideRestRoomUI();
    }

    public void OnAgileTrainingBtnClick()
    {
        CharacterInDungeon.Ins.IncreaseBasicAttributeExp(BasicAttributeType.Aglie, DungeonManager.Ins.AgileExp_Per_Training);
        HideRestRoomUI();
    }

    public void OnConstitutionTrainingBtnClick()
    {
        CharacterInDungeon.Ins.IncreaseBasicAttributeExp(BasicAttributeType.Constitution, DungeonManager.Ins.ConstitutionExp_Per_Training);
        HideRestRoomUI();
    }

    public void HideRestRoomUI()
    {
        gameObject.SetActive(false);
        DungeonManager.Ins.currentEvent.Finished();
    }
}
