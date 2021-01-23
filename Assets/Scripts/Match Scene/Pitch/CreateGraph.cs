using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGraph
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */

    /* #region ---- Dependencies -------------------------------------------------------------- */
    MatchManager MatchManager;
    
    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #endregion */
    /* ======================================================================================== */
    
    /* #region ==== CONSTRUCTOR =============================================================== */
    public CreateGraph(MatchManager matchManager)
    {
        this.MatchManager = matchManager;
    }
    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== C R E A T E  G R A P H ==================================================== */
    public void AddNeighbourTiles() 
    {
        
        PitchManager PitchManager = MatchManager.PitchManager;
        int pitchWidth = MatchManager.PitchGrid.PitchSettings.PitchWidth;
        int pitchLength = MatchManager.PitchGrid.PitchSettings.PitchLength;

        for (int x = 1; x <= pitchWidth; x++) {
            for (int z = 1; z <= pitchLength; z++) 
            {    
                PitchTile PitchTile = PitchManager.GetPitchTile(x, z); 
                PitchTile.NeighbourTiles = new List<PitchTile>();
                addNeighbourTiles(x, z, PitchTile);

            }
        }
        
        void addNeighbourTiles(int x, int z, PitchTile PitchTile) 
        {
            /* North */ 
            if (z < pitchLength) 
            {
                PitchTile neighbourPitchTile = PitchManager.GetPitchTile(x, z + 1); 
                PitchTile.NeighbourTiles.Add(neighbourPitchTile);
            }

            /* South */ 
            if (z > 1) 
            {
                PitchTile neighbourPitchTile = PitchManager.GetPitchTile(x, z - 1); 
                PitchTile.NeighbourTiles.Add(neighbourPitchTile);
            }
        

            /* West */ 
            if (x < pitchWidth) 
            {
                PitchTile neighbourPitchTile = PitchManager.GetPitchTile(x + 1, z); 
                PitchTile.NeighbourTiles.Add(neighbourPitchTile);
            }

            /* East */ 
            if (x > 1) 
            {
                PitchTile neighbourPitchTile = PitchManager.GetPitchTile(x - 1, z); 
                PitchTile.NeighbourTiles.Add(neighbourPitchTile);
            }

            /* NorthWest */ 
            if (x < pitchWidth && z < pitchLength) 
            {
                PitchTile neighbourPitchTile = PitchManager.GetPitchTile(x + 1, z + 1); 
                PitchTile.NeighbourTiles.Add(neighbourPitchTile);
            }

            /* NorthEast */ 
            if (x > 1 && z < pitchLength) 
            {
                PitchTile neighbourPitchTile = PitchManager.GetPitchTile(x - 1, z + 1); 
                PitchTile.NeighbourTiles.Add(neighbourPitchTile);
            }

            /* SouthWest*/ 
            if (x < pitchWidth && z > 1) 
            {
                PitchTile neighbourPitchTile = PitchManager.GetPitchTile(x + 1, z - 1); 
                PitchTile.NeighbourTiles.Add(neighbourPitchTile);
            }

            /* SouthEast*/ 
            if (x > 1 && z > 1) 
            {
                PitchTile neighbourPitchTile = PitchManager.GetPitchTile(x - 1, z - 1); 
                PitchTile.NeighbourTiles.Add(neighbourPitchTile);
            }
        }
    }
    /* #endregion */
    /* ======================================================================================== */

}
