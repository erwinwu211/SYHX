using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : SingletonMonoBehaviour<LocalizationManager>, ISerializationCallbackReceiver
{
    private List<LocalizableObject> objects = new List<LocalizableObject>();
    public List<LocalizableObject> Objects => objects;
    protected override void UnityAwake()
    {

    }
    public Language currentLanguage;

    [System.Serializable]
    public class LocalizableDataGroup
    {
        public string groupName;
        public LocalizableData data;
    }

    [SerializeField] public List<LocalizableDataGroup> localizableData;
    private Dictionary<string, LocalizableData> LocalizableData;

    public string GetData(string groupName, string id)
    {
        if (LocalizableData.ContainsKey(groupName))
        {
            return LocalizableData[groupName].GetData(id);
        }
        return "";
    }

    public string[] GetGroupNames()
    {
        List<string> names = new List<string>();
        foreach (var data in localizableData)
        {
            names.Add(data.groupName);
        }
        return names.ToArray();
    }

    public void OnAfterDeserialize()
    {
        LocalizableData = new Dictionary<string, LocalizableData>();
        foreach (var data in localizableData)
        {
            LocalizableData.Add(data.groupName, data.data);
        }
    }

    public void OnBeforeSerialize()
    {
    }
}
public enum Language
{
    Chinese, Japanese
}
