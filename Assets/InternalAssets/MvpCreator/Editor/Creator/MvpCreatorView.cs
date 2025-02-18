using System;
using UnityEditor;
using UnityEngine;

namespace MvpCreator
{
    public class MvpCreatorView
    {
        private int _index = 0;
        private Vector2 _scrollPosition;
        private string[] _modules = new string[] { "Manager", "Model", "View", "Presenter" };
        private GUIStyle _richTextStyle;
        private MvpCreatorModel _model;

        public event Action OnCreate;

        public MvpCreatorView(MvpCreatorModel model)
        {
            _model = model;
        }

        public void DrawUI()
        {
            EnsureStyles();
            GUILayout.Label("Parameters", EditorStyles.boldLabel);

            _model.ModuleName = EditorGUILayout.TextField("Name", _model.ModuleName);
            _model.Namespace = EditorGUILayout.TextField("Namespace", _model.Namespace);

            EditorGUILayout.BeginHorizontal();
            _model.FolderPath = EditorGUILayout.TextField("Path", _model.FolderPath);

            if (GUILayout.Button("...", GUILayout.Width(30)))
            {
                _model.FolderPath = EditorUtility.OpenFolderPanel("Select Folder", _model.FolderPath, "");
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(10);
            GUILayout.Label("Preview", EditorStyles.boldLabel);
            _index = GUILayout.Toolbar(_index, _modules);

            string example = GetModuleExample(_index);
            example = Highlight(example, _model.Namespace, "green");
            example = Highlight(example, _model.ModuleName, "yellow");
            
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.Height(400));
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextArea(example, _richTextStyle, GUILayout.ExpandHeight(true));
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndScrollView();

            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Create"))
            {
                OnCreate?.Invoke();
            }
        }

        private string GetModuleExample(int index)
        {
            switch (index)
            {
                case 0:
                    return _model.GetManagerTemplate();
                case 1:
                    return _model.GetModelTemplate();
                case 2:
                    return _model.GetViewTemplate();
                case 3:
                    return _model.GetPresenterTemplate();
                default:
                    return string.Empty;
            }
        }

        private string Highlight(string text, string value, string color)
        {
            return text.Replace(value, $"<color={color}>{value}</color>");
        }

        private void EnsureStyles()
        {
            if (_richTextStyle == null)
            {
                _richTextStyle = new GUIStyle(EditorStyles.textArea);
                _richTextStyle.richText = true;
            }
        }
    }
}