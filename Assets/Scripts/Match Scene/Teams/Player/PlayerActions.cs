﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAction
{
    Idle,
    Move,
    Pass,
    Shoot
}

public class PlayerActions
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */

    /* #region ---- Action Selector ----------------------------------------------------------- */
    public PlayerAction CurrentAction = PlayerAction.Idle;

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #region ---- Move ---------------------------------------------------------------------- */
    private PitchTile MoveTargetTile;
    private PitchTile MoveSourceTile;
    private List<PitchTile> waypoints = null;
    private Vector3 nextWaypoint;
    private int currentWaypoint = 0;

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #region ---- Dependencies -------------------------------------------------------------- */
    private MatchManager MatchManager;
    private MatchPlayer Player;
    
    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== CONSTRUCTOR =============================================================== */
    public PlayerActions(MatchPlayer player)
    {
        this.MatchManager = MatchManager.Instance;
        this.Player = player;
    }
    
    /* #endregion */
    /* ======================================================================================== */
    
    /* #region ==== A C T I O N  S E L E C T O R ============================================== */
    public void ActionSelector(PlayerAction playerAction)
    {
        switch(playerAction) 
        {
            case PlayerAction.Idle:
                Idle();
                break;
            case PlayerAction.Move:
                Move();
                break;
            default:
                Idle();
                break;
        }
    }

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== I D L E  a c t i o n ====================================================== */
    private void Idle()
    {
        return;
    }

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== M O V E  a c t i o n ====================================================== */
    
    /* #region ---- 1. CHECK FOR MOVEMENT ----------------------------------------------------- */
    public void CheckForMovement(PitchTile targetTile)
    {
        if (targetTile.IsViableTarget)
        {
            if (targetTile.IsOccupied)
            {
                if (targetTile.OccupiedByPlayer == MatchManager.MatchPlayerManager.CurrentActivePlayer)
                {
                    Debug.Log("Tile already occupied by the active player");
                    return;
                }
                else
                {
                    Debug.Log("Can't move here! Tile is occupied by other player");
                    return;
                }
            }
            else
            {
                if(!MatchManager.MatchPlayerManager.PlayInAction) 
                {
                    MovePlayerSetup(targetTile);
                }
            }
        }
        else
        {
            Debug.Log("This tile is out of movement reach");
            return;
        }
    }

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */
    
    /* #region ---- 2. SETUP & START MOVEMENT ------------------------------------------------- */
    public void MovePlayerSetup(PitchTile targetTile)
    {
        MoveTargetTile = targetTile;
        MoveSourceTile = MatchManager.PitchManager.GetPitchTile(Player.CoordX, Player.CoordZ);
        
        PitchManager PitchManager = MatchManager.PitchManager;
        PathFinding PathFinding = MatchManager.PitchGrid.PathFinding;
        
        waypoints = PathFinding.GetPathToTarget(MoveTargetTile, Player);

        MatchManager.MatchPlayerManager.setPlayerInActionState(true);
        waypoints.RemoveAt(0);
        CurrentAction = PlayerAction.Move;
    }

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */
    
    /* #region ---- 3. MOVE [Executed in MatchPlayer.Update() ] ------------------------------- */
    private void Move()
    {   
        nextWaypoint = new Vector3(waypoints[
        currentWaypoint].transform.position.x, 
        Player.transform.position.y, 
        waypoints[currentWaypoint].transform.position.z);

        float distance = Vector3.Distance(nextWaypoint, Player.transform.position);
        
        Player.transform.position = Vector3.MoveTowards(
            Player.transform.position, 
            nextWaypoint, 
            MatchManager.MatchPlayerManager.OnScreenPlayerMoveSpeed * Time.deltaTime);
        
        if (distance <= 0.005)
        {
            Player.transform.position = nextWaypoint;
            currentWaypoint++;
        }

        if (currentWaypoint >= waypoints.Count)
        {
            setTilesStates();
            setPlayerStates();
            stopAndReset();
        }
    }

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #region ---- helper - Stop and Reset --------------------------------------------------- */
    private void stopAndReset()
    {
        MatchManager.PitchGrid.PathFinding.DrawPathLine.ClearPlayerMovePathLines();
        waypoints = null;
        currentWaypoint = 0;
        CurrentAction = PlayerAction.Idle;
    }
    
    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */
    
    /* #region ---- helper - Set Player States ------------------------------------------------ */
    private void setPlayerStates()
    {
        Player.transform.position = targetPosition();
        Player.SetPlayerCoordinatesByTile(MoveTargetTile);
        MatchManager.MatchPlayerManager.setPlayerInActionState(false);
    }

    private Vector3 targetPosition()
    {
        Vector3 targetPosition = new Vector3(
            MoveTargetTile.transform.position.x,
            Player.transform.position.y,
            MoveTargetTile.transform.position.z
        );

        return targetPosition;
    }
    
    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */
    
    /* #region ---- helper - Set Tiles States ------------------------------------------------- */
    private void setTilesStates()
    {
        MoveTargetTile.setOccupied(Player);
        MoveSourceTile.setUnOccupied();
    }

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #endregion */
    /* ======================================================================================== */
    


}
