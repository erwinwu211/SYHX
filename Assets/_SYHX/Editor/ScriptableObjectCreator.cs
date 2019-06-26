using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ScriptableObjectCreatorWindow : EditorWindow
{
    readonly static string[] labels = { "Data", "ScriptableObject", string.Empty };
    static string sourcePath = "Assets/_SYHX/Scripts/Card/CardSource";
    static string objectPath = "Assets/_SYHX/Scripts/Card/CardScriptableObject";
    static int index = -1;
    public static List<ScriptableObject> objects;
    [MenuItem("自作/SO制造器/卡牌")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ScriptableObjectCreatorWindow));
        DirectoryInfo dir = new DirectoryInfo(sourcePath);
        FileInfo[] files = dir.GetFiles("*.cs");
        objects = new List<ScriptableObject>();
        foreach (var file in files)
        {
            var name = file.Name;
            name = name.Replace(".cs", "");
            UDebug.Log(name);
            ScriptableObject obj = ScriptableObject.CreateInstance(name);
            objects.Add(obj);
        }
    }

    /// <summary>
    /// OnGUI is called for rendering and handling GUI events.
    /// This function can be called multiple times per frame (one call per event).
    /// </summary>
    void OnGUI()
    {
        EditorGUILayout.BeginVertical();

        // var tempList = objects.ToArray();

        foreach (var obj in objects)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.ObjectField(obj, typeof(ScriptableObject), null);
            if (GUILayout.Button("Create"))
            {
                var path = GetSavePath(obj);
                AssetDatabase.CreateAsset(obj, path);
                labels[2] = obj.GetType().Name;
                ScriptableObject sobj = AssetDatabase.LoadAssetAtPath(path, typeof(ScriptableObject)) as ScriptableObject;
                AssetDatabase.SetLabels(sobj, labels);
                EditorUtility.SetDirty(sobj);
                index = objects.IndexOf(obj);
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
        if (index != -1)
        {
            ResetIndex();
        }
    }

    static void ResetIndex()
    {
        var obj = objects[index];
        ScriptableObject obj2 = ScriptableObject.CreateInstance(obj.GetType());
        objects[index] = obj2;
        index = -1;
    }

    static string GetSavePath(Object selectedObject)
    {
        string objectName = selectedObject.GetType().Name;
        string path = $"{objectPath}/{objectName}.asset";

        if (File.Exists(path))
        {
            for (int i = 0; ; i++)
            {
                path = $"{objectPath}/{objectName}({i}).asset";
                if (!File.Exists(path))
                {
                    break;
                }
            }
        }
        return path;
    }

}
