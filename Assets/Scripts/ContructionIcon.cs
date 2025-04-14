using UnityEngine;
using UnityEngine.UI;

public class ContructionIcon : MonoBehaviour
{
    Button button;
    public Image image;
    public Sprite sprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
    }
    public void Choise()
    {
        button.interactable = false;
        image.enabled = (true);
        image.sprite = sprite;
    }
    public void UnChoise()
    {
        button.interactable = true;
    }
}
