using UnityEditor;
using UnityEngine;
using System.IO;

public class MvpCreatorPresenter
{
    public void CreateMvpScripts(string baseName, string path)
    {
        MvpCreatorModel model = new MvpCreatorModel(baseName);
        string modulePath = Path.Combine(path, baseName);
        
        if (!Directory.Exists(modulePath))
        {
            Directory.CreateDirectory(modulePath);
        }

        CreateScript(modulePath, baseName + "Model.cs", model.GetModelTemplate());
        CreateScript(modulePath, baseName + "View.cs", model.GetViewTemplate());
        CreateScript(modulePath, baseName + "Presenter.cs", model.GetPresenterTemplate());
        CreateScript(modulePath, baseName + "Manager.cs", model.GetManagerTemplate());

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
