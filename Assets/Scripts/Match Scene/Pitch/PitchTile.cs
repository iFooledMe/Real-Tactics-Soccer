using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchTile : MonoBehaviour
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */
    
    /* #region ---- Coordinates / Position ---------------------------------------------------- */
    public int CoordX { get; private set; }
    public int CoordZ { get; private set; }

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #region ---- Dependencies -------------------------------------------------------------- */
    private MatchManager MatchManager;

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== GET DEPENDENCIES ========================================================== */
    private void getDependencies()
    {
        getMatchManager();
    }

    /* #region ---- Get MatchManager ---------------------------------------------------------- */
    public void getMatchManager()
    {
        MatchManager = MatchManager.Instance;
    }
    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== AWAKE / START ============================================================= */
    void Awake() 
    {
        getDependencies();
    }
    
    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== GENERAL HELPER FUNCTIONS ================================================== */
    
    /* #region ---- Helper - Set this tiles coordinates in the grid --------------------------- */
    public void SetCoodinates(int coordX, int coordZ) 
    {
        this.CoordX = coordX;
        this.CoordZ = coordZ;
    }
    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #endregion */
    /* ======================================================================================== */

}
