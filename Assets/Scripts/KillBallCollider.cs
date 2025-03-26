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

    private void Awake()
    {
        ballDestroyed?.AddListener(Transform.FindFirstObjectByType<GameManager>().EndTurn);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Debug.Log(this.gameObject.name + " Ball in kill collider");
            //StartCoroutine(deleteBall(collision.gameObject));
            ballDestroyed?.Invoke(ballBucket);
            Destroy(collision.gameObject);
        }
    }



    /// <summary>
    /// Ensures GameManager logic can commence properly before the reference is destroyed
    /// </summary>
    private IEnumerator deleteBall(GameObject ball)
    {
        yield return new WaitForSeconds(0.3f);
    }
}
