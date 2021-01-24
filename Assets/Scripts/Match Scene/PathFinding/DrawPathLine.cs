using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPathLine
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */
    
    /* #region ---- Draw Path Line ------------------------------------------------------------ */
    public int AccumulatedMoveCost {get; private set;}
    public bool AccCostReset {get; private set;}
    private List<Vector3> listLinePoints;

    /* #endregion */

    /* #region ---- Dependencies -------------------------------------------------------------- */
    MatchManager MatchManager;

    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */
    
    /* #region ==== CONSTRUCTOR =============================================================== */
    public DrawPathLine()
    {
        this.MatchManager = MatchManager.Instance;
        AccumulatedMoveCost = 0;
    }
    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== D R A W  P A T H  L I N E ================================================= */
    
    /* #region ---- Draw movement path line from active player to target tile ----------------- */
    public void Draw(PitchTile targetIn) 
    {
        if (MatchManager.MatchPlayerManager.CurrentActivePlayer.PlayerMode == PlayerMode.Move)
        {
            if (!MatchManager.MatchPlayerManager.PlayInAction)
            { 
                PitchManager PitchManager = MatchManager.PitchManager;
                PitchGrid PitchGrid = MatchManager.PitchGrid;
                MatchPlayer Player = MatchManager.MatchPlayerManager.GetActivePlayer();


                /* #region ---- Reset Path ----------------------------------------- */
                ClearPlayerMovePathLines();
                setAllTilesNoneViableTarget(PitchManager);
                ResetAccMoveCost();
                
                /* #endregion ------------------------------------------------------ */

                /* #region ---- Set new Path to target ----------------------------- */
                List<PitchTile> pathToTarget = PitchGrid.PathFinding.GetPathToTarget(targetIn, Player);
                listLinePoints = new List<Vector3>();
                
                /* #endregion ------------------------------------------------------ */
                
                /* #region ---- Draw Path Line & Rotate Player --------------------- */
                if (pathToTarget != null) 
                {    
                    /* #region ---- Player rotate & Calculate AP cost for rotation - */

                    Player.FaceTarget(pathToTarget[1].transform);
                    AccumulatedMoveCost = Player.CalcRotationApCost ((int)Player.transform.eulerAngles.y);

                    /* #endregion --------------------------------------------------- */
                    
                    /* #region ---- Draw Path Line --------------------------------- */
                    GameObject PathLineObject = MatchManager.InstantiateGameObject(PitchGrid.PathLinePrefab);
                    PathLineObject.name = "PlayerMove PathLine";
                    LineRenderer lineRenderer = PathLineObject.GetComponent<LineRenderer>();
                    int counter = 0;
                    foreach (PitchTile pathTile in pathToTarget) 
                    {
                        counter++;
                        //TODO: QuickFix: Find out why CurrentActionPoints has to be reduced by one for the player to not exceed CurrentActionPoints
                        if (Player.CurrentActionPoints - 1 >= AccumulatedMoveCost)
                        {
                            Vector3 linePoint = new Vector3 (
                                pathTile.transform.position.x, 
                                PathLineObject.transform.position.y, 
                                pathTile.transform.position.z);
                        
                            listLinePoints.Add(linePoint);
                            
                            if(counter != 1){ AccumulatedMoveCost += pathTile.CostToEnter; }
                            pathTile.IsViableTarget = true;
                        }

                        updateAPCostInHUD(AccumulatedMoveCost);
                    }
                    
                    lineRenderer.positionCount = listLinePoints.Count;
                    lineRenderer.SetPositions(listLinePoints.ToArray());
                    /* #endregion --------------------------------------------------- */
                }

                /* #endregion ------------------------------------------------------ */
            }
        }
    }
    /* #endregion */
    
    /* #region ---- Clear Player Movement Path Line(s) ---------------------------------------- */
    public void ClearPlayerMovePathLines() 
    {
        MatchManager.DestroyObjectsByTag("PathLine"); 
    }
    /* #endregion */

    /* #region ---- Reset all tiles to none viable movement targets --------------------------- */
    private void setAllTilesNoneViableTarget(PitchManager PitchManager)
    {
        for (int x = 1; x <= MatchManager.PitchGrid.PitchSettings.PitchWidth; x++) {
            for (int z = 1; z <= MatchManager.PitchGrid.PitchSettings.PitchLength; z++) 
            {    
               PitchManager.GetPitchTile(x, z).IsViableTarget = false; 
            }
        }
    }
    /* #endregion */

    /* #region ---- Reset/Update Accumulated Move Cost ---------------------------------------- */
    public void ResetAccMoveCost()
    {
        AccumulatedMoveCost = 0;
        AccCostReset = true;
        MatchManager.Hud.UpdateAccAPCost(AccumulatedMoveCost);
    } 
    
    private void updateAPCostInHUD(int accCost)
    {
        AccCostReset = false;
        MatchManager.Hud.UpdateAccAPCost(accCost);
    }
    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */
}
