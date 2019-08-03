using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizableText : LocalizableObject
{
    [SerializeField] public Text text;

    [SerializeField] public string groupName;
    [SerializeField] public string id;
    // Start is called before the first frame update
    void Start()
    {
        LocalizationManager.Ins.Objects.Add(this);
        // text.text = x
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        LocalizationManager.Ins.Objects.Remove(this);
    }

    public override void RefreshUI(Language currentLanguage)
    {

    }
}
