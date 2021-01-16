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

    public void SetPlayerInfo(Player player, int coordX, int coordZ)
    {
        CoordX = coordX;
        CoordZ = coordZ;
        Player = player;
        playerName = Player.Name;
        actionPoints = Player.Stats.ActionPoints;
    }









}
