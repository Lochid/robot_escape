using UnityEngine;
using UnityEngine.InputSystem;

public class PerspectiveCameraController : MonoBehaviour
{
    [Header("Параметры перемещения")]
    public float moveSpeed = 0.5f;  // скорость перемещения при драге

    [Header("Параметры зума")]
    public float zoomSpeed = 10f;   // скорость изменения поля зрения
    public float minFov = 20f;
    public float maxFov = 60f;

    private Vector3 dragOrigin;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        HandleZoom();
        HandleDrag();
    }

    private void HandleZoom()
    {
        float scroll = Mouse.current.scroll.ReadValue().y;
        if (scroll != 0f)
        {
            float newFov = cam.fieldOfView - scroll * zoomSpeed * Time.deltaTime;
            cam.fieldOfView = Mathf.Clamp(newFov, minFov, maxFov);
        }
    }

    private void HandleDrag()
    {
        if (Input.GetMouseButtonDown(2)) // Средняя кнопка мыши
        {
            dragOrigin = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 currentPos = Input.mousePosition;
            Vector3 difference = dragOrigin - currentPos;

            // перемещаем камеру вдоль локальных осей
            Vector3 move = new Vector3(difference.x, difference.y, 0f) * moveSpeed * Time.deltaTime;
            transform.Translate(move, Space.Self);

            dragOrigin = currentPos;
        }
    }
}
