using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.ProjectWindowCallback;

namespace EmptyPrefab
{
    public class CreatePrefabAction : EndNameEditAction
    {
        public override void Action(int instanceId, string path, string resourceFile)
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            GameObject emptyGameObject = new GameObject();
            string uniquePath = AssetDatabase.GenerateUniqueAssetPath(path);
            try
            {
                PrefabUtility.SaveAsPrefabAsset(emptyGameObject, uniquePath + ".prefab");
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
            }

            DestroyImmediate(emptyGameObject);

            AssetDatabase.Refresh();
        }
    }

    public class EmptyPrefab
    {
        private static string _folderPath = "Assets";

        [MenuItem("Assets/Create/Empty Prefab", false, 0)]
        private static void CreateEmptyPrefab()
        {
            CreatePrefab(GetPath() + "/EmptyPrefab");
        }

        private static void CreatePrefab(string assetPath)
        {
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
                0,
                ScriptableObject.CreateInstance<CreatePrefabAction>(),
                assetPath,
                (Texture2D)EditorGUIUtility.IconContent("Prefab Icon").image,
                null
            );
        }

        private static string GetPath()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);

            if (string.IsNullOrEmpty(path))
            {
                return _folderPath;
            }

            if (File.Exists(path))
            {
                return Path.GetDirectoryName(path);
            }

            if (Directory.Exists(path))
            {
                return path;
            }

            return _folderPath;
        }
    }
}