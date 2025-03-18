using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public UnityEvent OnShoot = new();

    void Update()
    {
        //on left mouse button clicked
        if (Input.GetMouseButtonDown(0))
        {
            OnShoot?.Invoke();
        }
    }
}
