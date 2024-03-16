using UnityEngine;
using UnityEngine.EventSystems;

public class LoginScreen : MonoBehaviour, IPointerClickHandler
{
    // �α��� ��ũ��Ʈ�� �ƹ� ȭ���� Ŭ������ �� ���� �α��� �׷�
    [SerializeField]
    private Login login;
    [SerializeField]
    private GameObject loginGroup;

    public bool LoginGroupActive
    {
        get
        {
            return loginGroup.activeSelf;
        }
        set
        {
            loginGroup.SetActive(value);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Natty.os.LoggedIn)
        {
            loginGroup.SetActive(!loginGroup.activeSelf);

            if (loginGroup.activeSelf)
            {
                login.FocusUserPasswordInputField();
            }
            else
            {
                login.ClearUserPasswordInputField();
            }
        }
    }
}
