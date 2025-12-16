using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class CreateAuditoriumPrefab : EditorWindow
{
    [MenuItem("Tools/Environment/Create Auditorium Prefab")]
    public static void ShowWindow()
    {
        GetWindow<CreateAuditoriumPrefab>("Create Auditorium Prefab");
    }

    string fbxRelativePath = "Assets/Environment/907.fbx";
    string texturesFolder = "Assets/Environment/Textures";
    string materialsFolder = "Assets/Environment/Materials";
    string prefabOutput = "Assets/Environment/Prefabs/907_Auditorium.prefab";

    void OnGUI()
    {
        GUILayout.Label("Auto-create Prefab from FBX", EditorStyles.boldLabel);

        fbxRelativePath = EditorGUILayout.TextField("FBX Path", fbxRelativePath);
        texturesFolder = EditorGUILayout.TextField("Textures Folder", texturesFolder);
        materialsFolder = EditorGUILayout.TextField("Materials Folder", materialsFolder);
        prefabOutput = EditorGUILayout.TextField("Prefab Output", prefabOutput);

        if (GUILayout.Button("Create Prefab"))
        {
            CreatePrefabFromFBX();
        }
    }

    void CreatePrefabFromFBX()
    {
        if (!File.Exists(fbxRelativePath))
        {
            Debug.LogError("❌ FBX not found at: " + fbxRelativePath);
            return;
        }

        // Создаём папки если их нет
        if (!AssetDatabase.IsValidFolder(materialsFolder))
        {
            AssetDatabase.CreateFolder("Assets/Environment", "Materials");
        }
        if (!AssetDatabase.IsValidFolder("Assets/Environment/Prefabs"))
        {
            AssetDatabase.CreateFolder("Assets/Environment", "Prefabs");
        }

        GameObject fbx = AssetDatabase.LoadAssetAtPath<GameObject>(fbxRelativePath);
        GameObject inst = (GameObject)PrefabUtility.InstantiatePrefab(fbx);
        inst.transform.position = Vector3.zero;
        inst.transform.rotation = Quaternion.identity;
        inst.transform.localScale = Vector3.one;

        // Находим все текстуры
        string[] texFiles = Directory.Exists(texturesFolder)
            ? Directory.GetFiles(texturesFolder, "*.*", SearchOption.AllDirectories)
                .Where(s => s.EndsWith(".png") || s.EndsWith(".jpg") || s.EndsWith(".jpeg"))
                .ToArray()
            : new string[0];

        // Создаём и назначаем материалы
        foreach (var renderer in inst.GetComponentsInChildren<MeshRenderer>(true))
        {
            Material[] mats = renderer.sharedMaterials;
            for (int i = 0; i < mats.Length; i++)
            {
                string matName = mats[i] != null ? mats[i].name : renderer.name + "_Mat";
                string matPath = Path.Combine(materialsFolder, matName + ".mat");

                Material mat = AssetDatabase.LoadAssetAtPath<Material>(matPath);
                if (mat == null)
                {
                    mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                    AssetDatabase.CreateAsset(mat, matPath);
                }

                // ищем подходящую текстуру
                foreach (var texPath in texFiles)
                {
                    if (texPath.ToLower().Contains(matName.ToLower()))
                    {
                        Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(texPath);
                        mat.SetTexture("_BaseMap", tex);
                        break;
                    }
                }

                mats[i] = mat;
            }
            renderer.sharedMaterials = mats;
        }

        // Коллайдеры для крупных объектов
        foreach (var mf in inst.GetComponentsInChildren<MeshFilter>())
        {
            if (mf.GetComponent<Collider>() == null)
            {
                mf.gameObject.AddComponent<MeshCollider>();
            }
        }

        // Сохраняем префаб
        PrefabUtility.SaveAsPrefabAssetAndConnect(inst, prefabOutput, InteractionMode.UserAction);
        DestroyImmediate(inst);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("✅ Префаб создан: " + prefabOutput);
    }
}
