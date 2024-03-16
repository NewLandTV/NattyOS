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

    // ���μ��� ���� �ٲٱ�
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

    // ���μ��� ���۽� ���ʷ� ����
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

    // �ּ�ȭ ��ư�� ������ ��
    public void OnMinimumButtonClick()
    {
        print("�̱��� - �ּ�ȭ");
    }

    // ��� ��ü ȭ�� ��ư�� ������ ��
    public void OnToggleFullScreenButtonClick()
    {
        print("�̱��� - ��� Ǯ ��ũ��");
    }

    // ���μ����� ����Ǳ� ���� ȣ�� ��
    public void Quit()
    {
        Release();

        Natty.os.ReleaseProcess(processIndex);

        Destroy(gameObject);
    }

    // ������ ����
    protected virtual void Main()
    {
        print($"���μ��� ���۵� : processIndex == {processIndex}");
    }

    // ���μ��� ���� ����
    protected virtual void Release()
    {
        print($"{processIndex} : ���μ��� ��ȯ ��.");
    }
}
