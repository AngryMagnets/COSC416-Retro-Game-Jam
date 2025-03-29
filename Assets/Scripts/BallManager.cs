using NUnit.Framework;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class BallManager : MonoBehaviour
{
    //[SerializeField] 
    //private GameManager gm;
    [Header("General Settings")]
    [SerializeField]
    TextMeshProUGUI ballText;
    [SerializeField]
    PlayerController player;
    [SerializeField]
    public UnityEvent OutOfBalls;

    [Header("Ball Spawning")]
    [SerializeField] 
    private Transform spawnPoint;
    [SerializeField] 
    private GameObject ball;
    [SerializeField] 
    private int startingBalls;
   
    [Header("Removal Animation")]
    [SerializeField]
    private Ease AnimationCurve;
    [SerializeField]
    private float duration;


    private int numBalls;
    private List<GameObject> ballList;

    private void Start()
    {
        numBalls = 0;
        ballList = new List<GameObject>(startingBalls);
        OutOfBalls = new UnityEvent();
    }
    void Awake()
    {
        player.canShoot = false;
        StartCoroutine(AddBalls(startingBalls));
    }

    public void AddBall ()
    {
        GameObject spawnBall = Instantiate(ball, spawnPoint.position, Quaternion.identity);
        ballList.Add(spawnBall);
        numBalls++;
        updateText();
    }

    public void RemoveBall()
    {
        if (player.canShoot)
        {
            ballList[0].transform.DOLocalMoveY(-10, duration).SetEase(AnimationCurve);
            numBalls--;
            updateText();
            GameObject ball = ballList[0];
            ballList.RemoveAt(0);
            StartCoroutine(DeleteBall(ball));
        }

        if (numBalls <= 0)
        {
            OutOfBalls?.Invoke();
        }
    }

    private IEnumerator DeleteBall (GameObject ball)
    {
        yield return new WaitForSeconds(1f);
        Destroy(ball);
    }

    private IEnumerator AddBalls (int ballCount)
    {
        player.canShoot = false;
        if (ballCount > 0)
        {
            yield return new WaitForSeconds(0.3f);
            GameObject spawnBall = Instantiate(ball, spawnPoint.position, Quaternion.identity);
            ballList.Add(spawnBall);
            numBalls++;
            updateText();
            StartCoroutine(AddBalls(ballCount - 1));
        }
        else
        {
            //Debug.Log("Setting canShoot True");
            player.canShoot = true;
        }
        yield return null;
    }

    private void updateText ()
    {
        ballText.SetText($"{numBalls}");
    }
}
