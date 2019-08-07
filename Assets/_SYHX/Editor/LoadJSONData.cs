using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SYHX.Cards;

public class LoadJSONData : MonoBehaviour
{
    [MenuItem("自作/读取/读取当前JSON")]
    public static void LoadJSON()
    {
        var card = Selection.activeObject as CardSource;
        if (card != null)
        {
            Debug.Log(card.ToJSON());
        }
    }

    // [MenuItem("自作/读取/存储当前JSON")]
    public static void SaveJSON()
    {
        var card = Selection.activeObject as CardSource;
        if (card != null)
        {
            Debug.Log(card.ToJSON());
            card.FromJSON("{\"defenceRate\":1.2}");
        }
    }
}
