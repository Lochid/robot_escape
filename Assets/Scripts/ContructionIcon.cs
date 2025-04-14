using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContructionIcon : MonoBehaviour
{
    Button button;
    public Image image;
    public Sprite sprite;
    public int count = 0;
    public TMP_Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
    }

    private void Update()
    {
        text.text = count.ToString();
        if (count <= 0)
        {
            button.interactable = false;
        }
    }
    public void Choise()
    {
        button.interactable = false;
        image.enabled = (true);
        image.sprite = sprite;
    }
    public void UnChoise()
    {
        if (count > 0)
        {
            button.interactable = true;
        }
    }
}
