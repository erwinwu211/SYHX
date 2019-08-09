using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestPanelUI : MonoBehaviour
{
    public Button RestBtn;
    public Button ForceTrainingBtn;
    public Text ForceLevelCount;
    public Text ForceExp;
    public GameObject ForceDesc;
    public Slider ForceSlider;

    public Button AgileTrainingBtn;
    public Text AgileLevelCount;
    public Text AgileExp;
    public GameObject AgileDesc;
    public Slider AgileSlider;

    public Button ConstitutionTrainingBtn;
    public Text ConstitutionLevelCount;
    public Text ConstitutionExp;
    public GameObject ConstitutionDesc;
    public Slider ConstitutionSlider;
    public void ShowRestRoomUI(BasicAttribute force, BasicAttribute agile, BasicAttribute constitution)
    {
        gameObject.SetActive(true);
        ForceLevelCount.text = force.currentLv + "";
        AgileLevelCount.text = agile.currentLv + "";
        ConstitutionLevelCount.text = constitution.currentLv + "";

        ForceTrainingBtn.interactable = !force.IsMaxLv();
        AgileTrainingBtn.interactable = !agile.IsMaxLv();
        ConstitutionTrainingBtn.interactable = !constitution.IsMaxLv();

        ForceDesc.SetActive(!force.IsMaxLv());
        AgileDesc.SetActive(!agile.IsMaxLv());
        ConstitutionDesc.SetActive(!constitution.IsMaxLv());

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
