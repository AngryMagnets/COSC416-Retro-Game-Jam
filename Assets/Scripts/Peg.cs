using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Peg : MonoBehaviour
{
    public UnityEvent<Peg> PegHit; //Message with color of peg being hit

    private char type = 'b';  //'b' blue, 'o' orange, 'g' green, 'p' purple

    // Should probably make the colours array static so that this isn't taking up memory for every single peg
    [SerializeField] private Color[] colors = new Color[4]; //Blue, orange, green, and purple colors
    [SerializeField] private Color[] highlights = new Color[4];
    private bool notHit = true;

    private void Start()
    {
        PegHit?.AddListener(GameManager.game.TouchPeg);
        UpdateColor(type);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (notHit)
        {
            PegHit?.Invoke(this);   //Sends Message with color of peg that this was hit
            notHit = false;
            changeColor(highlights[charToColor(type)]);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (notHit)
        {
            PegHit?.Invoke(this);   //Sends Message with color of peg that this was hit
            notHit = false;
            changeColor(highlights[charToColor(type)]);
        }
    }
    /// <summary>
    /// Method <c>GetColor</c> Returns peg color
    /// </summary>
    /// <returns>Returns a char of 'b'=blue, 'o'=orange, 'g'=green or 'p'=purple</returns>
    public char GetColor()
    {
        return type;
    }
    /// <summary>
    /// Method <c>UpdateColor</c> Updates the color of the peg
    /// <para>
    /// <paramref name="color"/> takes in values: 'b'=blue, 'o'=orange, 'g'=green or 'p'=purple
    /// </para>
    /// </summary>
    /// <param name="color">char of 'b'=blue, 'o'=orange, 'g'=green or 'p'=purple</param>
    public void UpdateColor(char color)
    {
        type = color;
        int i = charToColor(type);

        changeColor(colors[i]);
    }
    private int charToColor(char c)
    {
        switch (type)
        {
            case 'b':
                return 0;
            case 'o':
                return 1;
            case 'g':
                return 2;
            case 'p':
                return 3;
            default:
                return 0;
        }
    }
    private void changeColor (Color c)
    {
        SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.color = c;
        }
        else if (TryGetComponent<MeshRenderer>(out MeshRenderer m))
        {
            m.material.color = c;
        }
    }
}
