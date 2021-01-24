using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    public int MaxActionPoints {get; set;}

    public Stats()
    {
        MaxActionPoints = 5;//getRandom(4,8);
    }

    private int getRandom(int min, int max)
    {
        return Random.Range(min, max + 1);
    }

}
