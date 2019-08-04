using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LocalizableObject))]
public class LocalizableText : MonoBehaviour
{
    [SerializeField] public Text text;
    [SerializeField] public LocalizableObject Object;
    void Start()
    {
        Object.RegistRefesh(RefreshUI);
        text.text = Object.GetData();
    }

    void RefreshUI()
    {
        text.text = Object.GetData();
    }

}

