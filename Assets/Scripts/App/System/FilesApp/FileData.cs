using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FileData : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public enum Type
    {
        Directory,
        File
    }

    [SerializeField]
    private Sprite[] iconSprites;
    [SerializeField]
    private Color[] textColors;
    [SerializeField]
    private Image iconImage;
    [SerializeField]
    private TextMeshProUGUI nameText;

    private Type type;

    private string fileName;
    public string FileName => fileName;

    private int maxFileNameLength = 25;

    private FilesApp filesApp;

    public void Setup(FilesApp filesApp, string fileName, Type type)
    {
        this.fileName = fileName;
        this.type = type;
        this.filesApp = filesApp;

        iconImage.sprite = iconSprites[(int)type];
        nameText.color = textColors[(int)type];
        nameText.text = fileName;

        if (fileName.Length >= maxFileNameLength)
        {
            nameText.text = $"{fileName.Substring(0, maxFileNameLength)}..";
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        nameText.color = Color.green;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        filesApp.UpdateInputs(fileName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        nameText.color = textColors[(int)type];
    }
}
