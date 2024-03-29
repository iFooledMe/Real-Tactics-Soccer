﻿using System;
using System.Collections.Generic;
using UnityEngine;

    #region ==== ENUMS ========================================================================
    public enum PlayerMode
    {
        Idle,
        Move,
        Rotate,
        Pass
    }

    public enum PlayerStat
    {
        ActionPoints
    }

    //TODO: Move this enum to a general Enums file
    public enum ValueSign
    {
        Positive,
        Negative
    }

    #endregion
    

public class MatchPlayer : MonoBehaviour
{
    #region ==== FIELDS & PROPERTIES ==========================================================
    
    #region ---- Player Info ------------------------------------------------------------------
    public Player Player {get; private set;}

    public string Name {get; private set;}

    public int CoordX {get; private set;}
    public int CoordZ {get; private set;}

    

    #endregion

    #region ---- Player Stats -----------------------------------------------------------------
    public int MaxActionPoints {get; private set;}
    public int CurrentActionPoints;

    #endregion

    #region ---- Player States ----------------------------------------------------------------
    public PitchTile CurrentTile {get; private set;}

    public bool IsActive;
    public bool IsBallHolder;
    private bool updateBallPosition = true; 

    private Color defaultColor = Color.green;
    private Color highLightColor = Color.blue;
    private Color activeColor = Color.red;

    public PlayerMode PlayerMode = PlayerMode.Idle;

    public int currentAngle {get; private set;}
    private int fullRotation0;
    private int fullRotation1;
    private int fullRotation2;
    private int halfRotation1;
    private int halfRotation2;


    #endregion
    
    #region ---- Components -------------------------------------------------------------------
    private Renderer bodyRenderer;
    private GameObject directionIndicator;

    #endregion

    #region ---- Dependencies -----------------------------------------------------------------
    private MatchManager MatchManager;
    public PlayerActions PlayerActions {get; private set;}

    #endregion
    
    #endregion
    
    #region ==== GET DEPENDENCIES & COMPONENTS ================================================
    private void getDependencies()
    {
        getMatchManager();
        setPlayerActions();
    }

    private void getComponents()
    {
        getBodyRenderer();
        getDirectionIndicator();
    }

    #region ---- Get MatchManager -------------------------------------------------------------
    public void getMatchManager()
    {
        MatchManager = MatchManager.Instance;
    }
    #endregion

    #region ---- Get Renderer Component -------------------------------------------------------
    public void getBodyRenderer()
    {
        GameObject playerBody = this.transform.Find("PlayerBody").gameObject;
        bodyRenderer = playerBody.GetComponent<Renderer>();
    }
    #endregion

    #region ---- Get Renderer Component -------------------------------------------------------
    public void getDirectionIndicator()
    {
        directionIndicator = this.transform.Find("Direction").gameObject;
    }
    #endregion

    #region ---- Set PlayerActions ------------------------------------------------------------
    public void setPlayerActions()
    {
        this.PlayerActions = new PlayerActions(this);
    }

    #endregion

    #endregion
    
    #region ==== AWAKE / START ================================================================
    private void Awake() 
    {
        getDependencies();
        getComponents();
    }

    private void Start()
    {
        UpdateRotationAngle ((int)this.gameObject.transform.eulerAngles.y);
    }

    #endregion
    
    #region ==== UPDATE =======================================================================
    void Update() 
    {
        PlayerActions.ActionSelector(PlayerActions.CurrentAction);
        checkForZeroActionPoints();
        MatchManager.Ball.CheckForBallPosUpdate();

    }

    #endregion
     
    //TODO: Move each Player Mode to it's own sub-class???
    #region ==== P L A Y E R  M O D E S  ======================================================
    
    #region ---- Player Mode SWITCHER ---------------------------------------------------------
    public void SetPlayerMode(PlayerMode playerMode)
    {
        if (!MatchManager.MatchPlayerManager.PlayInAction)
        {
            clearAllModeStates();

            switch(playerMode) 
            {
            case PlayerMode.Idle:
                setIdleMode();
                break;
            case PlayerMode.Move:
                setMoveMode();
                break;
            case PlayerMode.Rotate:
                setRotateMode();
                break;
            case PlayerMode.Pass:
                setPassMode();
                break;
            default:
                setIdleMode();
                break;
            }
        }
    }

    #endregion

    #region ---- IDLE Mode --------------------------------------------------------------------
    private void setIdleMode()
    {
        PlayerMode = PlayerMode.Idle;
    }

    #endregion

    #region ---- MOVE Mode --------------------------------------------------------------------
    private void setMoveMode()
    {
        PlayerMode = PlayerMode.Move;
    }

    #endregion

