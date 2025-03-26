using UnityEngine;
using UnityEngine.Events;

public class Peg : MonoBehaviour
{
    public UnityEvent<char> PegHit; //Message with color of peg being hit

    public char type { get; private set; } = 'b';  //'b' blue, 'o' orange, 'g' green, 'p' purple 
    [SerializeField] private Color[] colors = new Color[4]; //Blue, orange, green, and purple colors
    private bool notHit = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (notHit && collision.gameObject.CompareTag("Ball"))
        {
            PegHit?.Invoke(type);   //Sends Message with color of peg that this was hit
            notHit = false;
            GameManager.game.TouchPeg(this);
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
