using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEffect : MonoBehaviour,
    IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    public GameObject TextPic;
    public GameObject BgPic;
    public Color Normal;
    public Color HighLight;

    public void OnPointerClick(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TextPic.GetComponent<Image>().color = HighLight;
        BgPic.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TextPic.GetComponent<Image>().color = Normal;
        BgPic.SetActive(false);
    }
}
