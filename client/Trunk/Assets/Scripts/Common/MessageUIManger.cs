using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
 
/// <summary>
/// 通知信息的UI管理器
/// </summary>
public class MessageUIManger : MonoBehaviour
{

#if DEVELOPMENT_BUILD ||UNITY_EDITOR
    public static MessageUIManger Instance { get; private set; }


    void Awake()
    {
        Instance = this;
        Application.logMessageReceived += OnLogReceived;
    }

    private void OnLogReceived(string condition, string stacktrace, LogType type)
    {
        var sber = new StringBuilder();
        sber.AppendLine(condition);
        sber.Append(stacktrace);
        _printList.Add(new KeyValuePair<float, string>(Time.realtimeSinceStartup, sber.ToString()));
    }

    void FixedUpdate()
    {
        var curTime = Time.realtimeSinceStartup;
        for (int i = 0; i < _printList.Count; i++)
        {
            var cur = _printList[i];
            if (curTime - cur.Key >= 20)
            {
                _printList.Remove(cur);
            }
        }
    }

    private List<KeyValuePair<float, string>> _printList = new List<KeyValuePair<float, string>>();


     void OnGUI()
    {
        GUILayout.BeginScrollView(new Vector2(Screen.width / 4, Screen.height));
        GUILayout.BeginVertical();
        for (int i = 0; i < _printList.Count; i++)
        {
            GUILayout.Label(_printList[i].Value);
        }
        GUILayout.EndVertical();
        GUILayout.EndScrollView();
    }
#else

#endif
}
