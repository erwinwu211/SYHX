using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterUI : SingletonMonoBehaviour<CharacterUI>
{
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

    public CharacterContent selectedCharacter { get; private set; }
    private int mode = 1; //当前此界面的模式：1-人物信息 2-天赋 3-介绍

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
        switch(mode)
        {
            //更新人物属性信息
            case 1:
                Lv.GetComponent<Text>().text = cc.Lv + "";
                ExpCount.GetComponent<Text>().text = cc.Exp + "/" + cc.lvInfos[cc.Lv-1].Exp;
                ExpSlider.GetComponent<Slider>().value = (float)cc.Exp / cc.lvInfos[cc.Lv-1].Exp;

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
                return;

            //更新人物天赋信息
            case 2:

                return;
            
            //更新人物介绍信息
            case 3:

                return;
        }
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
