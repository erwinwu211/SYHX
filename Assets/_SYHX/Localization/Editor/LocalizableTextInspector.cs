using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(LocalizableText))]
public class LocalizableTextInspector : Editor
{
    SerializedProperty textProperty;
    LocalizableText text;

    string[] groupNames;
    int groupNameIndex;
    string[] ids;
    int idIndex;

    void OnEnable()
    {
        textProperty = serializedObject.FindProperty("text");
        text = target as LocalizableText;

    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.PropertyField(textProperty);
            groupNameIndex = EditorGUILayout.Popup(groupNameIndex, groupNames);
            idIndex = EditorGUILayout.Popup(idIndex, ids);
            // EditorGUILayout.ObjectField()
            EditorGUILayout.EndVertical();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
