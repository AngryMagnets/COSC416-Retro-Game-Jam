using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Peg : MonoBehaviour
{
    public UnityEvent<char> PegHit; //Message with color of peg being hit

    private char type = 'b';  //'b' blue, 'o' orange, 'g' green, 'p' purple 
    [SerializeField] private Color[] colors = new Color[4]; //Blue, orange, green, and purple colors
    private bool notHit = true;

    [SerializeField] 
    private float stuckTimer;

    private bool isCurrentlyTouched = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isCurrentlyTouched = true;
        if (notHit)
        {
            PegHit?.Invoke(type);   //Sends Message with color of peg that this was hit
            notHit = false;
            GameManager.game.TouchPeg(this);
            //flash color
            //flashSprite.enable(); ??? as a child to each peg prefab, can edit
        }
    }
    // Could do this logic in an update function with conditionals but this also works
    private void OnCollisionStay2D(Collision2D collision)
    {
        isCurrentlyTouched = true;
        StartCoroutine(checkBallStuck());
    }
    private IEnumerator checkBallStuck()
    {
        if (isCurrentlyTouched)
        {
            yield return new WaitForSeconds(stuckTimer);
            //Debug.Log("Unsticking");
            
            try
            {
                MeshRenderer mr = this.gameObject.GetComponent<MeshRenderer>();
                PolygonCollider2D pc = this.GetComponent<PolygonCollider2D>(); pc.enabled = false;
                if (pc != null)
                {
                    mr.enabled = false;
                    pc.enabled = false;
                }
            }
            catch (MissingComponentException)
            {
                SpriteRenderer sr = this.GetComponentInChildren<SpriteRenderer>();
                CircleCollider2D cc = this.GetComponent<CircleCollider2D>();
                sr.enabled = false;
                cc.enabled = false;
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        isCurrentlyTouched = false;
        StopAllCoroutines();
        StopCoroutine(checkBallStuck()); // Extra redundancy cause Idk why this is kind of working
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
