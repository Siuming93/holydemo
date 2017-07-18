using UnityEditor;
using UnityEditor.SceneManagement;

public class Monster_ArtTools
{
    [MenuItem("Tools/Art/OpenPreLoadScene %H", false)]
    public static void OpenPreloadScene()
    {
        if (EditorApplication.isPlaying)
            return;
        EditorSceneManager.OpenScene("Assets/Scenes/Preload.unity");
        EditorApplication.isPlaying = true;
    }

    [MenuItem("Tools/Art/OpenUITestScene %M", false)]
    public static void OpenUITestScene()
    {
        if (EditorApplication.isPlaying)
            return;

        EditorSceneManager.OpenScene("Assets/Scenes/test/UITest.unity");
    }

    [MenuItem("Tools/Art/ToggleSelectedActive %G", false)]
    public static void ToggleSelectedActive()
    {
        foreach (var gameObject in Selection.gameObjects)
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
