using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Login : MonoBehaviour
{
    // �н����带 �Է¹ޱ� ���� ����
    [SerializeField]
    private TMP_InputField userPasswordInputField;
    [SerializeField]
    private Button loginButton;

    // �α��� ȭ�鿡�� �α��� �׷� ��Ƽ�긦 �����ϱ� ���� ����
    [SerializeField]
    private LoginScreen loginScreen;

    public void OnUserPasswordInputFieldValueChanged(string text)
    {
        loginButton.interactable = text.Length > 0;
    }

    public void OnLoginButtonClick()
    {
        string password = userPasswordInputField.text;

        // TODO Temp
        if (password.Equals("Test"))
        {
            loginScreen.LoginGroupActive = false;

            Natty.os.Login();
        }

        ClearUserPasswordInputField();
    }

    public void FocusUserPasswordInputField()
    {
        userPasswordInputField.ActivateInputField();
    }

    public void ClearUserPasswordInputField()
    {
        userPasswordInputField.text = string.Empty;
    }
}
