using UnityEngine;
using UnityEngine.EventSystems;

public class LoginScreen : MonoBehaviour, IPointerClickHandler
{
    // 로그인 스크립트와 아무 화면을 클릭했을 때 나올 로그인 그룹
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