    #region ---- ROTATE Mode ------------------------------------------------------------------
    private void setRotateMode()
    {
        PlayerMode = PlayerMode.Rotate;
        MatchManager.PitchGrid.RotateGrid.DrawRotationGrid(this);
    }

    #endregion

    #region ---- PASS Mode --------------------------------------------------------------------
    private void setPassMode()
    {
        if (PlayerMode == PlayerMode.Pass)
        {
            setIdleMode();
        }
        else if (IsBallHolder)
        {
            PlayerMode = PlayerMode.Pass;
            MatchManager.BallGrid.ActivateBallGrid();
        }
    }

    #endregion

    #region ---- Clear all Mode States --------------------------------------------------------
    private void clearAllModeStates()
    {
        clearMovePaths();
        deactivateBallGrid();
        clearRotationGrid();
        clearMoveTargetOverlay();
        resetHudElements();
    }
    
    //Move Paths -----------------
    private void clearMovePaths()
    {
        MatchManager.DestroyObjectsByTag("PathLine");
    }

    //BallGrid ------------------
    private void deactivateBallGrid()
    {
        MatchManager.BallGrid.DeactivateBallGrid();
    }

    //Rotation Grid ------------
    private void clearRotationGrid()
    {
        if (MatchManager.PitchGrid.RotateGrid != null)
        {
            MatchManager.PitchGrid.RotateGrid.ClearAll();
        }
    }

    //MoveTargetOverlay --------
    private void clearMoveTargetOverlay()
    {
        MatchManager.PitchGrid.DeactivateMoveTargetOverlay();
    }


    //Reset HUD-elements
    private void resetHudElements()
    {
        MatchManager.Hud.ResetGameMessage();
        MatchManager.Hud.UpdateApCostToString("");
    }

    #endregion
    
    #endregion
    
    #region ==== U P D A T E  P L A Y E R  S T A T S ==========================================
    
    #region ---- General Update Player Stat Method --------------------------------------------
    public void UpdateStat(PlayerStat stat, int newValue, ValueSign valueSign)
    {
        switch(stat) 
        {
            case PlayerStat.ActionPoints:
                updateActionPoints(newValue, valueSign);
                break;
        }
    }

    #endregion 

    #region ---- Action Points ----------------------------------------------------------------
    private void updateActionPoints(int updateValue, ValueSign valueSign)
    {
        if (valueSign == ValueSign.Negative)
        {
            updateValue = -updateValue;
        }
        
        if(updateValue <= MaxActionPoints) CurrentActionPoints += updateValue;
        if(updateValue > MaxActionPoints) CurrentActionPoints = MaxActionPoints;
    }

    private void checkForZeroActionPoints()
    {
        if (CurrentActionPoints <= 0 && 
            MatchManager.MatchPlayerManager.CurrentActivePlayer == this)
        {
            SetPlayerMode(PlayerMode.Idle);
            MatchManager.Hud.UpdateGameMessage(MatchManager.Hud.Messages.NoActionPoints);
        }
    }

    #endregion 

    #endregion
    
    #region ==== P L A Y E R  R O T A T I O N  A N G L E ======================================

    #region ---- Get/Set rotation indicator (empty object LookAt a specified target) ----------
    public int GetRotationIndicator(Transform target)
    {
        this.directionIndicator.transform.LookAt(target);
        return (int)directionIndicator.transform.eulerAngles.y;
    }

    public void ResetRotationIndicator()
    {
        this.directionIndicator.transform.eulerAngles = this.transform.eulerAngles;
    }

    #endregion --------------------------------------------------------------------------------
    
    #region ---- Player Rotation to face a specified target -----------------------------------
    public void FaceTarget(Transform target)
    {
        this.transform.LookAt(target);
    }

    #endregion --------------------------------------------------------------------------------

    #region ---- Update RotationAngle ---------------------------------------------------------
    public void UpdateRotationAngle (int angleY)
    {
        int angleX = (int)this.gameObject.transform.eulerAngles.x;
        int angleZ = (int)this.gameObject.transform.eulerAngles.z;
        this.gameObject.transform.eulerAngles = new Vector3(angleX, angleY, angleZ);
        this.currentAngle = angleY;
        setRotationLimits();
    }

