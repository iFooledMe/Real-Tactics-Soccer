using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team
{   
    public string Name {get; private set;}
    public int Id {get; private set;}
    public List<Player> Players {get; private set;}
    
    public Team(string teamName)
    {
        this.Name = teamName;
        getTeamId(GameManager.Instance);
    }

    private void getTeamId(GameManager gameManager)
    {
        Id = gameManager.Teams.Count + 1;
        Debug.Log($"Team {Name} with Id {Id} created");
    }

    private void createPlayers()
    {

    }

}
