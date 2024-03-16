using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FilesApp : App
{
    [SerializeField]
    private TextMeshProUGUI currentDirectoryNameText;
    [SerializeField]
    private Scrollbar verticalScrollbar;

    [SerializeField]
    private GameObject dataPrefab;
    [SerializeField]
    private Transform dataParent;

    private List<FileData> fileList = new List<FileData>();

    private DirectoryInfo defaultDirectory;
    private DirectoryInfo currentDirectory;

    protected override void Main()
    {
        string desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        defaultDirectory = new DirectoryInfo(desktopFolder);
        currentDirectory = new DirectoryInfo(desktopFolder);

        UpdateDirectory(currentDirectory);
    }

    protected override void Release()
    {
        base.Release();
    }

    private IEnumerator Start()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UpdateDirectory(defaultDirectory);
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                MoveToParentFolder(currentDirectory);
            }

            yield return null;
        }
    }

    private void UpdateDirectory(DirectoryInfo newDirectory)
    {
        currentDirectory = newDirectory;

        for (int i = 0; i < fileList.Count; i++)
        {
            Destroy(fileList[i].gameObject);
        }

        fileList.Clear();

        currentDirectoryNameText.text = currentDirectory.Name;
        verticalScrollbar.value = 1f;

        MakeData("...", FileData.Type.Directory);

        for (int i = 0; i < currentDirectory.GetDirectories().Length; i++)
        {
            MakeData(currentDirectory.GetDirectories()[i].Name, FileData.Type.Directory);
        }

        for (int i = 0; i < currentDirectory.GetFiles().Length; i++)
        {
            MakeData(currentDirectory.GetFiles()[i].Name, FileData.Type.File);
        }

        fileList.Sort((a, b) => a.FileName.CompareTo(b.FileName));

        for (int i = 0; i < fileList.Count; i++)
        {
            fileList[i].transform.SetSiblingIndex(i);

            if (fileList[i].FileName.Equals("..."))
            {
                fileList[i].transform.SetAsFirstSibling();
            }
        }
    }

    private void MoveToParentFolder(DirectoryInfo currentDirectory)
    {
        if (currentDirectory.Parent == null)
        {
            return;
        }

        UpdateDirectory(currentDirectory.Parent);
    }

    public void UpdateInputs(string fileDataName)
    {
        if (fileDataName.Equals("..."))
        {
            MoveToParentFolder(currentDirectory);

            return;
        }

        for (int i = 0; i < currentDirectory.GetDirectories().Length; i++)
        {
            if (fileDataName.Equals(currentDirectory.GetDirectories()[i].Name))
            {
                UpdateDirectory(currentDirectory.GetDirectories()[i]);

                return;
            }
        }

        for (int i = 0; i < currentDirectory.GetFiles().Length; i++)
        {
            if (fileDataName.Equals(currentDirectory.GetFiles()[i].Name))
            {
                Debug.Log($"Select file to {currentDirectory.GetFiles()[i].FullName}");
            }
        }
    }

    private void MakeData(string fileName, FileData.Type type)
    {
        GameObject instance = Instantiate(dataPrefab, Vector3.zero, Quaternion.identity, dataParent);
        FileData data = instance.GetComponent<FileData>();

        data.Setup(this, fileName, type);

        fileList.Add(data);
    }
}
