using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TalentPanelUI : MonoBehaviour
{
    public Transform talentGroupParent;
    public GameObject talentGO;
    public Transform talentOptionParent;
    public GameObject talentOptionGO;
    public GameObject mask;

    public Color DisableColor;
    public Color DefaultColor;
    public Color DefaultColor_Title;

    public void Refresh(List<TalentGroup> Tgroup)
    {
        foreach (Transform tf in talentGroupParent) Destroy(tf.gameObject);
        foreach (TalentGroup item in Tgroup)
        {
            Transform tf = Instantiate(talentGO, talentGroupParent).transform;
            tf.gameObject.SetActive(true);
            GameObject selectedFrame = tf.Find("SelectedFrame").gameObject;
            GameObject icon = tf.Find("Bg/Icon").gameObject;
            GameObject Lock = tf.Find("Lock").gameObject;
            GameObject needLvText = Lock.transform.Find("Text").gameObject;
            if (CharacterInDungeon.Ins.currentLv >= item.needLv)
            {
                selectedFrame.SetActive(true);
                Lock.SetActive(false);
                if (item.ActiveTalent != null)
                {
                    selectedFrame.SetActive(false);
                    icon.GetComponent<Image>().sprite = item.ActiveTalent.Icon;
                }
            }
            else
            {
                selectedFrame.SetActive(false);
                Lock.SetActive(true);
                needLvText.GetComponent<Text>().text = item.needLv + "级解锁";
            }
            tf.gameObject.GetComponent<Button>().onClick.AddListener(delegate
            {
                ShowTalentsInGroup(item);
            });
        }
    }

    public void ShowTalentsInGroup(TalentGroup TGroup)
    {
        talentOptionParent.gameObject.SetActive(true);
        mask.SetActive(true);
        foreach (Transform tf in talentOptionParent) Destroy(tf.gameObject);
        foreach (Talent item in TGroup.talents)
        {
            Transform tf = Instantiate(talentOptionGO, talentOptionParent).transform;
            tf.gameObject.SetActive(true);
            GameObject selectedFrame = tf.Find("SelectedFrame").gameObject;
            Text Title = tf.Find("Name").gameObject.GetComponent<Text>();
            Text Desc = tf.Find("Desc").gameObject.GetComponent<Text>();
            Image Icon = tf.Find("Icon").gameObject.GetComponent<Image>();
            GameObject Unlock = tf.Find("Icon/lock").gameObject;

            selectedFrame.SetActive(false);
            Unlock.SetActive(false);
            Title.text = item.Name;
            Desc.text = item.Desc;
            Icon.sprite = item.Icon;
            //等级还不够 全体变暗,显示上锁
            if (CharacterInDungeon.Ins.currentLv < TGroup.needLv)
            {
                Unlock.SetActive(true);
                Title.color = DisableColor;
                Desc.color = DisableColor;
                Icon.color = DisableColor;
            }
            else
            {
                //可以选天赋但还没选 所有可选的都高亮
                if (TGroup.ActiveTalent == null)
                {
                    if (item.IsUnlock)
                    {
                        Unlock.SetActive(false);
                        Title.color = DefaultColor_Title;
                        Desc.color = DefaultColor;
                        Icon.color = Color.white;
                        tf.gameObject.GetComponent<Button>().interactable = true;
                        tf.gameObject.GetComponent<Button>().onClick.AddListener(delegate{
                            OnTalentOptionClick(item);
                        });
                    }
                    else
                    {
                        Unlock.SetActive(true);
                        Title.color = DisableColor;
                        Desc.color = DisableColor;
                        Icon.color = DisableColor;
                    }
                }
                else
                //已经有选过的天赋了 被选的高亮，其它的变暗
                {
                    if (TGroup.ActiveTalent == item)
                    {
                        selectedFrame.SetActive(true);
                        Title.color = DefaultColor_Title;
                        Desc.color = DefaultColor;
                        Icon.color = Color.white;
                    }
                    else
                    {
                        Unlock.SetActive(true);
                        Title.color = DisableColor;
                        Desc.color = DisableColor;
                        Icon.color = DisableColor;
                    }
                }
            }
        }
    }

    public void HideTalentsInGroup()
    {
        mask.SetActive(false);
        talentOptionParent.gameObject.SetActive(false);
    }

    public void OnTalentOptionClick(Talent t)
    {
        CharacterInDungeon.Ins.ActiveTalent(t);
        HideTalentsInGroup();
    }
}
