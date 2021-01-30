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
        createPlayers();
    }

    private void createPlayers()
    {
        createPlayer("Teesti Eesti", 10, 10, true, true);
        createPlayer("Arne Duck", 1, 1, false, false);
        createPlayer("Just Anotherplayer", 1, 2, false, false);
    }

    private void createPlayer(
        string name, 
        int startCoordX, 
        int startCoordZ, 
        bool startActive,
        bool startBall)
    {
        if (Players == null)
        {
            Players = new List<Player>();
        }
        
        Player newPlayer = new Player(name, startCoordX, startCoordZ, startActive, startBall);
        Players.Add(newPlayer);
    }
}
