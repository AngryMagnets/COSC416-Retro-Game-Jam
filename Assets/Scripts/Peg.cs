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
    private bool notHit = true;

    [SerializeField] 
    private float stuckTimer;

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
            //flash color
            //flashSprite.enable(); ??? as a child to each peg prefab, can edit
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
        int i;
        switch (type)
        {
            case 'b':
                i = 0;
                break;
            case 'o':
                i = 1;
                break;
            case 'g':
                i = 2;
                break;
            case 'p':
                i = 3;
                break;
            default:
                i = 0;
                break;
        }

        SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.color = colors[i];
        } else if (TryGetComponent<MeshRenderer>(out MeshRenderer m))
        {
            m.material.color = colors[i];
        }
    }
}
