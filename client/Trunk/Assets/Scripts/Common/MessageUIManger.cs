using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 通知信息的UI管理器
/// </summary>
public class MessageUIManger : MonoBehaviour
{
 #if !UNITY_EDITOR
    public Text MessageText;

    public static MessageUIManger Instance { get; private set; }

    private List<string> _printList = new List<string>();

    private void Awake()
    {
        Instance = this;
    }

    public void Print(string message)
    {
        _printList.Add(message);
    }

    /// <summary>
    /// 设定通知信息
    /// </summary>
    /// <param name="message"></param>
    /// <param name="timer">存留时间</param>
    public void SetMessage(string message, float timer = 2f)
    {
        MessageText.text = message;
        StartCoroutine(ClearMessage(timer));
    }

    /// <summary>
    /// 到时间后清空信息
    /// </summary>
    /// <param name="timer"></param>
    /// <returns></returns>
    private IEnumerator ClearMessage(float timer)
    {
        yield return new WaitForSeconds(timer);

        MessageText.text = "";
    }

    private void OnGUI()
    {
        GUILayout.BeginScrollView(new Vector2(Screen.width / 4, Screen.height));
        GUILayout.BeginVertical();
        for (int i = 0; i < _printList.Count; i++)
        {
            GUILayout.Label(_printList[i]);
        }
        GUILayout.EndVertical();
        GUILayout.EndScrollView();
    }
#endif
}
