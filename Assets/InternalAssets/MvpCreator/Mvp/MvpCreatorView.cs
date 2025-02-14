using System;
using UnityEditor;
using UnityEngine;

public class MvpCreatorView
{
    public event Action OnCreate;

    public void DrawUI(MvpCreatorModel model)
    {
        GUILayout.Label("MVP Template Creator", EditorStyles.boldLabel);

        model.ModuleName = EditorGUILayout.TextField("Name", model.ModuleName);
        model.Namespace = EditorGUILayout.TextField("Namespace", model.Namespace);
        model.FolderPath = EditorGUILayout.TextField("Path", model.FolderPath);

        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Create"))
        {
            OnCreate?.Invoke();
        }
    }
}