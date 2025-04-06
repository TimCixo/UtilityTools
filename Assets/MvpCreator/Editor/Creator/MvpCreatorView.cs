using System;
using UnityEditor;
using UnityEngine;

namespace MvpCreator
{
    public class MvpCreatorView
    {
        private int _index = 0;
        private string _previousNamespace = "";
        private Vector2 _scrollPosition;
        private string[] _modules = new string[] { "Manager", "Model", "View", "Presenter" };
        private Func<string>[] _moduleTemplates;
        private GUIStyle _richTextStyle;
        private MvpCreatorModel _model;

        public event Action OnCreate;

        public MvpCreatorView(MvpCreatorModel model)
        {
            _model = model;

            _moduleTemplates = new Func<string>[]
            {
                _model.GetManagerTemplate,
                _model.GetModelTemplate,
                _model.GetViewTemplate,
                _model.GetPresenterTemplate
            };
        }

        public void DrawUI()
        {
            EnsureStyles();
            GUILayout.Label("Parameters", EditorStyles.boldLabel);

            DrawNamespaceField();
            DrawFolderPathField();
            DrawNewFolderCheckbox();

            GUILayout.Space(10);

            DrawPrewiewSection();

            GUILayout.FlexibleSpace();

            DrawCreateButton(HasInvalidInput());
        }

        private void DrawNamespaceField()
        {
            _previousNamespace = _model.Namespace;
            _model.Namespace = EditorGUILayout.TextField("Namespace", _model.Namespace);
        }

        private void DrawFolderPathField()
        {
            EditorGUILayout.BeginHorizontal();

            _model.FolderPath = EditorGUILayout.TextField("Path", _model.FolderPath);

            if (GUILayout.Button("...", GUILayout.Width(30)))
            {
                _model.FolderPath = EditorUtility.OpenFolderPanel("Select Folder", _model.FolderPath, "");
            }

            EditorGUILayout.EndHorizontal();
        }

        private void DrawNewFolderCheckbox()
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Create New Folder", GUILayout.Width(150));
            _model.CreateNewFolder = EditorGUILayout.Toggle(_model.CreateNewFolder, GUILayout.Width(20));

            if (_model.NewFolderName.Length <= 0 || _model.NewFolderName == _previousNamespace)
            {
                _model.NewFolderName = _model.Namespace;
            }

            EditorGUI.BeginDisabledGroup(!_model.CreateNewFolder);
            _model.NewFolderName = EditorGUILayout.TextField(_model.NewFolderName);
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.EndHorizontal();
        }

        private void DrawPrewiewSection()
        {
            GUILayout.Label("Preview", EditorStyles.boldLabel);
            _index = GUILayout.Toolbar(_index, _modules);

            string example = _moduleTemplates[_index]();
            example = Highlight(example, $"namespace {_model.Namespace}", "green");

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.Height(400));
            EditorGUI.BeginDisabledGroup(true);

            EditorGUILayout.TextArea(example, _richTextStyle, GUILayout.ExpandHeight(true));

            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndScrollView();
        }

        private void DrawCreateButton(bool disable)
        {
            EditorGUI.BeginDisabledGroup(disable);
            if (GUILayout.Button("Create"))
            {
                OnCreate?.Invoke();
            }
            EditorGUI.EndDisabledGroup();
        }

        private bool HasInvalidInput()
        {
            if (string.IsNullOrEmpty(_model.Namespace))
            {
                EditorGUILayout.HelpBox("Namespace cannot be empty.", MessageType.Warning);
                return true;
            }

            return false;
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