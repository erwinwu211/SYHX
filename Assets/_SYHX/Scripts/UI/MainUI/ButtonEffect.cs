using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEffect : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject TextPic;
    public GameObject BgPic;
    public Color Normal;
    public Color HighLight;
    private float FillSpeed = 9f;

    public void OnPointerClick(PointerEventData eventData)
    {
    }

    /// <summary>
    /// 鼠标移入时，显示并展开背景
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        TextPic.GetComponent<Image>().color = HighLight;
        BgPic.SetActive(true);
        BgPic.GetComponent<Image>().fillAmount = 0;
        StopCoroutine("UnFillBg");
        StartCoroutine("FillBg");
    }

    /// <summary>
    /// 鼠标移出时，收起背景然后隐藏
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        TextPic.GetComponent<Image>().color = Normal;
        StopCoroutine("FillBg");
        StartCoroutine("UnFillBg");
    }

    /// <summary>
    /// 填充背景
    /// </summary>
    /// <returns></returns>
    IEnumerator FillBg()
    {
        Image image = BgPic.GetComponent<Image>();
        for (; image.fillAmount < 1;)
        {
            image.fillAmount += FillSpeed * Time.deltaTime;
            yield return null;
        }
    }

    /// <summary>
    /// 反填充背景
    /// </summary>
    /// <returns></returns>
    IEnumerator UnFillBg()
    {
        Image image = BgPic.GetComponent<Image>();
        for (; image.fillAmount > 0;)
        {
            image.fillAmount -= FillSpeed * Time.deltaTime;
            yield return null;
        }
        BgPic.SetActive(false);
    }

    /// <summary>
    ///  重置填充背景
    /// </summary>
    public void ResetFillBg()
    {
        Image image = BgPic.GetComponent<Image>();
        image.fillAmount = 0;
        TextPic.GetComponent<Image>().color = Normal;
    }
}
