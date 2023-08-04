using UnityEngine;
using UnityEngine.EventSystems;

public class MatchPlayerInput : SingletonMonoBehaviour<MatchPlayerInput>
{
    #region ==== FIELDS & PROPERTIES =========================================================

    #region ---- Mouse Position --------------------------------------------------------------
    public Vector3 MousePosition {get; private set;}
    public bool MouseOnPitch {get; private set;}

    private bool MouseOverUi = false;

    #endregion

    #region ---- Dependencies ----------------------------------------------------------------
    private MatchManager MatchManager;

    #endregion
    
    #endregion
    
    #region ==== GET DEPENDENCIES ============================================================
    private void getDependencies()
    {
        getSetMatchManager();
    }

    #region ---- Get/Set MatchManager (Get Instance and sets itself as ref on the same -------
    public void getSetMatchManager()
    {
        this.MatchManager = MatchManager.Instance;
        MatchManager.SetMatchPlayerInput();
    }
    #endregion

    #endregion
    
    #region ==== ON ENABLE ===================================================================
    private void OnEnable() 
    {
        getDependencies();
    }
    #endregion
    
    #region ==== UPDATE ======================================================================
    private void Update()
    {
        checkForMouseInput();
        checkForMousePosition();
        checkForMousePosOnPitch();
        checkForMouseOnUi();
    }

    #endregion
    
    #region ==== M O U S E  I N P U T ========================================================
    
    #region ---- Check for Mouse Input -------------------------------------------------------
    private void checkForMouseInput()
    {
        #region ---- Left click --------------------------------------------------------------
        if(Input.GetMouseButtonDown(0))
        {
            #region ---- Check for Ball position update ------
            MatchManager.Ball.CheckForBallPosUpdate();

            #endregion
            
            #region ---- Player Mode States ------
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
                case PlayerMode.Rotate:
                    //Trigger by OnMouseUp on each PitchTile
                    break; 
                case PlayerMode.Pass:
                    Debug.Log("Pass ball!");
                    if (MouseOnPitch)
                    {
                        MatchManager.Ball.ballMovement(
                            Ball.BallMove.PassGround, 
                            MatchManager.BallGrid.PointerPlateObj.transform,
                            activePlayer);
                    }
                    break;
            }

            #endregion
        }

        #endregion

