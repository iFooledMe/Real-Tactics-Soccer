using System.Collections.Generic;
using UnityEngine;

public class RotateGrid
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */
    
    /* #region ---- Dependencies -------------------------------------------------------------- */
    private MatchManager MatchManager;

    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== CONSTRUCTOR =============================================================== */
    public RotateGrid(MatchManager MatchManager)
    {
        this.MatchManager = MatchManager;
    }

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== D R A W   R O T A T I O N  G R I D (Around the active player) ============= */
    
    public void DrawRotationGrid(MatchPlayer Player)
    {
        if (Player.PlayerMode == PlayerMode.Rotate && 
            Player.PlayerActions.RotationCounter < MatchManager.MatchPlayerManager.ActionsApCostSettings.MaxRotationsPerTurn)
        {
            ClearAll();
            
            foreach (var tile in Player.CurrentTile.NeighbourTiles)
            {
                tile.ActivateRotateGridOverlay(true);
            }
        }
        else
        {
            MatchManager.Hud.UpdateGameMessage(MatchManager.Hud.Messages.NoMoreRotations);
        }
    }

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== D R A W  /  C L E A R  R O T A T I O N  T A R G E T  O V E R L A Y ======== */
    
    /* #region ---- Draw Rotation Target Overlay ---------------------------------------------- */
    public void DrawRotationTarget(MatchPlayer Player, PitchTile targetTile)
    {
        if (Player.PlayerMode == PlayerMode.Rotate)
        {
            ClearRotationTarget(Player);
            
            List<PitchTile> neighbourTiles = Player.CurrentTile.NeighbourTiles;
            
            foreach (var tile in neighbourTiles)
            {
                if (tile == targetTile)
                {
                    tile.ActivateRotateTargetOverlay(true);
                    int direction = Player.GetRotationIndicator(targetTile.transform);
                    int apCost = Player.CalcRotationApCost(direction);
                    MatchManager.Hud.UpdateAccAPCost(apCost);
                    break;
                }
            }
        }
    }

    /* #endregion ----------------------------------------------------------------------------- */

    /* #region ---- Clear Rotation Target Overlay --------------------------------------------- */
    public void ClearRotationTarget(MatchPlayer Player)
    {
        List<PitchTile> neighbourTiles = Player.CurrentTile.NeighbourTiles;
        
        foreach (var tile in neighbourTiles)
        {
            tile.ActivateRotateTargetOverlay(false);
        }
    }

    /* #endregion ----------------------------------------------------------------------------- */

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== C L E A R  A L L ========================================================== */
    public void ClearAll()
    {
        foreach (var player in MatchManager.MatchPlayerManager.MatchPlayersList)
        {
            foreach(var tile in player.CurrentTile.NeighbourTiles)
            {
                tile.ActivateRotateGridOverlay(false);
                tile.ActivateRotateTargetOverlay(false);
            }
        }
    }

    /* #endregion */
    /* ======================================================================================== */


}
