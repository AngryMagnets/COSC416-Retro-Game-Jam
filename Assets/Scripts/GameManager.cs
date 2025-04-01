using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    [Header ("In Scene References")]
    public static GameManager game;
    [SerializeField] public BallManager ballManager;
    [SerializeField] private ScoreUI scoreUI;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private LayoutHandler layoutHandler;
    [SerializeField] private GameObject WinMenu;
    [SerializeField] private GameObject LossMenu;
    [SerializeField] private SoundHandler soundHandler;

    [Header ("Managed Variables")] 
    [SerializeField] private int[] pointValues = new int[4] { 10, 20, 20, 100 }; //0=blue, 1=orange, 2=green, 3=purple
    [SerializeField] private int LevelsToWin = 5;
    //public bool isShotActive = false; // Pretty sure I don't need this, basically an alias for !PlayerController.canShoot
    public int numPegs { get; private set; }
    public int score;
    private int numOrangePegs;
    private List<Peg> touchedPegs;

    private int levelsCompleted = 0;

    private float pitch;

    [Header("Events")]
    /// <summary>
    /// Set a listener for PlayerController.NextTurn()
    /// </summary>
    [SerializeField] public UnityEvent NextTurn;

    /// <summary>
    /// UnityEvent <c>EndLevel</c> True = win, false = lose
    /// </summary>
    public UnityEvent<bool> EndLevel;

    void Start()
    {
        WinMenu.SetActive (false);
        LossMenu.SetActive (false);

        SceneNavigator.Navigator.LoadNewPegLayout();

        if (playerController == null) { playerController = Transform.FindFirstObjectByType<PlayerController>(); }
        
        touchedPegs = new List<Peg>(10);

        //ballManager.OutOfBalls?.AddListener(SceneNavigator.Navigator.LoadMainMenu);
        NextTurn?.AddListener(layoutHandler.UpdatePurplePeg);
    }
    protected void Awake() { game = GetComponent<GameManager>(); }

    public void TouchPeg(Peg peg)
    {
        pitch = 1f + (0.2f * touchedPegs.Count);
        soundHandler.PlayPegHitSound(pitch);

        touchedPegs.Add(peg);
    }

    public void EndTurn(bool isInBucket)
    {
        if (isInBucket)
        {
            soundHandler.PlayInBucketSound();
            Debug.Log("Landed In Bucket");
            ballManager.AddBall();
        }
        CalculateScore();

        soundHandler.ResetPitch();
        
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
                    orangePegTouched();
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
    private void orangePegTouched() { numOrangePegs--; Debug.Log($"num orange pegs {numOrangePegs}"); }
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

            soundHandler.PlayPegClearSound();

            yield return new WaitForSeconds(0.12f);
        }
        if (endTurn)
        {
            if (numOrangePegs <= 0)
            {
                //SceneNavigator.Navigator.LoadNewPegLayout();
                EndLevel?.Invoke(true);
                WinMenu.SetActive(true);
            }
            else if (ballManager.CheckOutOfBalls())
            {
                EndLevel?.Invoke(false);
                LossMenu.SetActive(true);
            }
            else
            {
                playerController.canShoot = true;
                NextTurn?.Invoke();
            }
        }
    }

    public void LoadNextLevel()
    {
        levelsCompleted++;
        WinMenu.SetActive(false);
        if (levelsCompleted >= LevelsToWin)
        {
            //Win screen
        }
        else
        {
            SceneNavigator.Navigator.LoadNewPegLayout();
            ballManager.AddBallsHelper(ballManager.startingBalls);
        }
    }

    public void UpdateLayoutHandler (LayoutHandler lh)
    {
        this.layoutHandler = lh;
        numOrangePegs = layoutHandler.orangeCount;
        Debug.Log($"num orange pegs {numOrangePegs}");
    }
}