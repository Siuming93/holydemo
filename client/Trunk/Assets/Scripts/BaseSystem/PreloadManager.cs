using System.Collections;
using Monster.BaseSystem;
using Monster.BaseSystem.CoroutineTask;
using Monster.BaseSystem.SceneManager;
using Monster.Net;
using UnityEngine;
using UnityEngine.UI;

public class PreloadManager : MonoBehaviour
{
    public Text tipText;

    private Transform _uiRoot;
    private PreLoadTask task;

    void Awake()
    {
        //DontDestroyOnLoad(this);
    }

    void Start()
    {
        UpdateProxy.Instance.StartCoroutine(PreLoad());
    }

    void Update()
    {
        tipText.text = task.Description;
    }

    IEnumerator PreLoad()
    {
        task = new PreLoadTask();
        yield return task.Run();
    }
}