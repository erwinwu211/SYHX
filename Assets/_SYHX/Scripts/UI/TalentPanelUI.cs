using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TalentPanelUI : MonoBehaviour
{
    public GameObject TalentGO;
    public GameObject FrameTF;

    public void UpdateTalent(CharacterContent cc)
    {
        foreach (Talent t in cc.Talents)
        {
            GameObject go = GameObjectExtension.Create(TalentGO, FrameTF);
            TextMeshProUGUI name = go.transform.Find("name").GetComponent<TextMeshProUGUI>();
            name.text = t.Name;
            go.GetComponent<TalentUIInfo>().SetInfo(t);
            go.transform.Find("mask").gameObject.SetActive(true);
            if (t.IsEffect)
            {
                go.transform.Find("mask").gameObject.SetActive(false);
            }
        }
    }
}
