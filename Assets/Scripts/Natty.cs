using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Scenes
{
    Login,
    Main
}

public enum CursorType
{
    None
}

public class Natty : MonoBehaviour
{
    public static Natty os;

    // 로그인 관련 컴포넌트
    [SerializeField]
    private Animator loginUIAnimator;
    [SerializeField]
    private Image loginProgressCircle;

    // 커서 관련 컴포넌트와 변수
    [SerializeField]
    private Texture2D[] cursorTexture;

    // 프로세스 프리펩과 생성되는 부모 위치
    [SerializeField]
    private GameObject processPrefab;
    private Transform processParent;
    [SerializeField]
    private GameObject[] appContents;

    // 현재 실행중인 프로세스들
    private List<Process> runningProcesses = new List<Process>();

    // bool 변수
    public bool LoggedIn
    {
        get;
        set;
    }

    // Wait For Seconds
    private WaitForSeconds waitTime2_5f;

    private void Awake()
    {
        if (os == null)
        {
            os = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SetCursor(CursorType.None);

        waitTime2_5f = new WaitForSeconds(2.5f);
    }

    public void Login()
    {
        LoggedIn = true;

        StartCoroutine(LoginCoroutine());
    }

    private IEnumerator LoginCoroutine()
    {
        loginUIAnimator.gameObject.SetActive(true);
        loginUIAnimator.SetBool("login", true);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync((int)Scenes.Main);

        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                yield return waitTime2_5f;

                loginUIAnimator.SetBool("login", false);
                loginUIAnimator.gameObject.SetActive(false);

                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }

        processParent = MainUI.transform;

        // 바탕 화면에 있는 파일, 폴더, 정보들
        App.Data[] appDatas = new App.Data[3]
        {
            new App.Data(0, "NewLand", "Files"),
            new App.Data(1, "Mosazy", "Ef0"),
            new App.Data(2, "NaN", "c20")
        };

        for (int i = 0; i < appDatas.Length; i++)
        {
            appDatas[i].contents = appContents[i];
        }

        Desktop.showApps(appDatas);
        Desktop.setBackgroundImage("E:\\UserData\\Images\\ScreenShotAndArt\\QuickElementTitleBkg.png");
    }

    public int StartProcess(Process.Info processInfo, GameObject contents)
    {
        GameObject processInstance = Instantiate(processPrefab, processParent);

        Process processInstanceProcessComponent = processInstance.GetComponent<Process>();

        processInstanceProcessComponent.GenericInfo = processInfo;

        processInstanceProcessComponent.SetTitle(processInfo.name);

        GameObject contentsClone = Instantiate(contents, processInstanceProcessComponent.transform);

        processInstanceProcessComponent.Contents = contentsClone;

        contentsClone.GetComponent<App>().CallMain();

        runningProcesses.Add(processInstanceProcessComponent);

        return runningProcesses.Count - 1;
    }

    public void ReleaseProcess(int processIndex)
    {
        runningProcesses.RemoveAt(processIndex);
    }

    // 커서 타입 변경
    public void SetCursor(CursorType type)
    {
        Cursor.SetCursor(cursorTexture[(int)type], Vector3.zero, CursorMode.ForceSoftware);
    }
}
