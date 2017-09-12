using System.Collections;
using Monster.BaseSystem;
using Monster.BaseSystem.CoroutineTask;
using Monster.BaseSystem.SceneManager;
using Monster.Net;
using UnityEngine;

public class PreloadManager : MonoBehaviour
{
    private Transform _uiRoot;
    private PreLoadTask task;

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
        task = new PreLoadTask();
        yield return task.Run();
    }
}