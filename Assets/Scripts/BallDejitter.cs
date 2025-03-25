using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class BallDejitter : MonoBehaviour
{
    private Rigidbody2D rb;
    private CircleCollider2D cldr;
    [SerializeField] private float bounceStick, bounceStickTime;    //bounceStick = min velocity to stick to peg, bounceStickTime = time after stick is turned on that it checks if it is still in contact, checks repeatedly
    [SerializeField] private PhysicsMaterial2D[] materials; //The bouncy ball material and sticky material

    private int layermask = ~(1 << 6);  //Layer mask so that CircleCast only detects pegs

    void Start()
    {
        rb = GetComponent<Rigidbody2D> (); 
        cldr = GetComponent<CircleCollider2D> ();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.linearVelocity.magnitude < bounceStick)  //Checks velocity
        {
            cldr.sharedMaterial = materials[1]; //Sets to sticky if velocity too low
            StartCoroutine(StickCheck());   //Starts repeating timer checking if no longer in contact with peg
        }
    }
    private IEnumerator StickCheck()
    {
        do
        {
            yield return new WaitForSeconds(bounceStickTime);   //Waits for time
        } while (Physics2D.CircleCast(transform.position, cldr.radius+0.1f, Vector2.zero, 1f, layermask));  //Checks for peg in contact
        cldr.sharedMaterial = materials[0]; //After no peg in contact, sets back to bouncy

    }
}
