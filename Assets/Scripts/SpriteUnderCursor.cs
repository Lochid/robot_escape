using UnityEngine;

public class SpriteUnderCursor : MonoBehaviour
{
    [Header("Настройки смещения")]
    public Vector2 offset = Vector2.zero;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        rectTransform.position = mousePos + offset;

    }
}
