using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    public int ActionPoints {get; set;}

    public Stats()
    {
        ActionPoints = getRandom(4,8);
    }

    private int getRandom(int min, int max)
    {
        return Random.Range(min, max + 1);
    }





}
