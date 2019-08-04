using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LocalizableObject : MonoBehaviour
{
    [SerializeField] public string groupName;
    [SerializeField] public string id;

    private Action refreshCallback = delegate { };

    void Start()
    {
        LocalizationManager.Ins.Objects.Add(this);
    }

    void OnDestroy()
    {
        LocalizationManager.Ins.Objects.Remove(this);
    }

    public void RefreshUI()
    {
        refreshCallback();
    }

    public void RegistRefesh(Action callback)
    {
        refreshCallback = callback;
    }

    public string GetData()
    {
        return LocalizationManager.Ins.GetData(groupName, id);
    }
}
