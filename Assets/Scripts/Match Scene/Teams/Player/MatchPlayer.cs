﻿using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMode
{
    Idle,
    Move,
    Pass
}

public class MatchPlayer : MonoBehaviour
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */
    
    /* #region ---- Player Info --------------------------------------------------------------- */
    public Player Player {get; private set;}

    public string Name {get; private set;}

    public int CoordX {get; private set;}
    public int CoordZ {get; private set;}

    /* #endregion */

    /* #region ---- Player Stats -------------------------------------------------------------- */
    public int ActionPoints {get; private set;}

    /* #endregion */

    /* #region ---- Player States ------------------------------------------------------------- */
    public bool IsActive {get; private set;}

    private Color defaultColor = Color.green;
    private Color highLightColor = Color.blue;
    private Color activeColor = Color.red;

    public PlayerMode PlayerMode = PlayerMode.Idle;

    public int RotationAngle {get; private set;}

    /* #endregion */
    
    /* #region ---- Components ---------------------------------------------------------------- */
    private Renderer bodyRenderer;

    /* #endregion */

    /* #region ---- Dependencies -------------------------------------------------------------- */
    private MatchManager MatchManager;
    public PlayerActions PlayerActions {get; private set;}

    /* #endregion */
    
    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== GET DEPENDENCIES & COMPONENTS ============================================= */
    private void getDependencies()
    {
        getMatchManager();
        setPlayerActions();
    }

    private void getComponents()
    {
        getBodyRenderer();
    }

    /* #region ---- Get MatchManager ---------------------------------------------------------- */
    public void getMatchManager()
    {
        MatchManager = MatchManager.Instance;
    }
    /* #endregion */

    /* #region ---- Get Renderer Component ---------------------------------------------------- */
    public void getBodyRenderer()
    {
        GameObject playerBody = this.transform.Find("PlayerBody").gameObject;
        bodyRenderer = playerBody.GetComponent<Renderer>();
    }
    /* #endregion */

    /* #region ---- Set PlayerActions --------------------------------------------------------- */
    public void setPlayerActions()
    {
        this.PlayerActions = new PlayerActions(this);
    }

    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== AWAKE / START ============================================================= */
    private void Awake() 
    {
        getDependencies();
        getComponents();
    }

    private void Start()
    {
        UpdateRotationAngle ((int)this.gameObject.transform.eulerAngles.y);
    }

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== UPDATE ==================================================================== */
    void Update() 
    {
        PlayerActions.ActionSelector(PlayerActions.CurrentAction);
    }

    /* #endregion */
    /* ======================================================================================== */
    
    /* #region ==== P L A Y E R  M O D E S  =================================================== */
    
    /* #region ---- Player Mode Switcher ------------------------------------------------------ */
    public void SetPlayerMode(PlayerMode playerMode)
    {
        if (!MatchManager.MatchPlayerManager.PlayInAction)
        {
            switch(playerMode) 
            {
            case PlayerMode.Idle:
                setIdleMode();
                break;
            case PlayerMode.Move:
                setMoveMode();
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

    /* #endregion */

    /* #region ---- Idle Mode ------------------------------------------------------------------ */
    private void setIdleMode()
    {
        PlayerMode = PlayerMode.Idle;
        clearMovePaths();
        deactivateBallGrid();
        
    }

    /* #endregion */

    /* #region ---- Move Mode ------------------------------------------------------------------ */
    private void setMoveMode()
    {
        PlayerMode = PlayerMode.Move;
        deactivateBallGrid();
    }

    /* #endregion */

    /* #region ---- Pass Mode ------------------------------------------------------------------ */
    private void setPassMode()
    {
        PlayerMode = PlayerMode.Pass;
        MatchManager.BallGrid.ActivateBallGrid();
        clearMovePaths();
    }

    /* #endregion */

    /* #region ---- Commen helpers ------------------------------------------------------------- */
    private void clearMovePaths()
    {
        MatchManager.DestroyObjectsByTag("PathLine");
    }

    private void deactivateBallGrid()
    {
        MatchManager.BallGrid.DeactivateBallGrid();
    }

    /* #endregion */
    
    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== P L A Y E R  R O T A T I O N  A N G L E =================================== */

    /* #region ---- Player Rotation to face a specified target -------------------------------- */
    public void FaceTarget(Transform target)
    {
        this.transform.LookAt(target);
    }

    /* #endregion ----------------------------------------------------------------------------- */


    /* #region ---- Player Rotation to face a specified target -------------------------------- */
    public void UpdateRotationAngle (int angleY)
    {
        int angleX = (int)this.gameObject.transform.eulerAngles.x;
        int angleZ = (int)this.gameObject.transform.eulerAngles.z;

        this.gameObject.transform.eulerAngles = new Vector3(angleX, angleY, angleZ);
        this.RotationAngle = angleY;
    }

    /* #endregion ----------------------------------------------------------------------------- */

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== O T H E R  M E T H O D S ================================================== */
    
    /* #region ---- Set player info on Instantiation ------------------------------------------ */
    public void SetPlayerInfo(Player player, int coordX, int coordZ)
    {
        CoordX = coordX;
        CoordZ = coordZ;
        Player = player;
        Name = Player.Name;
        ActionPoints = Player.Stats.ActionPoints;
    }

    /* #endregion */

    /* #region ---- Set player Active / Inactive ---------------------------------------------- */
    public void SetPlayerActive()
    {
        IsActive = true;
        bodyRenderer.material.color = activeColor;
        MatchManager.MatchPlayerManager.SetOtherPlayersInactive(this);
        MatchManager.MatchPlayerManager.CurrentActivePlayer = this;
        MatchManager.DestroyObjectsByTag("PathLine");
    }

    public void SetPlayerInactive()
    {
        IsActive = false;
        PlayerMode = PlayerMode.Idle;
        bodyRenderer.material.color = defaultColor;
    }

    /* #endregion */

    /* #region ---- Set player Coordinates by PitchTile --------------------------------------- */
    public void SetPlayerCoordinatesByTile(PitchTile tile)
    {
        this.CoordX = tile.CoordX;
        this.CoordZ = tile.CoordZ;
    }

    /* #endregion */

    /* #region ---- Player Highlight ---------------------------------------------------------- */
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

    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */

}
