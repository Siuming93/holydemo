using UnityEngine;
using Monster.Net;
public class UpdateProxy : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        NetManager.Instance.Dispatch();
	}

    void OnDestroy()
    {
        NetManager.Instance.Close();
    }
}