    //TODO: SetRotationLimits(9 work but could propably be done a bit smarter with less code)
    // --- Set RotationLimits ---
    private void setRotationLimits()
    {
        if (currentAngle == 0)
        {
                fullRotation0 = 180;
                fullRotation1 = 135;
                fullRotation2 = 225;

                halfRotation1 = 90;
                halfRotation2 = 270;
        }
        else if (currentAngle == 45)
        {
                fullRotation0 = 180;
                fullRotation1 = 225;
                fullRotation2 = 270;

                halfRotation1 = 135;
                halfRotation2 = 315;
        }
        else if (currentAngle == 90)
        {
                fullRotation0 = 225;
                fullRotation1 = 270;
                fullRotation2 = 315;

                halfRotation1 = 0;
                halfRotation2 = 180;
        }
        else if (currentAngle == 135)
        {
                fullRotation2 = 0;
                fullRotation0 = 270;
                fullRotation1 = 315;
                
                halfRotation1 = 45;
                halfRotation2 = 225;
        }
        else if (currentAngle == 180)
        {
                fullRotation2 = 0;
                fullRotation0 = 45;
                fullRotation1 = 315;
                
                halfRotation1 = 90;
                halfRotation2 = 270;
        }
        else if (currentAngle == 225)
        {
                fullRotation2 = 0;
                fullRotation0 = 45;
                fullRotation1 = 90;
                
                halfRotation1 = 135;
                halfRotation2 = 315;
        }
        else if (currentAngle == 270)
        {
                fullRotation2 = 45;
                fullRotation0 = 90;
                fullRotation1 = 135;
                
                halfRotation1 = 0;
                halfRotation2 = 180;
        }
        else if (currentAngle == 315)
        {
                fullRotation2 = 90;
                fullRotation0 = 135;
                fullRotation1 = 180;
                
                halfRotation1 = 45;
                halfRotation2 = 225;
        }
    }

    #endregion --------------------------------------------------------------------------------

    #region ---- Calculate AP Cost for rotation -----------------------------------------------
    public int CalcRotationApCost (int angleY)
    {
        if (angleY == fullRotation0 || angleY == fullRotation1 || angleY == fullRotation2)
        {
            int apCost = MatchManager.MatchPlayerManager.ActionsApCostSettings.CostRotateFull;
            return apCost;
        }
        else if (angleY == halfRotation1 || angleY == halfRotation2)
        {
            int apCost = MatchManager.MatchPlayerManager.ActionsApCostSettings.CostRotateHalf;
            return apCost;
        }
        else
        {
            return 0;
        }
    }

    #endregion --------------------------------------------------------------------------------

    #endregion
    
    #region ==== P L A Y E R  I N S T A N T I A T I O N =======================================

    public void SetPlayerInfoOnInstantiation(Player player, PitchTile pitchTile, int coordX, int coordZ)
    {
        CoordX = coordX;
        CoordZ = coordZ;
        CurrentTile = pitchTile;
        Player = player;
        Name = Player.Name;
        IsBallHolder = Player.startWithBall;
        MaxActionPoints = Player.Stats.MaxActionPoints;
        CurrentActionPoints = MaxActionPoints;
    }

    #endregion
    
    #region ==== O T H E R  M E T H O D S =====================================================
    
    #region ---- Set player Active / Inactive -------------------------------------------------
    
    // Set Active
    public void SetPlayerActive()
    {
        activate();
        MatchManager.PitchGrid.PathFinding.DrawPathLine.ResetAccMoveCost();
    }

    public void SetPlayerActive(PlayerInit init)
    {
        activate();
    }

    private void activate()
    {
        clearAllModeStates();
        IsActive = true;
        bodyRenderer.material.color = activeColor;
        MatchManager.MatchPlayerManager.SetOtherPlayersInactive(this);
        MatchManager.MatchPlayerManager.CurrentActivePlayer = this;
        MatchManager.Hud.UpdatePlayerInfo(this); 
    }

    // Set Inactive
    public void SetPlayerInactive()
    {
        IsActive = false;
        PlayerMode = PlayerMode.Idle;
        bodyRenderer.material.color = defaultColor;
    }

    #endregion

    #region ---- Set player Coordinates and CurrentTile ---------------------------------------
    public void SetCurrentTile(PitchTile tile)
    {
        this.CoordX = tile.CoordX;
        this.CoordZ = tile.CoordZ;
        this.CurrentTile = tile;
    }

    #endregion

    #region ---- Player Highlight -------------------------------------------------------------
    public void PlayerHighlightOn()
    {
        bodyRenderer.material.color = highLightColor;
    }

    public void PlayerHighlightOff()
    {
        if (IsActive)
        {
            bodyRenderer.material.color = activeColor;
        }

        else
        {
            bodyRenderer.material.color = defaultColor;
        } 
    }

    #endregion

    #region ---- Set/Unset Player as BallHolder -----------------------------------------------
    public void setAsBallHolder(bool isBallHolder)
    {
        if (isBallHolder)
        {
            this.IsBallHolder = true;
        }
        else if (!isBallHolder)
        {
            this.IsBallHolder = false;
        }
    }

    #endregion


    #endregion
}
