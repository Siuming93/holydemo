using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChatItem : MonoBehaviour {

	public Image image;
	public LayoutElement elment;
	public Text text;
	public RectTransform rectTransform;
	// Use this for initialization
	void Start () {
        rectTransform.sizeDelta = new Vector2(480, text.preferredHeight);
        elment.preferredHeight = rectTransform.sizeDelta.y;
    }
	
	// Update is called once per frame
	void Update ()
	{
	   
	}
}
