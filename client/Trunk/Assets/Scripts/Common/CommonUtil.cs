
using Monster.BaseSystem;
using Monster.BaseSystem.SceneManager;

public class CommonUtil
{
    public static void ReStart()
    {
        UpdateProxy.Instance.StartCoroutine(SceneSwitcher.Instance.LoadScene(LoginSceneManager.SCENE_NAME));
    }
}

