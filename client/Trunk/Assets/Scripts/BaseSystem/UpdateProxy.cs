using UnityEngine;
using Monster.Net;
public class UpdateProxy : MonoBehaviour
{
    public static UpdateProxy Instance;
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        NetManager.Instance.Update();
	}

    void OnDestroy()
    {
        NetManager.Instance.Close();
    }
}
