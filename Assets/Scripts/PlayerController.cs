using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float mouseWheelAngle;

    public Transform shooterPivot;
    public Camera mainCamera;
    public GameObject ball;

    public float spriteOffset = 90f;
    public float minAngle = -90f;
    public float maxAngle = 90f;

    public float scrollSpeed = 0.5f;

    public float shootForce = -10f;

    public bool canShoot { get; set; }
    public bool poweredUp { get; set; }

    private void Awake()
    {
        canShoot = false;
        inputManager.OnShoot.AddListener(Shoot);
    }
    void Update()
    {
        //disable player movement if paused
        if (PauseManager.instance.IsSettingsMenuActive)
        {
            return;
        }

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

    private void Shoot()
    {
        if (canShoot)
        {
            canShoot = false;
            GameObject newBall = shootBall(poweredUp);

            if (poweredUp)
            {
                switch (PowerUpVaraible.powers[perks.origin])
                {
                    case 0: //Multiball
                        if (PowerUpVaraible.powers[perks.exploding] == 0)
                        {
                            int i = PowerUpVaraible.powers[perks.multiCount];
                            while (i>0)
                            {
                                StartCoroutine(shootDelay(PowerUpVaraible.MultiballDelay * i, poweredUp));
                                i--;
                            }
                        }
                        break;
                    case 1: //Fireball
                        if (PowerUpVaraible.powers[perks.fireCover] != 0)
                        {
                            //add the cover to the ball bucket
                        }
                        break;
                }
                poweredUp = false;
            }
        }
    }

    IEnumerator shootDelay (float delay, bool powered)
    {
        yield return new WaitForSeconds (delay);
        shootBall(powered);
    }

    private GameObject shootBall (bool powered)
    {
        GameObject newBall = Instantiate(ball, shooterPivot.position, shooterPivot.rotation);
        GameManager.game.ballActive += 1;

        Rigidbody2D rb = newBall.GetComponent<Rigidbody2D>();
        BallPowerUp bpu = newBall.GetComponent<BallPowerUp>();
        bpu.powered = powered;
        bpu.Activate();
        if (rb != null)
        {
            //apply force
            rb.AddForce(shooterPivot.up * shootForce, ForceMode2D.Impulse);
        }
        return newBall;
    }

    public void NextTurn ()
    {
        canShoot = true;
    }
}