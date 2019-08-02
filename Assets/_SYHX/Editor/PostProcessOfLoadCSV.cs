using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

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

        }
    }
}
