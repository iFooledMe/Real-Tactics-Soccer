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
        PitchManager PitchManager = MatchManager.PitchManager;
        PitchGrid PitchGrid = MatchManager.PitchGrid;
        MatchPlayer player = MatchManager.MatchPlayerManager.GetActivePlayer();

        if (!MatchManager.MatchPlayerManager.PlayInAction)
        {
            ClearPlayerMovePathLines();
            setAllTilesNoneViableTarget(PitchManager);
            accumulatedMoveCost = 0;
            List<PitchTile> pathToTarget = PitchGrid.PathFinding.GetPathToTarget(targetIn, player);
            listLinePoints = new List<Vector3>();

            if (pathToTarget != null) 
            {
                GameObject PathLineObject = MatchManager.InstantiateGameObject(PitchGrid.PathLinePrefab);
                PathLineObject.name = "PlayerMove PathLine";
                LineRenderer lineRenderer = PathLineObject.GetComponent<LineRenderer>();
                int counter = 0;
                foreach (PitchTile pathTile in pathToTarget) 
                {
                    counter++;

                    if (accumulatedMoveCost < player.ActionPoints)
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
        for (int x = 1; x <= PitchManager.PitchWidth; x++) {
            for (int z = 1; z <= PitchManager.PitchLength; z++) 
            {    
               PitchManager.GetPitchTile(x, z).IsViableTarget = false; 
            }
        }
    }
    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */
}
