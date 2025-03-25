using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject ball;
    [SerializeField] private int startingBalls;
    private int numBalls;
    private List<GameObject> ballList;

    private void Start()
    {
        ballList = new List<GameObject>();
    }
    void Awake()
    {
        StartCoroutine(AddBalls(startingBalls));
    }

    public void AddBall ()
    {
        Instantiate(ball, spawnPoint);
        numBalls++;
    }

    public void RemoveBall()
    {

        numBalls--;
    }

    private IEnumerator<WaitForSeconds> AddBalls (int ballCount)
    {
        if (ballCount > 0)
        {
            yield return new WaitForSeconds(0.3f);
            ballList.Add(Instantiate(ball, spawnPoint));
            StartCoroutine(AddBalls(ballCount - 1));
        }
        yield return null;
    }
}
