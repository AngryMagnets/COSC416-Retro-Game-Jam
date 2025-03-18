using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform shooterPivot;
    public Camera mainCamera;

    public float spriteOffset = 90f;
    public float minAngle = -90f;
    public float maxAngle = 90f;

    public float mouseWheelAngle = 0f;
    public float scrollSpeed = 0.5f;
    void Update()
    {
        //getting mouse position in world space
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f; //only using x,y since we want 2d

        //find direction vector from pivot to mouse
        Vector3 direction = mouseWorldPos - shooterPivot.position;

        //find angle in degrees
        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + spriteOffset;

        //mouse wheel control
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        mouseWheelAngle += scrollDelta * scrollSpeed;
        angle += mouseWheelAngle;

        //if on left side, side angle to minAngle. Else clamp angle
        if (angle > maxAngle + 90f)
        {
            angle = minAngle;
        }
        else
        {
            angle = Mathf.Clamp(angle, minAngle, maxAngle);
        }

        //apply the rotation
        shooterPivot.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
