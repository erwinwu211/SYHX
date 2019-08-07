using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SYHX.Cards;

public class CharacterUI : SingletonMonoBehaviour<CharacterUI>
{
    public GameObject Info;
    public GameObject StrSlider;
    public GameObject AgiSlider;
    public GameObject IntSlider;
    public GameObject ForSlider;
    public GameObject ATK;
    public GameObject DEF;
    public GameObject HP;
    public GameObject EP;
    public GameObject DRAW;
    public GameObject SkillList;
    public GameObject SkillGO;
    public GameObject Lv;
    public GameObject ExpSlider;
    public GameObject ExpCount;
    public GameObject SkillPanel;
    public GameObject DeckInformationUI;
    public float FillSpeed = 0.18f;

    public CharacterContent selectedCharacter { get; private set; }
    private int mode = 1; //当前此界面的模式：1-人物信息 2-天赋 3-介绍

    public void Start()
    {
        //TODO
        //要动态读取人物列表并动态挂载按钮事件
        OnToggleChanged(1);
    }

    /// <summary>
    /// 按返回按钮回到主界面
    /// </summary>
    public void OnReturnBtnClick()
    {
        SceneStatusManager.Ins.SetSceneStatus(new MainStatus(SceneStatusManager.Ins));
    }

    /// <summary>
    /// 当切换选择的人物时触发的事件
    /// </summary>
    /// <param name="i"></param>
    public void OnToggleChanged(int i)
    {
        switch (i)
        {
            case 1:
                if (PlayerRecord.Ins.Umirika.isLock) return;
                selectedCharacter = PlayerRecord.Ins.Umirika;
                RefreshCharacterInfo(selectedCharacter);
                return;
            case 2:

                return;
            case 3:
                return;
            default:
                return;
        }
    }

    /// <summary>
    /// 更新人物信息
    /// </summary>
    /// <param name="cc"></param>
    public void RefreshCharacterInfo(CharacterContent cc)
    {
        switch (mode)
        {
            //更新人物属性信息
            case 1:
                Lv.GetComponent<Text>().text = cc.currentGrade + "";
                //ExpCount.GetComponent<Text>().text = cc.Exp + "/" + cc.lvInfos[cc.Lv - 1].RequireCount;
                //ExpSlider.GetComponent<Slider>().value = (float)cc.Exp / cc.lvInfos[cc.Lv - 1].RequireCount;

                StrSlider.transform.Find("LvSlider/Slider").GetComponent<Slider>().value = cc.STR / 10;
                StrSlider.transform.Find("Lv/Count").GetComponent<Text>().text = cc.STR + "";

                AgiSlider.transform.Find("LvSlider/Slider").GetComponent<Slider>().value = cc.AGI / 10;
                AgiSlider.transform.Find("Lv/Count").GetComponent<Text>().text = cc.AGI + "";

                IntSlider.transform.Find("LvSlider/Slider").GetComponent<Slider>().value = cc.INT / 10;
                IntSlider.transform.Find("Lv/Count").GetComponent<Text>().text = cc.INT + "";

                ForSlider.transform.Find("LvSlider/Slider").GetComponent<Slider>().value = cc.FOR / 10;
                ForSlider.transform.Find("Lv/Count").GetComponent<Text>().text = cc.FOR + "";

                ATK.transform.Find("Count").GetComponent<Text>().text = cc.Attack + "";
                DEF.transform.Find("Count").GetComponent<Text>().text = cc.Defend + "";
                HP.transform.Find("Count").GetComponent<Text>().text = cc.Hp_max + "";
                EP.transform.Find("Count").GetComponent<Text>().text = cc.Energy_max + "";
                DRAW.transform.Find("Count").GetComponent<Text>().text = cc.Draw_count + "";

                foreach (Transform tf in SkillList.transform)
                {
                    Destroy(tf.gameObject);
                }
                foreach (CharacterSkill skill in cc.skills)
                {
                    GameObject go = Instantiate(SkillGO, SkillList.transform);
                    go.SetActive(true);
                    go.transform.Find("Name").GetComponent<Text>().text = skill.name;
                    go.GetComponent<Button>().onClick.AddListener(delegate
                    {
                        ShowSkillInfoPanel(skill);
                    });
                }
                return;

            //更新人物天赋信息
            case 2:

                return;

            //更新人物介绍信息
            case 3:

                return;
        }
    }


    public void ShowSkillInfoPanel(CharacterSkill skill)
    {
        Info.SetActive(false);
        SkillPanel.SetActive(true);
        Transform parent = SkillPanel.transform.Find("parent");
        parent.Find("skill/Name").GetComponent<Text>().text = skill.name;
        parent.Find("Desc").GetComponent<Text>().text = skill.desc;
        StopCoroutine("UnFillPanel");
        StartCoroutine("FillPanel");
    }

    public void EnhanceSkillInfoPanel()
    {
        StopCoroutine("FillPanel");
        StartCoroutine("UnFillPanel");
    }

    public void OnDeckBtnClick()
    {
        GameObject go = Instantiate(DeckInformationUI,transform.parent);
        List<CardContent> ccList = new List<CardContent>();
        foreach (CardSource cs in selectedCharacter.Deck)
        {
            CardContent cc = cs.GenerateCard();
            ccList.Add(cc);
        }
        go.GetComponent<DeckInformationUI>().LoadDeckInfomation(ccList);
    }

    IEnumerator FillPanel()
    {
        Image image = SkillPanel.GetComponent<Image>();
        image.fillAmount = 0.2f;
        for (; image.fillAmount < 1;)
        {
            image.fillAmount += FillSpeed;
            yield return new WaitForSeconds(0.02f);
        }
        SkillPanel.transform.Find("parent").gameObject.SetActive(true);
    }

    IEnumerator UnFillPanel()
    {
        SkillPanel.transform.Find("parent").gameObject.SetActive(false);
        Image image = SkillPanel.GetComponent<Image>();
        for (; image.fillAmount > 0.2f;)
        {
            image.fillAmount -= FillSpeed;
            yield return new WaitForSeconds(0.02f);
        }
        SkillPanel.SetActive(false);
        Info.SetActive(true);
    }

    public void OnTalentBtnClick()
    {

    }

    public void OnEquipmentBtnClick()
    {

    }

    protected override void UnityAwake()
    {
    }
}
