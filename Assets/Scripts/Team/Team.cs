using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team
{   
    public string Name {get; private set;}
    public List<Player> Players {get; private set;}
    
    public Team(string teamName)
    {
        this.Name = teamName;
        Debug.Log($"Team {Name} created");
        createPlayers();
    }

    private void createPlayers()
    {
        createPlayer("Teesti Eesti", 1, 1);
    }

    private void createPlayer(string name, int startCoordX, int startCoordZ)
    {
        if (Players == null)
        {
            Players = new List<Player>();
        }
        
        Player newPlayer = new Player(name, startCoordX, startCoordZ);
        Players.Add(newPlayer);
    }

}
