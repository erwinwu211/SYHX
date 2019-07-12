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
    public float FillSpeed = 0.18f;

    public void OnPointerClick(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TextPic.GetComponent<Image>().color = HighLight;
        BgPic.SetActive(true);
        BgPic.GetComponent<Image>().fillAmount = 0;
        StartCoroutine(FillBg());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TextPic.GetComponent<Image>().color = Normal;
        BgPic.SetActive(false);
    }

    IEnumerator FillBg()
    {
        Image image = BgPic.GetComponent<Image>();
        for(; image.fillAmount<1;)
        {
            image.fillAmount += FillSpeed;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
