using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Transform shooterPivot;      
    public GameObject ball;      
    public float shootForce = -1f;      

    void Update()
    {
        //on left mouse button clicked
        if (Input.GetMouseButtonDown(0))
        {
            //spawn a new Ball at the pivot’s position and rotation
            GameObject newBall = Instantiate(ball, shooterPivot.position, shooterPivot.rotation);

            Rigidbody2D rb = newBall.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                //apply force
                rb.AddForce(shooterPivot.up * shootForce, ForceMode2D.Impulse);
            }
        }
    }
}
