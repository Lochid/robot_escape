using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum SelectedItem
{
    None,
    Bridge,
    Elevator,
    Door
}
public class Placer : MonoBehaviour
{
    [Header("Настройки")]
    public GameObject bridgePrefab;
    public GameObject elevatorPrefab;
    public GameObject doorPrefab;
    public Camera mainCamera;
    SelectedItem selectedItem = SelectedItem.None;
    public UnityEvent onUse;
    public AudioSource placeSound;
    public ContructionIcon bridge;
    public ContructionIcon elevator;
    public ContructionIcon door;
    public LayerMask construction;
    public PerspectiveCameraController perspectiveCameraController;
    [SerializeField] private float fixedDistanceFromCamera = 18.35f;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    private void Update()
    {

        Vector3 mousePos2 = Mouse.current.position.ReadValue();
        mousePos2.z = fixedDistanceFromCamera; // нужно только расстояние от камеры для корректного преобразования

        Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos2);


        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (Input.GetMouseButtonDown(0)) // Левая кнопка мыши
        {
            switch (selectedItem)
            {
                case SelectedItem.Bridge:
                    Instantiate(bridgePrefab, worldPos, Quaternion.identity);
                    selectedItem = SelectedItem.None;
                    onUse.Invoke();
                    perspectiveCameraController.AvoidMarker();
                    placeSound.Play();
                    bridge.count--;
                    break;
                case SelectedItem.Elevator:
                    Instantiate(elevatorPrefab, worldPos, Quaternion.identity);
                    selectedItem = SelectedItem.None;
                    onUse.Invoke();
                    perspectiveCameraController.AvoidMarker();
                    placeSound.Play();
                    elevator.count--;
                    break;
                case SelectedItem.Door:
                    Instantiate(doorPrefab, worldPos, Quaternion.identity);
                    selectedItem = SelectedItem.None;
                    onUse.Invoke();
                    perspectiveCameraController.AvoidMarker();
                    placeSound.Play();
                    door.count--;
                    break;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, construction))
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }

    public void SelectBridge()
    {
        selectedItem = SelectedItem.Bridge;
    }

    public void SelectElevator()
    {
        selectedItem = SelectedItem.Elevator;
    }

    public void SelectDoor()
    {
        selectedItem = SelectedItem.Door;
    }
}
