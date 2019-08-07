using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventUI : MonoBehaviour
{
    public Text title;
    public Text desc;
    public Image pic;
    public Transform optionParent;
    public GameObject option;

    public void ShowEventUI(EventSource e)
    {
        this.gameObject.SetActive(true);
        this.title.text = e.title;
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
}