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

    /* #region ==== UPDATE ==================================================================== */
    private void Update()
    {
        checkForMouseInput();
    }

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== G A M E  C O N T R O L S ================================================== */
    
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

    /* #region ---- IDLE Mode Controls -------------------------------------------------------- */
    public void OnBtnIdleMode()
    {
        MatchManager.MatchPlayerManager.CurrentActivePlayer.SetPlayerMode(PlayerMode.Idle);
    }

    public void OnMouseLeftClickIdle(MatchPlayer player) 
    {
        return;
    }

    public void OnMouseRightClickIdle(MatchPlayer player) 
    {
        return;
    }

    /* #endregion */

    /* #region ---- MOVE Mode Controls -------------------------------------------------------- */
    public void OnBtnMoveMode()
    {
        MatchManager.MatchPlayerManager.CurrentActivePlayer.SetPlayerMode(PlayerMode.Move);
    }

    public void OnMouseLeftClickMove(MatchPlayer player) 
    {
        return;
        //Move controll is triggered on tile click
    }

    public void OnMouseRightClickMove(MatchPlayer player) 
    {
        player.SetPlayerMode(PlayerMode.Idle);
    }

    /* #endregion */

    /* #region ---- PASS Mode Controls -------------------------------------------------------- */
    public void OnBtnPassMode()
    {
        MatchManager.MatchPlayerManager.CurrentActivePlayer.SetPlayerMode(PlayerMode.Pass);
    }

    public void OnMouseLeftClickPass(MatchPlayer player) 
    {
        Debug.Log("Pass Action Ordered");
    }

    public void OnMouseRightClickPass(MatchPlayer player) 
    {
        player.SetPlayerMode(PlayerMode.Idle);
    }

    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */  

    /* #region ==== M O U S E  C O N T R O L ================================================== */
    private void checkForMouseInput()
    {
        /* #region ---- Left click ------------------------------------------------------------*/
        if(Input.GetMouseButtonDown(0))
        {
            MatchPlayer activePlayer = MatchManager.MatchPlayerManager.CurrentActivePlayer;
            PlayerMode playerMode = activePlayer.PlayerMode;
            
            switch(playerMode) 
            {
                case PlayerMode.Idle:
                    OnMouseLeftClickIdle(activePlayer);
                    break; 
                case PlayerMode.Move:
                    OnMouseLeftClickMove(activePlayer);
                    break; 
                case PlayerMode.Pass:
                    OnMouseLeftClickPass(activePlayer);
                    break;
            }
        }
        /* #endregion */

        /* #region ---- Right click ------------------------------------------------------------*/
        if(Input.GetMouseButtonDown(1))
        {
            MatchPlayer activePlayer = MatchManager.MatchPlayerManager.CurrentActivePlayer;
            PlayerMode playerMode = activePlayer.PlayerMode;
            
            switch(playerMode) 
            {
                case PlayerMode.Idle:
                    OnMouseRightClickIdle(activePlayer);
                    break; 
                case PlayerMode.Move:
                    OnMouseRightClickMove(activePlayer);
                    break; 
                case PlayerMode.Pass:
                    OnMouseRightClickPass(activePlayer);
                    break;
            }
        }
        /* #endregion */

    }
    
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
