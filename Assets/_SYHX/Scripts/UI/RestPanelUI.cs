using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SYHX.Cards;

public class RestPanelUI : MonoBehaviour
{
    public Button RestBtn;
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
        gameObject.SetActive(true);
        ForceLevelCount.text = "力量强化Lv."+force.currentLv + "";
        AgileLevelCount.text = "敏捷强化Lv."+agile.currentLv + "";
        ConstitutionLevelCount.text = "智力强化Lv."+constitution.currentLv + "";

        ForceTrainingBtn.interactable = !force.IsMaxLv();
        AgileTrainingBtn.interactable = !agile.IsMaxLv();
        ConstitutionTrainingBtn.interactable = !constitution.IsMaxLv();

        ForceTrainingDesc.SetActive(!force.IsMaxLv());
        AgileTrainingDesc.SetActive(!agile.IsMaxLv());
        ConstitutionTrainingDesc.SetActive(!constitution.IsMaxLv());

        ForceRewardDesc.text = "当前效果\n最高可以将"+CardType.攻击+"卡升至"+Initializer.Ins.AttrLvInfos[CharacterInDungeon.Ins.Force.currentLv-1].LvName;
        AgileRewardDesc.text = "当前效果\n最高可以将"+CardType.防御+"卡升至"+Initializer.Ins.AttrLvInfos[CharacterInDungeon.Ins.Agile.currentLv-1].LvName;
        ConstitutionRewardDesc.text = "当前效果\n最高可以将"+CardType.技能+"卡升至"+Initializer.Ins.AttrLvInfos[CharacterInDungeon.Ins.Constitution.currentLv-1].LvName;

        if (force.IsMaxLv())
        {
            ForceExp.text = "MAX";
            ForceSlider.value = 1;
        }
        else
        {
            ForceExp.text = force.currentExp + " / " + force.maxExp;
            ForceSlider.value = (float)force.currentExp / force.maxExp;
        }

        if (agile.IsMaxLv())
        {
            AgileExp.text = "MAX";
            AgileSlider.value = 1;
        }
        else
        {
            AgileExp.text = agile.currentExp + " / " + agile.maxExp;
            AgileSlider.value = (float)agile.currentExp / agile.maxExp;
        }

        if (constitution.IsMaxLv())
        {
            ConstitutionExp.text = "MAX";
            ConstitutionSlider.value = 1;
        }
        else
        {
            ConstitutionExp.text = constitution.currentExp + " / " + constitution.maxExp;
            ConstitutionSlider.value = (float)constitution.currentExp / constitution.maxExp;
        }

    }

    public void OnRestBtnClick()
    {
        DungeonManager.Ins.Rest();
        HideRestRoomUI();
    }

    public void OnForceTrainingBtnClick()
    {
        CharacterInDungeon.Ins.IncreaseBasicAttributeExp(BasicAttributeType.Force, 10);
        HideRestRoomUI();
    }

    public void OnAgileTrainingBtnClick()
    {
        CharacterInDungeon.Ins.IncreaseBasicAttributeExp(BasicAttributeType.Aglie, 10);
        HideRestRoomUI();
    }

    public void OnConstitutionTrainingBtnClick()
    {
        CharacterInDungeon.Ins.IncreaseBasicAttributeExp(BasicAttributeType.Constitution, 10);
        HideRestRoomUI();
    }

    public void HideRestRoomUI()
    {
        gameObject.SetActive(false);
        DungeonManager.Ins.currentEvent.Finished();
    }
}
