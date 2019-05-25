using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterUI : MonoBehaviour
{
    public GameObject TalentPanel;
    public GameObject EquipmentPanel;

    private GameObject TalentBtnTip;
    private CharacterContent selectedCharacter;
    private Transform Info;
    private TextMeshProUGUI NameText;
    private TextMeshProUGUI AttackCount;
    private TextMeshProUGUI DefendCount;
    private TextMeshProUGUI PowerCount;
    private TextMeshProUGUI DrawCount;
    private TextMeshProUGUI DeckCount;
    public GameObject Mask;

    private CharacterContent FukasakiKotone;

    void Start()
    {
        //读取界面
        TalentBtnTip = GameObject.Find("TalentBtn/Tip");
        Info = GameObject.Find("Info").transform;
        NameText = GameObject.Find("Name").GetComponent<TextMeshProUGUI>();
        AttackCount = Info.Find("Attack/Count").GetComponent<TextMeshProUGUI>();
        DefendCount = Info.Find("Defend/Count").GetComponent<TextMeshProUGUI>();
        PowerCount = Info.Find("Power/Count").GetComponent<TextMeshProUGUI>();
        DrawCount = Info.Find("DrawCount/Count").GetComponent<TextMeshProUGUI>();
        DeckCount = Info.Find("DeckCount/Count").GetComponent<TextMeshProUGUI>();

        //创建好人物信息
        FukasakiKotone = new FukasakiKotone().GenerateCharacter();

        //进行初始化设定
        selectedCharacter = FukasakiKotone;
        TalentBtnTip.SetActive(false);
        RefreshSelectedCharacterInfo();
    }

    /// <summary>
    /// 根据选中的人物刷新显示数据
    /// </summary>
    private void RefreshSelectedCharacterInfo()
    {
        NameText.text = selectedCharacter.Name;
        AttackCount.text = selectedCharacter.Attack+"";
        DefendCount.text = selectedCharacter.Defend + "";
        PowerCount.text = selectedCharacter.Energy + "";
        DrawCount.text = selectedCharacter.DrawCount + "";
        DeckCount.text = selectedCharacter.Cards.Count + "";
    }

    /// <summary>
    /// 按返回按钮回到主界面
    /// </summary>
    public void OnReturnBtnClick()
    {
        SceneStateManager.Ins.SetSceneStatus(new MainState(SceneStateManager.Ins));
    }

    public void OnToggleChanged(int i)
    {
        switch (i)
        {
            case 1:
                selectedCharacter = FukasakiKotone;
                return;
            case 2:
                return;
            case 3:
                return;
            default:
                return;
        }
    }

    public void OnTalentBtnClick()
    {
        GameObject go = GameObject.Instantiate(TalentPanel, transform.parent);
        go.transform.localPosition = Vector3.zero;
        Mask.SetActive(true);
        go.transform.Find("CloseBtn").GetComponent<Button>().onClick.AddListener(delegate ()
        {
            Mask.SetActive(false);
            Destroy(go);
        });
    }

    public void OnEquipmentBtnClick()
    {
        GameObject go = GameObject.Instantiate(EquipmentPanel, transform.parent);
        go.transform.localPosition = Vector3.zero;
        Mask.SetActive(true);
        go.transform.Find("CloseBtn").GetComponent<Button>().onClick.AddListener(delegate ()
        {
            Mask.SetActive(false);
            Destroy(go);
        });
    }
}
