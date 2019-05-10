using UnityEngine;
using UnityEditor;
using System.IO;

public class ScriptableObjectToAsset
{
    readonly static string[] labels = { "Data", "ScriptableObject", string.Empty };
    [MenuItem("Assets/Create/ScriptableObject")]
    static void Create()
    {
        foreach (Object selectedObject in Selection.objects)
        {
            var path = GetSavePath(selectedObject);

            ScriptableObject obj = ScriptableObject.CreateInstance(selectedObject.name);
            AssetDatabase.CreateAsset(obj, path);
            labels[2] = selectedObject.name;

            ScriptableObject sobj = AssetDatabase.LoadAssetAtPath(path, typeof(ScriptableObject)) as ScriptableObject;
            AssetDatabase.SetLabels(sobj, labels);
            EditorUtility.SetDirty(sobj);
        }
    }

    static string GetSavePath(Object selectedObject)
    {
        string objectName = selectedObject.name;
        string dirPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(selectedObject));
        string path = $"{dirPath}/{objectName}.asset";

        if (File.Exists(path))
        {
            for (int i = 0; ; i++)
            {
                path = $"{dirPath}/{objectName}({i}).asset";
                if (!File.Exists(path))
                {
                    break;
                }
            }
        }
        return path;
    }

}
