using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
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
    public Image image;
    public AudioSource placeSound;
    public ContructionIcon bridge;
    public ContructionIcon elevator;
    public ContructionIcon door;
    public LayerMask construction;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (Input.GetMouseButtonDown(0)) // Левая кнопка мыши
        {
            mousePos.z = 0f;
            switch (selectedItem)
            {
                case SelectedItem.Bridge:
                    Instantiate(bridgePrefab, mousePos, Quaternion.identity);
                    selectedItem = SelectedItem.None;
                    onUse.Invoke();
                    image.enabled = false;
                    placeSound.Play();
                    bridge.count--;
                    break;
                case SelectedItem.Elevator:
                    Instantiate(elevatorPrefab, mousePos, Quaternion.identity);
                    selectedItem = SelectedItem.None;
                    onUse.Invoke();
                    image.enabled = false;
                    placeSound.Play();
                    elevator.count--;
                    break;
                case SelectedItem.Door:
                    Instantiate(doorPrefab, mousePos, Quaternion.identity);
                    selectedItem = SelectedItem.None;
                    onUse.Invoke();
                    image.enabled = false;
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
