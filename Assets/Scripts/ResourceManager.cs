using System.IO;
using UnityEngine;

public static class ResourceManager
{
    public static readonly string RESOURCE_PATH = Path.Combine(Application.persistentDataPath, "Resources");

    public static string DEFAULT_BACKGROUND_IMAGE_PATH => Path.Combine(RESOURCE_PATH, "2022-03-26_Background-Default.png");
}
