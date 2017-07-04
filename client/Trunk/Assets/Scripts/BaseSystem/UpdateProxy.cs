using UnityEngine;
using Monster.Net;
public class UpdateProxy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        NetManager.Instance.Dispatch();
	}
}
