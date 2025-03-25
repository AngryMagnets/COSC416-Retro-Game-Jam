using UnityEngine;

public class LayoutHandler : MonoBehaviour
{
    [SerializeField] public int orangeCount = 25;
    [SerializeField] public int greenCount = 2;
    [SerializeField] public int purpleCount = 1;
    private Peg[] pegs;
    private Peg purplePeg;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        pegs = GetComponentsInChildren<Peg>();
        int[] initialColorChanges = new int[orangeCount+greenCount+purpleCount];
        for (int i = 0; i<initialColorChanges.Length; i++)
        {
            do
            {
                initialColorChanges[i] = Random.Range(0, pegs.Length);
            } while (!checkUniqueValue(initialColorChanges, i, initialColorChanges[i]));
            if (i<orangeCount)
            {
                pegs[initialColorChanges[i]].UpdateColor('o');
            } else if (i<orangeCount+greenCount)
            {
                pegs[initialColorChanges[i]].UpdateColor('g');
            } else if (i<orangeCount+greenCount+purpleCount)
            {
                pegs[initialColorChanges[i]].UpdateColor('p');
                purplePeg = pegs[initialColorChanges[i]];
            }
        }
    }

    private bool checkUniqueValue(int[] array, int maxIndex, int newValue)
    {
        for (int  i = 0; i<maxIndex; i++)
        {
            if (array[i] == newValue) return false;
        }
        return true;
    }

    public void UpdatePurplePeg()
    {
        if (purplePeg!=null)
        {
            purplePeg.UpdateColor('b');
        }
        int i;
        do
        {
            i = Random.Range(0, pegs.Length);
        } while (!checkForBluePeg(i));
        pegs[i].UpdateColor('p');
    }

    private bool checkForBluePeg(int i)
    {
        if (pegs[i] != null)
        {
            if (pegs[i].GetType() == 'b') return true;
        }
        return false;
    }
}
