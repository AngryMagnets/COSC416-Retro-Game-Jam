using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PerkSelection : MonoBehaviour
{
    [SerializeField] private string[] perkButtonNames;
    [SerializeField] private GameObject[] perkButtons;
    [SerializeField] private int[] perkButtonsIndex;

    public void GeneratePerkButtons()
    {
        Debug.Log("Going");
        List<string> availablePerks = new List<string>();
        switch(PowerUpVaraible.powers[perks.origin])
        {
            case 0: //Multiball
                availablePerks.Add("multiCount");
                availablePerks.Add("bonusFree");
                if (PowerUpVaraible.powers[perks.spooky] == 0) availablePerks.Add("spooky");
                else availablePerks.Add("spooky2");
                if (PowerUpVaraible.powers[perks.exploding] != 0) availablePerks.Add("exploding");
                else availablePerks.Add("exploding2");
                break;
            case 1: //Fireball
                availablePerks.Add("fireballSize");
                /*if (PowerUpVaraible.powers[perks.fireWisp] != 0)
                {
                    availablePerks.Add("wispDur");
                    availablePerks.Add("wispInterval");
                    if (PowerUpVaraible.powers[perks.wispExplode] != 0) availablePerks.Add("wispExplode");
                    else availablePerks.Add("wispExplode2");
                } else availablePerks.Add("fireWisp");
                if (PowerUpVaraible.powers[perks.fireCover] == 0) availablePerks.Add("fireCover");*/
                break;
        }
        int i = Random.Range(0, availablePerks.Count);
        string perk1 = availablePerks[i];
        availablePerks.RemoveAt(Random.Range(0,availablePerks.Count));

        i = Random.Range(0, availablePerks.Count);
        string perk2 = availablePerks[i];
        availablePerks.RemoveAt(Random.Range(0, availablePerks.Count));

        int iPerk1 = getButtonIndexFromName(perk1);
        GameObject gPerk1 = Instantiate(perkButtons[iPerk1], transform.position, transform.rotation);
        gPerk1.transform.parent = transform;
        gPerk1.GetComponent<RectTransform>().position = new Vector3(-90,200,0);
        gPerk1.GetComponent<Button>().onClick?.AddListener(() => IncreasePerk(perkButtonsIndex[iPerk1]));

        int iPerk2 = getButtonIndexFromName(perk2);
        GameObject gPerk2 = Instantiate(perkButtons[iPerk2], transform.position, transform.rotation);
        gPerk2.transform.parent = transform;
        gPerk1.GetComponent<RectTransform>().position = new Vector3(90, 200, 0);
        gPerk1.GetComponent<Button>().onClick?.AddListener(() => IncreasePerk(perkButtonsIndex[iPerk2]));
    }

    public void IncreasePerk(int p)
    {
        PowerUpVaraible.IncreasePerk((perks)p);
        GameManager.game.LoadNextLevel();
    }

    private int getButtonIndexFromName(string name)
    {
        for (int i = 0; i<perkButtonNames.Length;i++)
        {
            if (name.Equals(perkButtonNames[i])) return i;
        }
        return 0;
    }
}