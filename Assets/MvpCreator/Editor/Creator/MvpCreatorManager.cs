using UnityEditor;
using UnityEngine;

namespace MvpCreator
{
    public class MvpCreatorManager : EditorWindow
    {
        private MvpCreatorModel _model;
        private MvpCreatorView _view;
        private MvpCreatorPresenter _presenter;

        [MenuItem("Assets/Create/MVP Module", false, 0)]
        private static void ShowWindow()
        {
            MvpCreatorManager window = GetWindow<MvpCreatorManager>("Create MVP Module");

            window.titleContent.image = EditorGUIUtility.IconContent("cs Script Icon").image;
            window.minSize = new Vector2(800, 600);
            window.maxSize = new Vector2(800, 600);
        }

        private void OnEnable()
        {
            _model = new MvpCreatorModel();

            ModelInit();

            _view = new MvpCreatorView(_model);
            _presenter = new MvpCreatorPresenter(_model, _view);
        }

        private void ModelInit()
        {
            _model.ModuleName = "NewModule";
            _model.Namespace = "NewNamespace";
            _model.FolderPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        }

        private void OnGUI()
        {
            _presenter.OnGUI();
        }
    }
}