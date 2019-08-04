using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

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
            return LocalizableData[groupName].GetData(id, currentLanguage);
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

    public string[] GetIds(string groupName)
    {
        if (groupName != null && LocalizableData.ContainsKey(groupName))
        {
            return LocalizableData[groupName].GetIds();
        }
        return new string[] { "null" };
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
        LocalizableData = new Dictionary<string, LocalizableData>();
        foreach (var data in localizableData)
        {
            LocalizableData.Add(data.groupName, data.data);
        }
    }

    public void ChangeLanguage(Language language)
    {
        this.currentLanguage = language;
        foreach (var data in objects)
        {
            data.RefreshUI();
        }
    }

#if UNITY_EDITOR
    static string sourcePath = "Assets/_SYHX/Prefabs/Initializer.prefab";
    static LocalizationManager manager;
    public static string EditorGetData(string groupName, string id)
    {
        if (manager == null)
        {
            manager = (LocalizationManager)AssetDatabase.LoadAssetAtPath(sourcePath, typeof(LocalizationManager));
        }
        return manager.GetData(groupName, id);
    }
#endif
}
public enum Language
{
    Chinese, Japanese
}
