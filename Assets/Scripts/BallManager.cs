using NUnit.Framework;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallManager : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject ball;
    [SerializeField] private int startingBalls;

    [SerializeField] TextMeshProUGUI ballText;

    [SerializeField]
    private Ease AnimationCurve;
    [SerializeField]
    private float duration;


    private int numBalls;
    private List<GameObject> ballList;

    private void Start()
    {
        numBalls = 0;
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
        ballList[0].transform.DOLocalMoveY(-10, duration).SetEase(AnimationCurve);
        numBalls--;
        GameObject temp = ballList[0];  
        ballList.RemoveAt(0);
        Destroy(temp);
    }

    private IEnumerator<WaitForSeconds> AddBalls (int ballCount)
    {
        if (ballCount > 0)
        {
            yield return new WaitForSeconds(0.3f);
            ballList.Add(Instantiate(ball, spawnPoint));
            numBalls++;
            StartCoroutine(AddBalls(ballCount - 1));
        }
        yield return new WaitForSeconds(0f);
    }
}
