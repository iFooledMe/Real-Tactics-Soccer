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
        createPlayer("Teesti Eesti", 1, 1, true);
        createPlayer("Arne Duck", 5, 5, false);
        createPlayer("Just Anotherplayer", 5, 10, false);
    }

    private void createPlayer(string name, int startCoordX, int startCoordZ, bool startActive)
    {
        if (Players == null)
        {
            Players = new List<Player>();
        }
        
        Player newPlayer = new Player(name, startCoordX, startCoordZ, startActive);
        Players.Add(newPlayer);
    }
}
