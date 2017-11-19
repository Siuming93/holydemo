using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
/// <summary>
/// 通知信息的UI管理器
/// </summary>
public class MessageUIManger : MonoBehaviour
{

#if DEVELOPMENT_BUILD
    public static MessageUIManger Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
        Application.logMessageReceived += OnLogReceived;
    }

    private void OnLogReceived(string condition, string stacktrace, LogType type)
    {
        _printList.Add(condition);
    }

    private List<string> _printList = new List<string>();


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
#else

#endif
}
