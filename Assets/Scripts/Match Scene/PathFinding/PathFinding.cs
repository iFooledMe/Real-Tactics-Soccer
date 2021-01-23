using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */
    
    /* #region ---- Draw Path Line ------------------------------------------------------------ */
    private int accumulatedMoveCost;
    private List<Vector3> listLinePoints;


    /* #region ---- Dependencies -------------------------------------------------------------- */
    MatchManager MatchManager;
    public DrawPathLine DrawPathLine {get; private set;}

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== CONSTRUCTOR =============================================================== */
    public PathFinding()
    {
        this.MatchManager = MatchManager.Instance;
        this.DrawPathLine = new DrawPathLine();
    }
    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== C A L C U L A T E  &  G E T  P A T H  T O  T A R G E T ==================== */   
    
    public List<PitchTile> GetPathToTarget(PitchTile targetIn, MatchPlayer sourceIn)
    {
        PitchManager PitchManager = MatchManager.PitchManager;

        Dictionary<PitchTile, float> distanceToSource = new Dictionary<PitchTile, float>();
        Dictionary<PitchTile, PitchTile> previousPitchTile = new Dictionary<PitchTile, PitchTile>();
        List<PitchTile> unvisitedPitchTiles = new List<PitchTile>();
        List<PitchTile> CalculatedPathToTarget = null;
        
        //Set target tile
        PitchTile target = PitchManager.GetPitchTile(targetIn.CoordX, targetIn.CoordZ);

        //Set source tile
        PitchTile source = PitchManager.GetPitchTile(sourceIn.CoordX, sourceIn.CoordZ);
        distanceToSource[source] = 0;
        previousPitchTile[source] = null;

        //Set all other tiles and add all (incl. source) to list of unvisited tiles
        for (int x = 1; x <= MatchManager.PitchGrid.PitchSettings.PitchWidth; x++) {
            for (int z = 1; z <= MatchManager.PitchGrid.PitchSettings.PitchLength; z++) 
            {
                PitchTile pitchTile = PitchManager.GetPitchTile(x, z);

                if(pitchTile != source) 
                {
                    distanceToSource[pitchTile] = Mathf.Infinity;
                    previousPitchTile[pitchTile] = null;
                }

                unvisitedPitchTiles.Add(pitchTile);
            }
        }

        while(unvisitedPitchTiles.Count > 0) 
        {
            //"u" is going to be the unvisited node with the smallest distance.
            PitchTile u = null;

            //Find the tile with the shortest Distance to Source
            foreach(PitchTile possibleU in unvisitedPitchTiles ) 
            {
                if(u == null || distanceToSource[possibleU] < distanceToSource[u]) 
                {
                    u = possibleU;
                }
            }
            
            if (u == target) {
                break;
            }
            
            unvisitedPitchTiles.Remove(u);

            foreach(PitchTile v in u.NeighbourTiles) 
            {
                //float alt = distanceToSource[u] + u.DistanceToTile(v);
                float alt = distanceToSource[u] + v.CostToEnter + costForDiagonalMove(u, v);
                if (alt < distanceToSource[v]) 
                {
                    distanceToSource[v] = alt;
                    previousPitchTile[v] = u;
                }
            }
        }

        //If the while loop has ended or breaked, either we found the shortest path or there is no path to the target.
        if (previousPitchTile[target] == null) 
        {
            return CalculatedPathToTarget; 
        }
        else 
        {
            //Step trough the previousPitchTiles chain and add it to the pathToTarget.
            CalculatedPathToTarget = new List<PitchTile>();
            PitchTile current = target;  //start tile

            while (current != null) 
            {
                CalculatedPathToTarget.Add(current);
                current = previousPitchTile[current]; //Reset current to the previous tile in the chain
            }

            CalculatedPathToTarget.Reverse();
            return CalculatedPathToTarget;   
        }

        
    }

    // Check if movement from one tile to another is diagonal and add some small extra cost to make the algorithm to prefer straight movement
    float costForDiagonalMove(PitchTile fromTile, PitchTile toTile)
    {
        if (fromTile.CoordX != toTile.CoordX && fromTile.CoordZ != toTile.CoordZ)
        {
            return 0.001f;
        }
        else
        {
            return 0f;
        } 
    }

    /* #endregion */
    /* ======================================================================================== */


    
    
}
