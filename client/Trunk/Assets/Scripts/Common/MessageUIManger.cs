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

    void Start()
    {
        scrollPos = new Vector2(Screen.width / 4, Screen.height);
        _contentBuilder = new StringBuilder();
    }

    private void OnLogReceived(string condition, string stacktrace, LogType type)
    {
        _logInfos.Add(new KeyValuePair<float, string>(Time.realtimeSinceStartup, condition));
        _stacktraceInfos.Add(new KeyValuePair<float, string>(Time.realtimeSinceStartup, stacktrace));
    }

    private List<KeyValuePair<float, string>> _logInfos = new List<KeyValuePair<float, string>>();
    private List<KeyValuePair<float, string>> _stacktraceInfos = new List<KeyValuePair<float, string>>();
    private bool _showStack = false;
    private bool _showLog = false;
    private int _fontSize = 24;
    private Vector2 scrollPos;
    private StringBuilder _contentBuilder;

    void OnGUI()
    {
        GUILayout.Label(" \n");
        GUILayout.BeginHorizontal(GUILayout.MaxWidth(400));
        GUI.skin.label.fontSize = _fontSize;
        GUI.skin.button.fontSize = _fontSize;
        if (GUILayout.Button(" Log "))
        {
            _showLog = !_showLog;
        }
        if (GUILayout.Button("Stacktrace"))
        {
            _showStack = !_showStack;
        }
        if (GUILayout.Button(" + "))
        {
            _fontSize++;
        }
        if (GUILayout.Button(" - "))
        {
            _fontSize--;
        }
        if (GUILayout.Button(" Clear "))
        {
            _logInfos.Clear();
            _stacktraceInfos.Clear();
        }
        GUILayout.EndHorizontal();
        if (!_showLog)
            return;
        scrollPos = GUILayout.BeginScrollView(scrollPos);
        GUILayout.BeginVertical();
        for (int i = 0; i < _logInfos.Count; i++)
        {
            var content = _contentBuilder.AppendLine(_logInfos[i].Value);
            if (_showStack)
                content.Append(_stacktraceInfos[i].Value);
            GUILayout.Label(content.ToString(), GUILayout.MaxWidth(1000));
            _contentBuilder.Remove(0, _contentBuilder.Length);
        }
        GUILayout.EndVertical();
        GUILayout.EndScrollView();


    }
#else

#endif
}
