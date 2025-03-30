using UnityEngine;

/// <summary>
/// MonoBehaviour <c>LayoutHandler</c> Object contains a peg layout and handles peg color setting
/// </summary>
public class LayoutHandler : MonoBehaviour
{
    [SerializeField] public int orangeCount = 25;
    [SerializeField] public int greenCount = 2;
    [SerializeField] public int purpleCount = 1;
    private Peg[] pegs; //Contains all the pegs
    private Peg purplePeg;  //Holds the purple peg

    // Sets the initial set of colored pegs
    void OnEnable()
    {
        pegs = GetComponentsInChildren<Peg>();
        Debug.Log(pegs.Length);
        int[] initialColorChanges = new int[orangeCount+greenCount+purpleCount];    //Gets a set of indexes to set as colored pegs
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

    /// <summary>
    /// Method <c>checkUniqueValue</c> Checks that <paramref name="array"/> doesn't contain the <paramref name="newValue"/> up to <paramref name="maxIndex"/>
    /// </summary>
    /// <param name="array"></param>
    /// <param name="maxIndex"></param>
    /// <param name="newValue"></param>
    /// <returns></returns>
    private bool checkUniqueValue(int[] array, int maxIndex, int newValue)
    {
        for (int  i = 0; i<maxIndex; i++)
        {
            if (array[i] == newValue) return false;
        }
        return true;
    }

    /// <summary>
    /// Method <c>UpdatePurplePeg</c> Makes the purple peg blue (if it exists) and sets a new purple peg (of the blue pegs)
    /// </summary>
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

    /// <summary>
    /// Method <c>checkForBluePeg</c> Ensures index in list of pegs isn't null and is blue
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    private bool checkForBluePeg(int i)
    {
        if (pegs[i] != null)
        {
            if (pegs[i].GetColor() == 'b') return true;
        }
        return false;
    }
}
