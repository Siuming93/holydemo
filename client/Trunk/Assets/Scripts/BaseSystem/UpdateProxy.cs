using System;
using Monster.BaseSystem;
using UnityEngine;
using Monster.Net;
public class UpdateProxy : MonoBehaviour
{
    public static UpdateProxy Instance;

    public Action UpdateEvent;
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        NetManager.Instance.Update();

	    if (UpdateEvent != null)
	    {
	        UpdateEvent.Invoke();
	    }
	}

    void OnDestroy()
    {
        NetManager.Instance.Close();
    }
}
