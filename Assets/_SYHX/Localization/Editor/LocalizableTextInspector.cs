using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(LocalizableText))]
public class LocalizableTextInspector : Editor
{
    static string sourcePath = "Assets/_SYHX/Prefabs/Initializer.prefab";
    SerializedProperty textProperty;
    SerializedProperty groupName;
    SerializedProperty id;
    LocalizationManager manager;
    string[] groupNames;
    int groupNameIndex;
    string[] ids;
    int idIndex;

    void OnEnable()
    {
        textProperty = serializedObject.FindProperty("text");
        groupName = serializedObject.FindProperty("groupName");
        id = serializedObject.FindProperty("id");
        serializedObject.Update();
        manager = (LocalizationManager)AssetDatabase.LoadAssetAtPath(sourcePath, typeof(LocalizationManager));
        groupNames = manager.GetGroupNames();
        if (groupName.stringValue == null || groupName.stringValue == "")
        {
            groupName.stringValue = groupNames[0];
        }
        groupNameIndex = FindIndex(groupNames, groupName.stringValue);
        ids = manager.GetIds(groupName.stringValue);
        if (id.stringValue == null || id.stringValue == "")
        {
            id.stringValue = ids[0];
        }
        idIndex = FindIndex(ids, id.stringValue);
        serializedObject.ApplyModifiedProperties();

    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.PropertyField(textProperty);
            var prevIndex = groupNameIndex;
            groupNameIndex = EditorGUILayout.Popup(groupNameIndex, groupNames);
            if (groupNameIndex != prevIndex)
            {
                groupName.stringValue = groupNames[groupNameIndex];
                ids = manager.GetIds(groupName.stringValue);
                idIndex = 0;

            }
            prevIndex = idIndex;
            idIndex = EditorGUILayout.Popup(idIndex, ids);
            if (idIndex != prevIndex)
            {
                id.stringValue = ids[idIndex];
                if (serializedObject.hasModifiedProperties) Debug.Log("right");
            }
            EditorGUILayout.LabelField(manager.GetData(groupNames[groupNameIndex], ids[idIndex]));
            EditorGUILayout.EndVertical();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }
        }

        serializedObject.ApplyModifiedProperties();

    }

    int FindIndex(string[] strings, string data)
    {
        var id = 0;
        foreach (var item in strings)
        {
            if (item == data) break;
            id++;
        }
        return id;
    }
}
