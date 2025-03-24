using UnityEngine;
using UnityEngine.Events;

public class KillBallCollider : MonoBehaviour
{
    [SerializeField] public UnityEvent<bool> ballDestroyed;
    [SerializeField] private bool ballBucket = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Destroy(collision.gameObject);
            ballDestroyed?.Invoke(ballBucket);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
