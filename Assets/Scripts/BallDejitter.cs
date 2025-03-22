using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class BallDejitter : MonoBehaviour
{
    private Rigidbody2D rb;
    private CircleCollider2D cldr;
    [SerializeField] private float bounceStick, bounceStickTime;
    [SerializeField] private PhysicsMaterial2D[] materials;

    private int layermask = ~(1 << 6);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D> (); 
        cldr = GetComponent<CircleCollider2D> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(rb.linearVelocity.magnitude);
        if (rb.linearVelocity.magnitude < bounceStick)
        {
            cldr.sharedMaterial = materials[1];
            StartCoroutine(StickCheck());
        }
    }
    private IEnumerator StickCheck()
    {
        do
        {
            yield return new WaitForSeconds(bounceStickTime);
        } while (Physics2D.CircleCast(transform.position, cldr.radius+0.1f, Vector2.zero, 1f, layermask));
        cldr.sharedMaterial = materials[0];

    }
}
