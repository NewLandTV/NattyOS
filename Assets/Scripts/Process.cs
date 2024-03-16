using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Process : MonoBehaviour
{
    public class Info
    {
        public string name;

        public Info(string name)
        {
            this.name = name;
        }

        public Info(Info info)
        {
            name = info.name;
        }
    }

    private Info info;

    public Info GenericInfo
    {
        get
        {
            return info;
        }
        set
        {
            info = value;
        }
    }

    private int processIndex;

    [SerializeField]
    private TextMeshProUGUI titleText;
    private GameObject contents;
    public GameObject Contents
    {
        set
        {
            contents = value;
        }
    }
    private List<GameObject> contentObject = new List<GameObject>();

    // 프로세스 제목 바꾸기
    public void SetTitle(string title)
    {
        titleText.text = title;
    }

    public int AddObjectInContent(GameObject newObject)
    {
        GameObject instance = Instantiate(newObject, Vector3.zero, Quaternion.identity, contents.transform);

        contentObject.Add(instance);

        return contentObject.Count - 1;
    }

    public void RemoveObjectInContent(int objectId)
    {
        Destroy(contentObject[objectId]);

        contentObject.RemoveAt(objectId);
    }

    // 프로세스 시작시 최초로 실행
    public void Execute(Info info, GameObject contents)
    {
        processIndex = Natty.os.StartProcess(info, contents);

        this.info = info;
        this.contents = contents;

        Main();
    }

    public void CallMain()
    {
        Main();
    }

    // 최소화 버튼이 눌렸을 때
    public void OnMinimumButtonClick()
    {
        print("미구현 - 최소화");
    }

    // 토글 전체 화면 버튼이 눌렸을 때
    public void OnToggleFullScreenButtonClick()
    {
        print("미구현 - 토글 풀 스크린");
    }

    // 프로세스가 종료되기 전에 호출 됨
    public void Quit()
    {
        Release();

        Natty.os.ReleaseProcess(processIndex);

        Destroy(gameObject);
    }

    // 진입점 구현
    protected virtual void Main()
    {
        print($"프로세스 시작됨 : processIndex == {processIndex}");
    }

    // 프로세스 종료 구현
    protected virtual void Release()
    {
        print($"{processIndex} : 프로세스 반환 됨.");
    }
}
