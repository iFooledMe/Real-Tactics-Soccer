using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPathLine
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */
    
    /* #region ---- Draw Path Line ------------------------------------------------------------ */
    private int accumulatedMoveCost;
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
                accumulatedMoveCost = 0;
                
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
                    int rotationApCost = Player.CalcRotationApCost ((int)Player.transform.eulerAngles.y);

                    /* #endregion --------------------------------------------------- */
                    
                    /* #region ---- Draw Path Line --------------------------------- */
                    GameObject PathLineObject = MatchManager.InstantiateGameObject(PitchGrid.PathLinePrefab);
                    PathLineObject.name = "PlayerMove PathLine";
                    LineRenderer lineRenderer = PathLineObject.GetComponent<LineRenderer>();
                    int counter = 0;
                    foreach (PitchTile pathTile in pathToTarget) 
                    {
                        counter++;
                        if (accumulatedMoveCost < Player.CurrentActionPoints - rotationApCost)
                        {
                            Vector3 linePoint = new Vector3 (
                                pathTile.transform.position.x, 
                                PathLineObject.transform.position.y, 
                                pathTile.transform.position.z);
                        
                            listLinePoints.Add(linePoint);
                            
                            if(counter != 1){ accumulatedMoveCost += pathTile.CostToEnter; }
                            pathTile.IsViableTarget = true;
                        }
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
    void setAllTilesNoneViableTarget(PitchManager PitchManager)
    {
        for (int x = 1; x <= MatchManager.PitchGrid.PitchSettings.PitchWidth; x++) {
            for (int z = 1; z <= MatchManager.PitchGrid.PitchSettings.PitchLength; z++) 
            {    
               PitchManager.GetPitchTile(x, z).IsViableTarget = false; 
            }
        }
    }
    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */
}
