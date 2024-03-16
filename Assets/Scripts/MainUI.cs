using UnityEngine;

public class MainUI : MonoBehaviour
{
    public static new Transform transform;

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }
}
