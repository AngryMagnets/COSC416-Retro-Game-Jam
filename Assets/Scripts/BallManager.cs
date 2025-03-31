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


    public int numBalls { get; private set; }
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
        //StartCoroutine(AddBalls(startingBalls));
        StartCoroutine(DevAddBalls(startingBalls));
    }

    public void AddBall ()
    {
        GameObject spawnBall = Instantiate(ball, spawnPoint.position, Quaternion.identity);
        ballList.Add(spawnBall);
        numBalls++;
        updateText();
    }
    public void AddBall(int n)
    {
        for (int i = 0; i < n; i++)
        {
            GameObject spawnBall = Instantiate(ball, spawnPoint.position, Quaternion.identity);
            ballList.Add(spawnBall);
            numBalls++;
            updateText();
        }
    }

    public void RemoveBall()
    {
        if (player.canShoot)
        {
            int idx = 0;
            ballList[idx].transform.DOLocalMoveY(-10, duration).SetEase(AnimationCurve);
            numBalls--;
            updateText();
            GameObject ball = ballList[idx];
            ballList.RemoveAt(idx);
            StartCoroutine(DeleteBall(ball));
        }
    }
    public bool CheckOutOfBalls ()
    {
        bool noBallsRemaining = numBalls <= 0;
        if (noBallsRemaining) { OutOfBalls?.Invoke(); }
        return noBallsRemaining;
    }

    private IEnumerator DeleteBall (GameObject ball)
    {
        yield return new WaitForSeconds(1f);
        Destroy(ball);
    }

    public int BallsUsed ()
    {
        return startingBalls - numBalls;
    }

    public void AddBallsHelper (int ballCount) { StartCoroutine(AddBalls(ballCount)); }
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
    private IEnumerator DevAddBalls(int ballCount)
    {
        player.canShoot = false;
        if (ballCount > 0)
        {
            yield return new WaitForEndOfFrame();
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
