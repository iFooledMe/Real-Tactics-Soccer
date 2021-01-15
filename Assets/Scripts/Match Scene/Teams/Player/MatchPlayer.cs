using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPlayer : MonoBehaviour
{
    public Player Player {get; private set;}

    [SerializeField]
    private string playerName;

    [SerializeField]
    private int actionPoints;
    
    public int CoordX {get; private set;}
    public int CoordZ {get; private set;}

    public void SetPlayer(Player player)
    {
        this.Player = player;
        playerName = Player.Name;
        actionPoints = Player.Stats.ActionPoints;
    }

    public void SetCoordinates(int coordX, int coordZ)
    {
        this.CoordX = coordX;
        this.CoordZ = coordZ;
    }









}
