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

    /* #region ==== D R A W / C L E A R  R O T A T I O N  G R I D (Around the active player) == */
    
    /* #region ---- Draw Rotation Grid -------------------------------------------------------- */
    public void DrawRotationGrid(MatchPlayer Player)
    {
        if (Player.PlayerMode == PlayerMode.Rotate)
        {
            ClearRotationGrid();
            List<PitchTile> neighbourTiles = Player.CurrentTile.NeighbourTiles;

            foreach (var tile in neighbourTiles)
            {
                tile.ActivateRotateGridOverlay(true);
            }
        }
    }

    /* #endregion ----------------------------------------------------------------------------- */

    /* #region ---- Clear Rotation Grid ------------------------------------------------------- */
    public void ClearRotationGrid()
    {
        foreach (var tile in MatchManager.PitchGrid.PitchTilesList)
        {
            tile.ActivateRotateGridOverlay(false);
        }
    }

    /* #endregion ----------------------------------------------------------------------------- */

    /* #endregion */
    /* ======================================================================================== */




}
