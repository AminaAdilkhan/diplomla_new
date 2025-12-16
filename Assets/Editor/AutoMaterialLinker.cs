using UnityEngine;
using UnityEditor;
using System.IO;

public class AutoMaterialLinker : EditorWindow
{
    [MenuItem("Tools/Optimize FBX/Link Textures and Setup Prefab")]
    static void Init()
    {
        var path = EditorUtility.OpenFilePanel("Select FBX Model", "Assets", "fbx");
        if (string.IsNullOrEmpty(path)) return;

        string relative = "Assets" + path.Replace(Application.dataPath, "").Replace("\\", "/");
        GameObject fbx = AssetDatabase.LoadAssetAtPath<GameObject>(relative);
        if (fbx == null)
        {
            Debug.LogError("❌ Could not load FBX at " + relative);
            return;
        }

        // Создаём экземпляр во временной сцене
        GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(fbx);

        // Создаём материалы
        string matFolder = "Assets/Environment/Materials";
        if (!AssetDatabase.IsValidFolder(matFolder))
            AssetDatabase.CreateFolder("Assets/Environment", "Materials");

        // Привязка текстур
        string[] textures = Directory.GetFiles("Assets/Environment/Textures", "*.jpg", SearchOption.AllDirectories);
        foreach (var renderer in instance.GetComponentsInChildren<MeshRenderer>())
        {
            foreach (var mat in renderer.sharedMaterials)
            {
                if (mat == null) continue;
                string matPath = Path.Combine(matFolder, mat.name + ".mat");
                Material newMat = AssetDatabase.LoadAssetAtPath<Material>(matPath);
                if (newMat == null)
                {
                    newMat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                    AssetDatabase.CreateAsset(newMat, matPath);
                }

                // Поиск текстуры по имени материала
                foreach (var texPath in textures)
                {
                    if (texPath.ToLower().Contains(mat.name.ToLower()))
                    {
                        Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(texPath);
                        newMat.SetTexture("_BaseMap", tex);
                        break;
                    }
                }
                renderer.sharedMaterial = newMat;
            }
        }

        // Добавляем LODGroup
        var lodGroup = instance.AddComponent<LODGroup>();
        var renderers = instance.GetComponentsInChildren<Renderer>();
        LOD[] lods = new LOD[1];
        lods[0] = new LOD(0.5f, renderers);
        lodGroup.SetLODs(lods);
        lodGroup.RecalculateBounds();

        // Применяем Collider
        foreach (var mf in instance.GetComponentsInChildren<MeshFilter>())
            if (mf.GetComponent<MeshCollider>() == null)
                mf.gameObject.AddComponent<MeshCollider>();

        // Создаём префаб
        string prefabPath = "Assets/Environment/907_Auditorium.prefab";
        PrefabUtility.SaveAsPrefabAssetAndConnect(instance, prefabPath, InteractionMode.AutomatedAction);
        DestroyImmediate(instance);

        Debug.Log("✅ FBX optimized and prefab created at " + prefabPath);
    }
}
