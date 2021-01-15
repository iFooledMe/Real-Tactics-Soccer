using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public enum StartPos
    {
        X,
        Z
    }
    
    public string Name {get; private set;}
    public Dictionary<StartPos, int> startPosition {get; private set;}

    public Player(string name, int startCoordX, int startCoordZ)
    {
        startPosition = new Dictionary<StartPos, int>();
        this.Name = name;
        this.startPosition.Add(StartPos.X, startCoordX);
        this.startPosition.Add(StartPos.Z, startCoordZ);

        Debug.Log($"Player {Name} created");
    }
}
