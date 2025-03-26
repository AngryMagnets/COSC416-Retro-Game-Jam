using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class BallUnstuck : MonoBehaviour
{
    public UnityEvent ballStuck;
    [SerializeField] private float stuckTimer = 1f;
    [SerializeField] private float reStuckTime = 2f;
    [SerializeField] private float minVelocity = 0.2f;
    private Rigidbody2D rb;

    private bool stuck = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ballStuck?.AddListener(GameManager.game.BallStuck);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        StartCoroutine(checkBallStuck());
    }

    private IEnumerator checkBallStuck()
    {
        yield return new WaitForSeconds(stuckTimer);
        if (rb.linearVelocity.magnitude <= minVelocity)
        {
            if (!stuck)
            {
                stuck = true;
                ballStuck?.Invoke();
                StartCoroutine(reStuckTimer());
            }
        }
    }

    private IEnumerator reStuckTimer()
    {
        yield return new WaitForSeconds(reStuckTime);
        stuck = false;
    }
}