        #region ---- Right click ------------------------------------------------------------*/
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
                case PlayerMode.Rotate:
                    OnMouseRightClickRotate(activePlayer);
                    break; 
                case PlayerMode.Pass:
                    OnMouseRightClickPass(activePlayer);
                    break;
            }
        }
        #endregion
    
        #region ---- Mouse Pointer on/off pitch ---------------------------------------------*/
        
        // --- On Pitch ---
        if (MouseOnPitch)
        {
            MatchPlayer activePlayer = MatchManager.MatchPlayerManager.CurrentActivePlayer;
            PlayerMode playerMode = activePlayer.PlayerMode;

            switch(playerMode) 
            {
                case PlayerMode.Idle:
                    Cursor.visible = true;
                    break; 
                
                case PlayerMode.Move:
                    Cursor.visible = true;
                    break; 

                case PlayerMode.Rotate:
                    Cursor.visible = true;
                    break; 
                
                case PlayerMode.Pass:
                    Cursor.visible = true;
                    break;
            }
        }

        // --- Off Picth ---
        if (!MouseOnPitch)
        {
            Cursor.visible = true;
        }

        #endregion

    }

    #endregion

    #region ---- Check for Mouse Position ----------------------------------------------------
    private void checkForMousePosition()
    {
        Plane plane=new Plane(Vector3.up,new Vector3(0, 0, 0));
        Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;
        
        if(plane.Raycast(ray, out distance)) 
        {
            MousePosition=ray.GetPoint(distance);
        }
    }

    private void checkForMousePosOnPitch()
    {
        if (MousePosition.x < MatchManager.PitchGrid.outLineNegX.x | 
            MousePosition.x > MatchManager.PitchGrid.outLinePosX.x | 
            MousePosition.z < MatchManager.PitchGrid.outLineNegZ.z |
            MousePosition.z > MatchManager.PitchGrid.outLinePosZ.z )
        {
            MouseOnPitch = false;
        }
        else
        {
            MouseOnPitch = true;
        }
    }

    private void checkForMouseOnUi()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            MouseOverUi = true;
        }
    }

    #endregion

    #region ---- Player MouseEnter / MouseExit -----------------------------------------------
    public void OnPlayerMouseEnter(MatchPlayer player)
    {
        player.PlayerHighlightOn();
    }

    public void OnPlayerMouseExit(MatchPlayer player)
    {
        player.PlayerHighlightOff();
    }

    #endregion
    
    #region ---- Player Mouse Left click -----------------------------------------------------
    public void OnPlayerLeftClick(MatchPlayer player)
    {
        player.SetPlayerActive();
    }

    #endregion
    
    #region ---- PitchTile MouseEnter / MouseExit --------------------------------------------
    public void OnPitchTileMouseEnter(PitchTile tile)
    {
        MatchPlayer activePlayer = MatchManager.MatchPlayerManager.CurrentActivePlayer;
        
        switch(activePlayer.PlayerMode) 
        {
            case PlayerMode.Idle:
                break; 
            case PlayerMode.Move:
                MatchManager.PitchGrid.PathFinding.DrawPathLine.Draw(tile);
                break; 
            case PlayerMode.Rotate:
                MatchManager.PitchGrid.RotateGrid.DrawRotationTarget(activePlayer, tile);
                break; 
            case PlayerMode.Pass:
                break;
        }
    }

    #endregion

    #region ---- PitchTile Mouse Left click --------------------------------------------------
    public void OnPitchTileLeftClick(PitchTile targetTile)
    {
        MatchPlayer activePlayer = MatchManager.MatchPlayerManager.CurrentActivePlayer;
        
        switch(activePlayer.PlayerMode) 
        {
            case PlayerMode.Idle:
                break; 
            case PlayerMode.Move:
                MatchManager.MatchPlayerManager.CurrentActivePlayer.PlayerActions.CheckForMovement(targetTile);
                break; 
            case PlayerMode.Rotate:
                OnMouseLeftClickRotate(activePlayer, targetTile);
                break; 
            case PlayerMode.Pass:
                OnMouseLeftClickPass(activePlayer, targetTile);
                break;
        } 
    }

    #endregion

    #endregion
    
    #region ==== P L A Y E R  M O D E  B U T T O N S =========================================
    
    #region ---- Select IDLE Mode ------------------------------------------------------------
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

    #endregion

    #region ---- Select MOVE Mode ------------------------------------------------------------
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

    #endregion

    #region ---- Select ROTATE Mode ----------------------------------------------------------
    public void OnBtnRotateMode()
    {
        MatchManager.MatchPlayerManager.CurrentActivePlayer.SetPlayerMode(PlayerMode.Rotate);
    }

    public void OnMouseLeftClickRotate(MatchPlayer player, PitchTile tile) 
    {
        MatchManager.MatchPlayerManager.CurrentActivePlayer.PlayerActions.CheckForRotation(tile);
    }

    public void OnMouseRightClickRotate(MatchPlayer player) 
    {
        player.SetPlayerMode(PlayerMode.Idle);
    }

    #endregion

    #region ---- Select PASS Mode ------------------------------------------------------------
    public void OnBtnPassMode()
    {
        MatchManager.MatchPlayerManager.CurrentActivePlayer.SetPlayerMode(PlayerMode.Pass);
    }

    public void OnMouseLeftClickPass(MatchPlayer player, PitchTile targetTile) 
    {
        //TODO: Remove the below code - Just for test...
        foreach (var ballPoint in targetTile.BallGridPoints)
        {
            Debug.Log(ballPoint.gameObject.name);
        }
    }

    public void OnMouseRightClickPass(MatchPlayer player) 
    {
        player.SetPlayerMode(PlayerMode.Idle);
    }

    #endregion

    #endregion
      
    #region ==== C A M E R A  C O N T R O L ==================================================
    
    #region ---- Rotate Main Camera ----------------------------------------------------------
    public void OnBtnRotateMainCameraLeft()
    {
        MatchManager.CameraManager.RotateLeft();
    }

    public void OnBtnRotateMainCameraRight()
    {
        MatchManager.CameraManager.RotateRight();
    }

    #endregion

    #endregion
    
    #region ==== G E N E R A L  U I ==========================================================

    #region ---- Back to Main Menu -----------------------------------------------------------
    public void OnBtnMainMenu()
    {
        MatchManager.GameManager.LoadMainMenu();
    }

    #endregion
    
    #region ---- Toggle Overlay Grid ---------------------------------------------------------
    public void OnBtnToggleGrid()
    {
        MatchManager.GridOverlay.ToggleOverlayGrid();
    }

    #endregion

    #endregion
    

}
