using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Transform shooterPivot;
    public Camera mainCamera;
    void Update()
    {
        //getting mouse position in world space
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f; //only using x,y since we want 2d

        //find direction vector from pivot to mouse
        Vector3 direction = mouseWorldPos - shooterPivot.position;

        //find angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //rotate pivot around z-axis _ 90f since shooter head is below pivot
        shooterPivot.rotation = Quaternion.Euler(0f, 0f, angle + 90f);
    }
}
