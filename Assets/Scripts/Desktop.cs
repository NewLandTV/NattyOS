using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Desktop : MonoBehaviour
{
    [SerializeField]
    private RawImage backgroundImage;

    [SerializeField]
    private GameObject appPrefab;

    private List<App> apps = new List<App>();

    public static Action<App.Data[]> showApps;
    public static Action<string> setBackgroundImage;

    private void Awake()
    {
        showApps = ShowApps;
        setBackgroundImage = SetBackgroundImage;
    }

    public void ShowApps(App.Data[] appDatas)
    {
        ClearAllAppOnDesktop();

        for (int i = 0; i < appDatas.Length; i++)
        {
            MakeAppOnDesktop(appDatas[i]);
        }
    }

    private void ClearAllAppOnDesktop()
    {
        if (apps.Count <= 0)
        {
            return;
        }

        for (int i = 0; i < apps.Count; i++)
        {
            Destroy(apps[i].gameObject);
        }

        apps.Clear();
    }

    private void MakeAppOnDesktop(App.Data appData)
    {
        GameObject instance = Instantiate(appPrefab, transform);

        App instanceAppComponent = instance.GetComponent<App>();

        instanceAppComponent.GenericData = new App.Data(appData);

        Button instanceButtonComponent = instance.GetComponent<Button>();

        instanceButtonComponent.onClick.AddListener(() =>
        {
            instanceAppComponent.Execute(new Process.Info(appData.name), appData.contents);
        });

        apps.Add(instanceAppComponent);
    }

    public void SetBackgroundImage(string path)
    {
        Texture2D texture;

        byte[] imageTextureBytes = File.ReadAllBytes(path);

        if (imageTextureBytes.Length > 0)
        {
            texture = new Texture2D(0, 0);

            texture.LoadImage(imageTextureBytes);

            backgroundImage.texture = texture;
        }
    }
}
