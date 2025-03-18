using UnityEngine;
using UnityEngine.Events;

public class Peg : MonoBehaviour
{
    public UnityEvent<char> PegHit;

    [SerializeField] private char type = 'b'; //'b' blue, 'o' orange, 'g' green, 'p' purple
    private bool notHit = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (notHit)
        {
            PegHit?.Invoke(type);
            Debug.Log(type + " Hit");
            notHit = false;
            //flash color
        }
    }
}
