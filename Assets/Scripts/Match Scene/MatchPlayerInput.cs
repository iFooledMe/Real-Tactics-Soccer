using System;
using UnityEngine;

public class MatchPlayerInput : SingletonMonoBehaviour<MatchPlayerInput>
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
        getSetMatchManager();
    }

    /* #region ---- Get/Set MatchManager (Get Instance and sets itself as ref on the same ----- */
    public void getSetMatchManager()
    {
        this.MatchManager = MatchManager.Instance;
        MatchManager.SetMatchPlayerInput();
    }
    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== ON ENABLE ================================================================= */
    private void OnEnable() 
    {
        getDependencies();
    }
    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== P L A Y E R  E V E N T S ================================================== */
    
    /* #region ---- Player MouseEnter / MouseExit --------------------------------------------- */
    public void OnPlayerMouseEnter(MatchPlayer player)
    {
        player.PlayerHighlightOn();
    }

    public void OnPlayerMouseExit(MatchPlayer player)
    {
        player.PlayerHighlightOff();
    }

    /* #endregion */

    /* #region ---- Player Mouse Left click --------------------------------------------------- */
    public void OnPlayerLeftClick(MatchPlayer player)
    {
        player.SetPlayerActive();
    }

    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */  

    /* #region ==== P I T C H  T I L E  E V E N T S =========================================== */
    
    /* #region ---- PitchTile MouseEnter / MouseExit ------------------------------------------ */
    public void OnPitchTileMouseEnter(PitchTile tile)
    {
        MatchManager.PitchGrid.PathFinding.DrawPathLine.Draw(tile);
    }

    /* #endregion */

    /* #region ---- PitchTile Mouse Left click ------------------------------------------------ */
    public void OnPitchTileLeftClick(PitchTile targetTile)
    {
        MatchManager.MatchPlayerManager.CurrentActivePlayer.PlayerActions.CheckForMovement(targetTile);
    }

    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */  

    /* #region ==== C A M E R A  C O N T R O L ================================================ */
    
    /* #region ---- Rotate Main Camera -------------------------------------------------------- */
    public void OnBtnRotateMainCameraLeft()
    {
        MatchManager.CameraManager.RotateLeft();
    }

    public void OnBtnRotateMainCameraRight()
    {
        MatchManager.CameraManager.RotateRight();
    }

    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== G E N E R A L  U I ======================================================== */

    /* #region ---- Back to Main Menu --------------------------------------------------------- */
    public void OnBtnMainMenu()
    {
        MatchManager.GameManager.LoadMainMenu();
    }

    /* #endregion */
    
    /* #region ---- Toggle Overlay Grid ------------------------------------------------------- */
    public void OnBtnToggleGrid()
    {
        MatchManager.GridOverlay.ToggleOverlayGrid();
    }

    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */

}
