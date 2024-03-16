using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Login : MonoBehaviour
{
    // 패스워드를 입력받기 위한 변수
    [SerializeField]
    private TMP_InputField userPasswordInputField;
    [SerializeField]
    private Button loginButton;

    // 로그인 화면에서 로그인 그룹 액티브를 조작하기 위한 변수
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
