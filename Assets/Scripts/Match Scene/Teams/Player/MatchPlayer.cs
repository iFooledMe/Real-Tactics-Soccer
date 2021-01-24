using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMode
{
    Idle,
    Move,
    Pass
}

public enum PlayerStat
{
    ActionPoints
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
    public int MaxActionPoints {get; private set;}
    public int CurrentActionPoints {get; private set;}

    /* #endregion */

    /* #region ---- Player States ------------------------------------------------------------- */
    public bool IsActive {get; private set;}

    private Color defaultColor = Color.green;
    private Color highLightColor = Color.blue;
    private Color activeColor = Color.red;

    public PlayerMode PlayerMode = PlayerMode.Idle;

    private int currentAngle;
    private int fullRotation0;
    private int fullRotation1;
    private int fullRotation2;
    private int halfRotation1;
    private int halfRotation2;


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

    /* #region ---- Update RotationAngle ------------------------------------------------------ */
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

    /* #endregion ----------------------------------------------------------------------------- */

    /* #region ---- Calculate AP Cost for rotation -------------------------------------------- */
    public int CalcRotationApCost (int angleY)
    {
        if (angleY == fullRotation0 || angleY == fullRotation1 || angleY == fullRotation2)
        {
            int apCost = MatchManager.MatchPlayerManager.ActionsApCostSettings.CostRotateFull;
            Debug.Log($"Full Rotation - Cost {apCost}");
            return apCost;
        }
        else if (angleY == halfRotation1 || angleY == halfRotation2)
        {
            int apCost = MatchManager.MatchPlayerManager.ActionsApCostSettings.CostRotateHalf;
            Debug.Log($"Half Rotation - Cost {apCost}");
            return apCost;
        }
        else
        {
            Debug.Log("No/Minor Rotation - Cost 0");
            return 0;
        }
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
        MaxActionPoints = Player.Stats.MaxActionPoints;
        CurrentActionPoints = MaxActionPoints;
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

    /* #region ---- Update Player Stat -------------------------------------------------------- */
    public void UpdateStat(PlayerStat stat, int newValue)
    {
        switch(stat) 
        {
            case PlayerStat.ActionPoints:
                updateActionPoints(newValue);
                break;
        }
    }

    private void updateActionPoints(int newValue)
    {
        if(newValue <= MaxActionPoints) CurrentActionPoints = newValue;
        if(newValue > MaxActionPoints) CurrentActionPoints = MaxActionPoints;
    }


    /* #endregion */ 

    /* #endregion */
    /* ======================================================================================== */

}
