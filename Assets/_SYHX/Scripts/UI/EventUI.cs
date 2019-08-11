using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventUI : MonoBehaviour
{
    public GameObject eventPanel;
    public Text title;
    public Text desc;
    public Image pic;
    public Transform optionParent;
    public GameObject option;


    public GameObject resultPanel;
    public Text resultTitle;
    public Text resultDesc;


    public void ShowEventUI(EventSource e)
    {
        this.gameObject.SetActive(true);
        this.eventPanel.SetActive(true);
        this.resultPanel.SetActive(false);
        this.title.text = e.title;
        this.resultTitle.text = e.title;
        this.desc.text = e.desc;
        this.pic.sprite = e.pic;
        foreach (Transform tf in optionParent) Destroy(tf.gameObject);
        foreach (OptionSource op in e.optionSources)
        {
            GameObject go = Instantiate(option, optionParent);
            go.SetActive(true);
            go.GetComponentInChildren<Text>().text = op.desc;
            go.GetComponent<Button>().onClick.AddListener(delegate
            {
                op.Effect();
            });
        }
    }

    public void EnhanceEventUI()
    {
        this.gameObject.SetActive(false);
    }

    public void ShowResultPanel(string desc)
    {
        eventPanel.SetActive(false);
        resultPanel.SetActive(true);
        resultDesc.text = desc;
    }

    public void OnCloseBtnClick()
    {
        EventManager.Ins.ReleaseEvent();
    }
}