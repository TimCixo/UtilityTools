using UnityEditor;
using UnityEngine;
using System.IO;

namespace MvpCreator
{
    public class MvpCreatorPresenter
    {
        private MvpCreatorModel _model;
        private MvpCreatorView _view;

        public MvpCreatorPresenter(MvpCreatorModel model, MvpCreatorView view)
        {
            _model = model;
            _view = view;

            _view.OnCreate += CreateMvpScripts;
        }

        public void OnGUI()
        {
            _view.DrawUI();
        }

        public void CreateMvpScripts()
        {
            string modulePath = Path.Combine(_model.FolderPath, _model.ModuleName);

            if (!Directory.Exists(modulePath))
            {
                Directory.CreateDirectory(modulePath);
            }

            CreateScript(modulePath, _model.ModuleName + "Manager.cs", _model.GetManagerTemplate());
            CreateScript(modulePath, _model.ModuleName + "Model.cs", _model.GetModelTemplate());
            CreateScript(modulePath, _model.ModuleName + "View.cs", _model.GetViewTemplate());
            CreateScript(modulePath, _model.ModuleName + "Presenter.cs", _model.GetPresenterTemplate());

            AssetDatabase.Refresh();
        }

        private void CreateScript(string folderPath, string fileName, string content)
        {
            string fullPath = Path.Combine(folderPath, fileName);

            if (!File.Exists(fullPath))
            {
                File.WriteAllText(fullPath, content);
                Debug.Log($"{fileName} created at {fullPath}");
            }
            else
            {
                Debug.LogWarning($"{fileName} already exists at {fullPath}");
            }
        }
    }
}