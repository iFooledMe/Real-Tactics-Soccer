using System.Collections.Generic;
using UnityEngine;

public class RotateGrid
{

    MatchManager MatchManager;

    public RotateGrid(MatchManager MatchManager)
    {
        this.MatchManager = MatchManager;
    }

    public void DrawRotationGrid()
    {
        MatchPlayer Player = MatchManager.MatchPlayerManager.CurrentActivePlayer;

        if (Player.PlayerMode == PlayerMode.Rotate)
        {
            Debug.Log("RotateGrid.DrawRotationGrid initiated");
        }
    }

}
