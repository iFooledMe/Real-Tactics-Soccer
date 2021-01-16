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
    public Stats Stats {get; set;}
    public bool startActive {get; private set;}

    public Player(string name, int startCoordX, int startCoordZ, bool startActive)
    {
        setStartPos(startCoordX, startCoordZ);
        this.Name = name;
        this.startActive = startActive;
        setStats();
    }

    private void setStats()
    {
        this.Stats = new Stats();
    }

    private void setStartPos(int coordX, int coordZ)
    {
        startPosition = new Dictionary<StartPos, int>();
        this.startPosition.Add(StartPos.X, coordX);
        this.startPosition.Add(StartPos.Z, coordZ);
    }

}
