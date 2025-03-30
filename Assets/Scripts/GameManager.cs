using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    public static GameManager game; protected void Awake () { game = GetComponent<GameManager> (); }
    /// <summary>
    /// Set a listener for PlayerController.NextTurn()
    /// </summary>
    [SerializeField] public UnityEvent NextTurn;
    [SerializeField] public BallManager ballManager;
    [SerializeField] private ScoreUI scoreUI;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private int[] pointValues = new int[4] {10,20,20,100 }; //0=blue, 1=orange, 2=green, 3=purple

    //public bool isShotActive = false; // Pretty sure I don't need this, basically an alias for !PlayerController.canShoot
    public int numPegs { get; private set; }
    public int score;
    private List<Peg> touchedPegs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (playerController == null)
        { playerController = Transform.FindFirstObjectByType<PlayerController>(); }
        touchedPegs = new List<Peg>(10);
    }

    public void TouchPeg(Peg peg)
    {
        touchedPegs.Add(peg);
    }

    public void EndTurn(bool isInBucket)
    {
        if (isInBucket)
        {
            Debug.Log("Landed In Bucket");
            ballManager.AddBall();
        }
        CalculateScore();
        // Do other things
        // NextTurn?.Invoke();
        clearPegsHelper(true);
    }

    public void BallStuck()
    {
        CalculateScore();
        clearPegsHelper(false);
    }

    private void CalculateScore()
    {
        foreach (Peg p in touchedPegs)
        {
            //Debug.Log(p.gameObject.name);
            int pScore = 0;
            char type = p.GetColor();
            switch (type)
            {
                case 'b':
                    pScore = pointValues[0];
                    // Do blue peg
                    break;
                case 'o':
                    pScore = pointValues[1];
                    // Do orange peg
                    break;
                case 'g':
                    pScore = pointValues[2];
                    // Do green peg
                    break;
                case 'p':
                    pScore = pointValues[3];
                    // Do purple peg
                    break;
            }
            //pScore *= scoreMult;
            score += pScore;
        }
        scoreUI.UpdateScore(score);
    }
    private void clearPegsHelper(bool endTurn)
    {
        StartCoroutine(clearPegs(endTurn));
    }
    private IEnumerator clearPegs(bool endTurn)
    {
        while (touchedPegs.Count > 0)
        {
            GameObject destroyTarget = touchedPegs[0].gameObject;
            Destroy(destroyTarget);
            touchedPegs.RemoveAt(0);
            yield return new WaitForSeconds(0.12f);
        }
        playerController.canShoot = endTurn;
    }
}

public enum pegTypes
{
    blue,
    orange,
    green,
    purple
}