using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.Controls.AxisControl;

public class PerspectiveCameraController : MonoBehaviour
{
    [Header("Параметры перемещения")]
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private Vector2 minBounds;
    [SerializeField] private Vector2 maxBounds;

    [Header("Параметры зума")]
    [SerializeField] private float zoomSpeed = 100f;
    [SerializeField] private float minFov = 20f;
    [SerializeField] private float maxFov = 60f;

    [Header("Маркер и подсветка")]
    [SerializeField] private LayerMask deletableMask;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private float fixedDistanceFromCamera = 18.35f;

    private Camera cam;
    private Vector3 dragOrigin;
    private GameObject previewMarkerInstance;
    private GameObject highlightedObject;
    private Material originalMaterial;
    
    private void Start()
    {
        cam = Camera.main;
    }

    public void SetMarker(GameObject previewMarker)
    {
        previewMarkerInstance = Instantiate(previewMarker);
        previewMarkerInstance.SetActive(true);
    }

    public void AvoidMarker()
    {
        Destroy(previewMarkerInstance);
        previewMarkerInstance=null;
    }

    private void Update()
    {
        HandleZoom();
        HandleDrag();
        UpdatePreviewMarker();
        UpdateHighlight();
    }

    private void HandleZoom()
    {
        float scroll = Mouse.current.scroll.ReadValue().y;
        if (Mathf.Abs(scroll) > Mathf.Epsilon)
        {
            float newFov = cam.fieldOfView - scroll * zoomSpeed * Time.deltaTime;
            cam.fieldOfView = Mathf.Clamp(newFov, minFov, maxFov);
        }
    }

    private void HandleDrag()
    {
        if (Input.GetMouseButtonDown(2))
        {
            dragOrigin = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 currentPos = Input.mousePosition;
            Vector3 difference = dragOrigin - currentPos;

            Vector3 move = new Vector3(difference.x, difference.y, 0f) * moveSpeed * Time.deltaTime;
            transform.Translate(move, Space.Self);

            dragOrigin = currentPos;

            ClampPosition();
        }
    }

    private void ClampPosition()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
        pos.y = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y);
        transform.position = pos;
    }
    private void UpdatePreviewMarker()
    {
        if (previewMarkerInstance == null) return;

        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = fixedDistanceFromCamera; // нужно только расстояние от камеры для корректного преобразования

        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);

        previewMarkerInstance.transform.position = Vector3.Lerp(
            previewMarkerInstance.transform.position,
            worldPos,
            15f * Time.deltaTime
        );
    }




    private void UpdateHighlight()
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, deletableMask))
        {
            GameObject target = hit.collider.gameObject;

            if (highlightedObject != target)
            {
                ClearHighlight();

                Renderer rend = target.GetComponent<Renderer>();
                if (rend != null)
                {
                    originalMaterial = rend.material;
                    rend.material = highlightMaterial;
                }

                highlightedObject = target;
            }
        }
        else
        {
            ClearHighlight();
        }
    }

    private void ClearHighlight()
    {
        if (highlightedObject != null)
        {
            Renderer rend = highlightedObject.GetComponent<Renderer>();
            if (rend != null && originalMaterial != null)
            {
                rend.material = originalMaterial;
            }
            highlightedObject = null;
            originalMaterial = null;
        }
    }
}
