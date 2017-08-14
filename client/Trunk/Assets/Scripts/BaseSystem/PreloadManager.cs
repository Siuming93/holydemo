using System.Collections;
using BaseSystem;
using Monster.BaseSystem.SceneManager;
using Monster.Net;
using UnityEngine;

public class PreloadManager : MonoBehaviour
{
    private Transform _uiRoot;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        StartCoroutine(PreLoad());
    }

    IEnumerator PreLoad()
    {
        int step = 0;
        new UIManager(GameObject.Find("Canvas").transform);
        Object origin = ResourcesFacade.Instance.Load<GameObject>("Prefab/UI/Preload/PreloadPanel");
        GameObject preloadView = Instantiate(origin) as GameObject;
        UIManager.Intance.AddChild(preloadView.transform);
        yield return step++;

        NetManager.Instance.Init();
        yield return step++;

        yield return SceneSwitcher.Instance.LoadScene(LoginSceneManager.SCENE_NAME);
        Destroy(preloadView);
        yield return step++;
    }

    void LoadNextScene()
    {

    }
}