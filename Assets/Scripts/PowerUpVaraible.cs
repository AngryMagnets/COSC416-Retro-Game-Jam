using System.Collections.Generic;
using UnityEngine;

public static class PowerUpVaraible
{
    /// <summary>
    /// Dictionary <c>powers</c> List of powers indexed by enum <paramref name="perks"></paramref> <br/>
    /// perks.origin = 0 for multiball, 1 for fireball
    /// </summary>
    public static Dictionary<perks, int> powers { get; private set; } = new Dictionary<perks, int>();
    public static float MultiballDelay { get; private set; } = 0.32f;

    /// <summary>
    /// Static Method <c>InitializePowers</c> Initializes <paramref name="powers"/>. <br/>
    /// <paramref name="power"/> = 0 for Multiball, <paramref name="power"/> = 1 for Fireball
    /// </summary>
    /// <param name="power"></param>
    public static void InitializePowers(int power)
    {
        powers.Add(perks.origin, power);
        if (power == 0) //Multiball
        {
            powers.Add(perks.multiCount, 1);
            powers.Add(perks.exploding, 0);
            powers.Add(perks.bonusFree, 0);
            powers.Add(perks.spooky, 0);
        }
        else if (power == 1)    //Fireball
        {
            powers.Add(perks.fireCover, 0);
            powers.Add(perks.fireWisp, 0);
            powers.Add(perks.wispInterval, 0);
            powers.Add(perks.wispDur, 1);
            powers.Add(perks.wispExplode, 0);
            powers.Add(perks.fireballSize, 0);
        }
    }

    public static void IncreasePerk(perks p)
    {
        powers[p] += 1;
    }
}

public enum perks
{
    origin = 0,
    multiCount = 1,
    exploding = 2,
    bonusFree = 3,
    spooky = 4,
    fireCover = 1,
    fireWisp = 2,
    wispInterval = 3,
    wispDur = 4,
    wispExplode = 5,
    fireballSize = 6
}