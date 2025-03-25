using UnityEngine;
using UnityEngine.Events;

public class Peg : MonoBehaviour
{
    public UnityEvent<char> PegHit;

    [SerializeField] private char type = 'b'; //'b' blue, 'o' orange, 'g' green, 'p' purple
    [SerializeField] private Color[] colors = new Color[4];
    private bool notHit = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (notHit && collision.gameObject.CompareTag("Ball"))
        {
            PegHit?.Invoke(type);
            Debug.Log(type + " Hit");
            notHit = false;
            //flash color
            //flashSprite.enable(); ??? as a child to each peg prefab, can edit
        }
    }

    public char GetType()
    {
        return type;
    }

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
