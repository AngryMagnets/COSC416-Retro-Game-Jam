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

    //public bool isShotActive = false; // Pretty sure I don't need this, basically an alias for !PlayerController.canShoot
    public int numPegs { get; private set; }
    public int score;
    private List<Peg> touchedPegs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (playerController == null)
        { playerController = Transform.FindFirstObjectByType<PlayerController>(); }
        touchedPegs = new List<Peg>();
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
        foreach (Peg p in touchedPegs)
        {
            //Debug.Log(p.gameObject.name);
            int pScore = 0;
            char type = p.type;
            switch (type)
            {
                case 'b':
                    pScore = 10;
                    // Do blue peg
                    break;
                case 'o':
                    pScore = 20;
                    // Do orange peg
                    break;
                case 'g':
                    pScore = 20;
                    // Do green peg
                    break;
                case 'p':
                    pScore = 100;
                    // Do purple peg
                    break;
            }
            //pScore *= scoreMult;
            score += pScore;
            scoreUI.UpdateScore(score);
            StartCoroutine(clearPegs());
            // Do other things
            // NextTurn?.Invoke();
        }
    }

    private IEnumerator clearPegs()
    {
        GameObject destroyTarget = touchedPegs[0].gameObject;
        touchedPegs.RemoveAt(0);
        yield return new WaitForSeconds(0.1f);
        Destroy(destroyTarget);
        if (touchedPegs.Count == 0)
        {
            playerController.canShoot = true;
            yield break;
        }
        else { StartCoroutine(clearPegs()); }
    }
}
