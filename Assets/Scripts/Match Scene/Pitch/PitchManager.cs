﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchManager : SingletonScriptableObject<PitchManager>
{
    
    /* #region ==== FIELDS & PROPERTIES ======================================================= */
   
    /* #region ---- Dependencies -------------------------------------------------------------- */
    private MatchManager MatchManager;

    /* #endregion */
    
    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== GET DEPENDENCIES ========================================================== */
    private void getDependencies()
    {
        MatchManager = MatchManager.Instance;
    }

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== ON ENABLE ================================================================= */
    void OnEnable() 
    {
        getDependencies();
    }
    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== GENERAL HELPERS =========================================================== */

    /* #region ---- Get a single PitchTile script reference ----------------------------------- */
    public PitchTile GetPitchTile(int x, int z) 
    {
        GameObject pitchTile = MatchManager.PitchGrid.PitchTiles[x,z];
        return pitchTile.GetComponent<PitchTile>();
    }
    /* #endregion */

    /* #region ---- Set a pitchTile instance to occupied (by player) -------------------------- */
    public void setPitchTileOccupied(PitchTile pitchTile, MatchPlayer player) 
    {
       pitchTile.setOccupied(player);
    }
    /* #endregion */

    /* #region ---- Set a pitchTile instance to unoccupied ------------------------------------ */
    public void setPitchTileUnOccupied(PitchTile pitchTile) 
    {
       pitchTile.setUnOccupied();
    }
    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */

}
