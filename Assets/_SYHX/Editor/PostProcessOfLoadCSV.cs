using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;
using SYHX.Cards;
using System.Collections.Generic;
using System.IO;
using System;


public class PostProcessOfLoadCSV : AssetPostprocessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string str in importedAssets)
        {
            if (str.IndexOf("/character.csv") != -1)
            {
                TextAsset data = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
                string assetfile = str.Replace(".csv", ".asset");
                CharacterText gm = AssetDatabase.LoadAssetAtPath<CharacterText>(assetfile);
                if (gm == null)
                {
                    gm = new CharacterText();
                    AssetDatabase.CreateAsset(gm, assetfile);
                }

                gm.characters = CSVSerializer.Deserialize<CharacterText.Character>(data.text);

                EditorUtility.SetDirty(gm);
                AssetDatabase.SaveAssets();
#if DEBUG_LOG || UNITY_EDITOR
                Debug.Log("Reimported Asset: " + str);
#endif
            }
            if (Regex.IsMatch(str, "L[0-9a-zA-Z]*[.]csv$"))
            {
                TextAsset data = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
                string assetfile = str.Replace(".csv", ".asset");
                LocalizableData gm = AssetDatabase.LoadAssetAtPath<LocalizableData>(assetfile);
                if (gm == null)
                {
                    gm = new LocalizableData();
                    AssetDatabase.CreateAsset(gm, assetfile);
                }

                gm.datas = CSVSerializer.Deserialize<LocalizableData.Data>(data.text);

                EditorUtility.SetDirty(gm);
                AssetDatabase.SaveAssets();
#if DEBUG_LOG || UNITY_EDITOR
                Debug.Log("Reimported Asset: " + str);
#endif
            }

            if (str.IndexOf("/CardData.csv") != -1)
            {
                TextAsset data = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
                string assetfile = str.Replace(".csv", ".asset");
                CardData gm = AssetDatabase.LoadAssetAtPath<CardData>(assetfile);
                if (gm == null)
                {
                    gm = new CardData();
                    AssetDatabase.CreateAsset(gm, assetfile);
                }

                gm.Cards = CSVSerializer.Deserialize<CardData.Data>(data.text);
                var tempList = new List<CardSource>();

                foreach (var card in gm.Cards)
                {
                    var cardSource = CheckAndGetCard(card);
                    ApplyData(card, cardSource);
                    tempList.Add(cardSource);
                }

                foreach (var card in gm.Cards)
                {
                    ApplyUpgradeList(card, tempList[0]);
                    tempList.RemoveAt(0);
                }

                EditorUtility.SetDirty(gm);
                AssetDatabase.SaveAssets();
#if DEBUG_LOG || UNITY_EDITOR
                Debug.Log("Reimported Asset: " + str);
#endif
            }

        }
    }
    static string cardPath = "Assets/_SYHX/Scripts/Card/CardScriptableObject";
    static string initializerPath = "Assets/_SYHX/Prefabs/Initializer.prefab";

    static Dictionary<int, CardSource> cardDictionary = new Dictionary<int, CardSource>();
    static bool isInited = false;
    readonly static string[] labels = { "Data", "ScriptableObject", string.Empty };
    static CardSource CheckAndGetCard(CardData.Data data)
    {
        if (!isInited)
        {
            var match = "[1-9][0-9]{0,4}";
            DirectoryInfo dir = new DirectoryInfo(cardPath);
            FileInfo[] files = dir.GetFiles("*.asset");

            foreach (var file in files)
            {
                var result = Regex.Match(file.Name, match).Value;
                if (result != "")
                {
                    var card = AssetDatabase.LoadAssetAtPath(cardPath + "/" + file.Name, typeof(CardSource)) as CardSource;
                    var id = card.ID;
                    cardDictionary.Add(id, card);
                }
            }
            isInited = true;
        }
        ScriptableObject obj = ScriptableObject.CreateInstance(data.source);
        if (cardDictionary.ContainsKey(data.ID))
        {
            var tempCard = cardDictionary[data.ID];
            if (cardDictionary[data.ID].GetType() == obj.GetType())
                return tempCard;
            var cardPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(tempCard));
            AssetDatabase.DeleteAsset(cardPath + "/" + tempCard.name + ".asset");
            cardDictionary.Remove(data.ID);
        }
        var path = cardPath + "/c" + string.Format("{0:00000}", data.ID) + "_" + data.名称 + ".asset";
        labels[2] = data.source;
        AssetDatabase.CreateAsset(obj, path);
        CardSource sobj = AssetDatabase.LoadAssetAtPath(path, typeof(CardSource)) as CardSource;
        AssetDatabase.SetLabels(sobj, labels);
        EditorUtility.SetDirty(sobj);
        cardDictionary.Add(data.ID, sobj);
        return sobj;
    }

    static void ApplyData(CardData.Data data, CardSource source)
    {
        source.ApplyData(data);
        EditorUtility.SetDirty(source);
    }

    static void ApplyUpgradeList(CardData.Data data, CardSource source)
    {
        var tempList = new List<CardSource>();
        foreach (var index in data.升级选项列表)
        {
            tempList.Add(cardDictionary[index]);
        }
        source.ApplyUpgradeList(tempList);
        EditorUtility.SetDirty(source);
    }
}
