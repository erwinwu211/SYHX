using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class LocalizableData : ScriptableObject, ISerializationCallbackReceiver
{
    [System.Serializable]
    public class Data
    {
        public string id;
        public string Chinese;
        public string Japanese;
    }

    [TableList] public Data[] datas;
    private Dictionary<string, Data> quickDatas;

    public string GetData(string id, Language language)
    {
        Data data;
        if (!quickDatas.TryGetValue(id, out data)) return "";
        switch (language)
        {
            case Language.Chinese:
                return data.Chinese;
            case Language.Japanese:
                return data.Japanese;
        }
        return "";
    }


    public string[] GetIds()
    {
        List<string> ids = new List<string>();
        foreach (var data in datas)
        {
            ids.Add(data.id);
        }
        return ids.ToArray();
    }

    public void OnAfterDeserialize()
    {
        quickDatas = new Dictionary<string, Data>();
        foreach (var data in datas)
        {
            quickDatas.Add(data.id, data);
        }
    }

    public void OnBeforeSerialize()
    {
        quickDatas = new Dictionary<string, Data>();
        foreach (var data in datas)
        {
            quickDatas.Add(data.id, data);
        }
    }
}
