using System;
using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;
using System.IO;

namespace MvpCreator
{
    public class MvpCreatorView
    {
        private bool _disableCreateButton = true;
        private int _index = 0;
        private string _previousNamespace = "";
        private string[] _modules = new string[] { "Manager", "Model", "View", "Presenter" };
        private Vector2 _scrollPosition;
        private GUIStyle _richTextStyle;
        private MvpCreatorModel _model;
        private Func<string>[] _moduleTemplates;

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
            _disableCreateButton = false;
            GUILayout.Label("Parameters", EditorStyles.boldLabel);

            DrawNamespaceField();
            DrawPrefixField();
            DrawFolderPathField();
            DrawNewFolderCheckbox();

            GUILayout.Space(10);

            DrawPrewiewSection();

            GUILayout.FlexibleSpace();

            DrawCreateButton();
        }

        private void DrawNamespaceField()
        {
            Regex NamespaceRegex = new(@"^([_\p{L}][_\p{L}\p{N}]*)(\.[_\p{L}][_\p{L}\p{N}]*)*$", RegexOptions.Compiled);

            _previousNamespace = _model.Namespace;
            _model.Namespace = EditorGUILayout.TextField("Namespace", _model.Namespace);


            if (string.IsNullOrEmpty(_model.Namespace))
            {
                EditorGUILayout.HelpBox("Namespace cannot be empty.", MessageType.Warning);
                _disableCreateButton = true;
            }
            else if (!NamespaceRegex.IsMatch(_model.Namespace))
            {
                EditorGUILayout.HelpBox("Namespace is not valid.", MessageType.Warning);
                _disableCreateButton = true;
            }
        }

        private void DrawPrefixField()
        {
            Regex ClassNameRegex = new(@"^[_\p{L}][_\p{L}\p{N}]*$", RegexOptions.Compiled);

            _model.Prefix = EditorGUILayout.TextField("Prefix", _model.Prefix);

            if (!ClassNameRegex.IsMatch(_model.Prefix) && _model.Prefix.Length > 0)
            {
                EditorGUILayout.HelpBox("Prefix is not valid.", MessageType.Warning);
                _disableCreateButton = true;
            }
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

            if (!Directory.Exists(_model.FolderPath))
            {
                EditorGUILayout.HelpBox("Folder path is not valid.", MessageType.Warning);
                _disableCreateButton = true;
            }
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

            if (_model.NewFolderName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0 && _model.CreateNewFolder)
            {
                EditorGUILayout.HelpBox("New folder name cannot contain invalid characters.", MessageType.Warning);
                _disableCreateButton = true;
            }
            else if (string.IsNullOrEmpty(_model.NewFolderName) && _model.CreateNewFolder)
            {
                EditorGUILayout.HelpBox("New folder name cannot be empty.", MessageType.Warning);
                _disableCreateButton = true;
            }
        }

        private void DrawPrewiewSection()
        {
            GUILayout.Label("Preview", EditorStyles.boldLabel);
            _index = GUILayout.Toolbar(_index, _modules);

            string example = _moduleTemplates[_index]();
            example = Highlight(example, $"namespace {_model.Namespace}", "yellow");
            example = Highlight(example, $"{_model.Prefix}Manager", "yellow");
            example = Highlight(example, $"{_model.Prefix}Model", "yellow");
            example = Highlight(example, $"{_model.Prefix}View", "yellow");
            example = Highlight(example, $"{_model.Prefix}Presenter", "yellow");

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.Height(400));
            EditorGUI.BeginDisabledGroup(true);

            EditorGUILayout.TextArea(example, _richTextStyle, GUILayout.ExpandHeight(true));

            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndScrollView();
        }

        private void DrawCreateButton()
        {
            EditorGUI.BeginDisabledGroup(_disableCreateButton);
            if (GUILayout.Button("Create"))
            {
                OnCreate?.Invoke();
            }
            EditorGUI.EndDisabledGroup();
        }

        private string Highlight(string text, string value, string color)
        {
            if (text.Length <= 0 || value.Length <= 0)
            {
                return text;
            }

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