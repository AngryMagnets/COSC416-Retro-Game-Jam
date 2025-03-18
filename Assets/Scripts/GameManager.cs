using UnityEngine;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    public static GameManager game;
    protected void Awake ()
    {
        game = GetComponent<GameManager> ();
    }

    public int numBalls { get; private set; }
    public int numBlocks { get; private set; }
    public int score;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
