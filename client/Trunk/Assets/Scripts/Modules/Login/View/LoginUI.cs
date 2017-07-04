using UnityEngine;
using UnityEngine.UI;
using Monster.Net;
public class LoginUI : MonoBehaviour
{
    public InputField acountInputField;
    public InputField ipAddressInputField;
    public Button loginButton;

    void Start()
    {
        loginButton.onClick.AddListener(OnLoginBtnClick);
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
}
