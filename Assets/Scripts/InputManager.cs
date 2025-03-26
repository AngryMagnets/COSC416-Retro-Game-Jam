using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GraphicRaycaster uiRaycaster;
    [SerializeField] private GameObject pauseButton;

    public UnityEvent OnShoot = new();
    public UnityEvent OnPause = new();

    void Update()
    {
        //on esc key clicked
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPause?.Invoke();
        }

        //get pointer data
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        uiRaycaster.Raycast(pointerData, results);

        //Check if the pause button is in the raycast hits
        foreach (RaycastResult result in results)
        {
            if (result.gameObject == pauseButton)
            {
                //skip shoot
                return;
            }
        }

        if (PauseManager.instance.IsSettingsMenuActive)
        {
            return;
        }

        //on left mouse button clicked
        if (Input.GetMouseButtonDown(0))
        {
            OnShoot?.Invoke();
        }
    }
}
