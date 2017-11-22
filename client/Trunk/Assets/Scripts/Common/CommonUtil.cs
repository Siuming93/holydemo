
using Monster.BaseSystem;
using Monster.BaseSystem.SceneManager;

public class CommonUtil
{
    public static void ReStart()
    {
        UpdateProxy.Instance.StartCoroutine(SceneSwitcher.Instance.LoadScene(PreloadSceneManager.SCENE_NAME));
    }
}

