using UnityEditor;
using UnityEngine;

public class MvpCreatorView : EditorWindow
{
    private string _baseName = "NewFeature";
    private string _path;

    private MvpCreatorPresenter presenter;

    [MenuItem("Assets/Create/MVP Module", false, 0)]
    public static void ShowWindow()
    {
        GetWindow<MvpCreatorView>("Create MVP Module");
    }

    private void OnEnable()
    {
        presenter = new MvpCreatorPresenter();
    }

    private void OnGUI()
    {
        GUILayout.Label("MVP Template Generator", EditorStyles.boldLabel);

        _baseName = EditorGUILayout.TextField("Base Name", _baseName);
        _path = EditorGUILayout.TextField("Path", AssetDatabase.GetAssetPath(Selection.activeObject));

        if (GUILayout.Button("Generate MVP Module"))
        {
            presenter.CreateMvpScripts(_baseName, _path);
        }
    }
}