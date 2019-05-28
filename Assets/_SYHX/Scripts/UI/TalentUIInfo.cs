using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TalentUIInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject TalentDescGO;
    public GameObject TalentFrameGO;
    private Talent info;
    private GameObject desc;

    public void SetInfo(Talent info)
    {
        this.info = info;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject go = GameObjectExtension.Create(TalentDescGO, TalentFrameGO);
        desc = go;
        go.transform.Find("desc").GetComponent<TextMeshProUGUI>().text = info.Desc;
        Rect rect = this.GetComponent<RectTransform>().rect;
        go.transform.localPosition = new Vector3(transform.localPosition.x + rect.width / 2 + 10, transform.localPosition.y + rect.height / 2);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (desc)
        {
            Destroy(desc);
        }
    }
}
