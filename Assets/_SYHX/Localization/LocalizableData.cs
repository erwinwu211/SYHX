using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class LocalizableData : ScriptableObject
{
    [System.Serializable]
    public class Data
    {
        public string id;
        public string Chinese;
        public string Japanese;
    }

    [TableList] public Data[] datas;
    private Dictionary<string, Data> quickDatas = new Dictionary<string, Data>();

    public string GetData(string id)
    {
        Data data;
        if (!quickDatas.TryGetValue(id, out data)) return "";
        switch (Initializer.Ins.CurrentLanguage)
        {
            case Initializer.Language.Chinese:
                return data.Chinese;
            case Initializer.Language.Japanese:
                return data.Japanese;
        }
        return "";
    }

    public void Init()
    {
        foreach (var data in datas)
        {
            quickDatas.Add(data.id, data);
        }
    }
}
