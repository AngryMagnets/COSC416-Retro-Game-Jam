using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// MonoBehaviour <c>KillBallCollider</c> Attached to an object with a 2D trigger collider that destroys the ball and sends a message with whether a ball bucket destroyed the ball.
/// </summary>
public class KillBallCollider : MonoBehaviour
{
    [SerializeField] public UnityEvent<bool> ballDestroyed; //bool = ball bucket or not
    [SerializeField] private bool ballBucket = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            StartCoroutine(deleteBall(collision.gameObject));
            ballDestroyed?.Invoke(ballBucket);
        }
    }

    /// <summary>
    /// Ensures GameManager logic can commence properly before the reference is destroyed
    /// </summary>
    private IEnumerator deleteBall(GameObject ball)
    {
        yield return new WaitForSeconds(1f);
        Destroy(ball);
    }
}
