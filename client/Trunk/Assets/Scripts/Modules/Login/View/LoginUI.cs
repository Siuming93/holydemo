using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Monster.Net;
public class LoginUI : MonoBehaviour
{
    public InputField acountInputField;
    public InputField ipAddressInputField;
    public Button loginButton;
    public Text stateText;

    void Start()
    {
        loginButton.onClick.AddListener(OnLoginBtnClick);
        StartCoroutine(ChcekState());
    }

    void OnDestory()
    {
        loginButton.onClick.RemoveListener(OnLoginBtnClick);
    }

    public void OnLoginBtnClick()
    {
        if (NetManager.Instance.connectState != ConnectState.Connected)
        {
            LoginController.instance.Login(acountInputField.text, ipAddressInputField.text);
        }
        else
        {
            LoginController.instance.SendMessageTest(acountInputField.text);
        }
    }

    private IEnumerator ChcekState()
    {
        WaitForSeconds wait = new WaitForSeconds(1);
        int count = 0;
        while (true)
        {
            switch (NetManager.Instance.connectState)
            {
                case ConnectState.TryConnecting:
                    string content = "正在连接";
                    for (int i = 0; i < count; i++)
                    {
                        content += ".";
                    }
                    stateText.text = content;
                    count++;
                    count %= 5;
                    break;
                case ConnectState.NonConnect:
                    stateText.text = "未连接";
                    break;
                case ConnectState.Connected:
                    stateText.text = "已连接";
                    break;
                case ConnectState.ConnectingOutTime:
                    stateText.text = "连接超时!!";
                    break;
                case ConnectState.ConnectedFailed:
                    stateText.text = "连接失败!!";
                    break;
            }
            yield return wait;
        }
    }
}
