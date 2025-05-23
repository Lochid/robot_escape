using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContructionIcon : MonoBehaviour
{
    Button button;
    public PerspectiveCameraController perspectiveCameraController;
    public GameObject marker;
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
        } else
        {
            button.interactable = true;
        }
    }
    public void Choise()
    {
        button.interactable = false;
        perspectiveCameraController.SetMarker(marker);
    }
    public void UnChoise()
    {
        if (count > 0)
        {
            button.interactable = true;
        }
    }
}
