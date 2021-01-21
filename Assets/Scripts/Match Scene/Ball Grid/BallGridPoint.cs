using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGridPoint : MonoBehaviour
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */
       
    /* #region ---- Dependencies -------------------------------------------------------------- */
    private MatchManager MatchManager;

    /* #endregion */

    public PitchTile PitchTile;
    List<Collider> collidingPoints = new List<Collider>();
    public List<PitchTile> ReachedFromTiles {get; private set;}

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== GET DEPENDENCIES ========================================================== */
    private void getDependencies()
    {
        getMatchManager();
    }

    /* #region ---- Get/Set MatchManager (Get Instance and sets itself as ref on the same ----- */
    public void getMatchManager()
    {
        this.MatchManager = MatchManager.Instance;
    }
    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== AWAKE / START ============================================================= */
    private void Awake() 
    {
        getDependencies();
    }
    
    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== INTERACTIONS ============================================================== */
    
    /* #region ---- MouseOver / MouseExit ----------------------------------------------------- */
    void OnMouseEnter() 
    {
        
    }

    /* #endregion */

    /* #region ---- Left click ---------------------------------------------------------------- */
    private void OnMouseUp() 
    {
        foreach(var tile in ReachedFromTiles)
        {
            Debug.Log(tile.gameObject.name);
        }
    }

    /* #endregion */
    
    /* #endregion */
    /* ======================================================================================== */
    
    /* #region ==== GET OVERLAPPING BALL POINTS AND ADD THEIR PITCHTILE TO THIS LIST ========== */
    private void OnTriggerEnter(Collider collidingPoint)
    {
        if (!collidingPoints.Contains(collidingPoint))
        {
            if(collidingPoint.tag != "BallPointer")
            {
                collidingPoints.Add(collidingPoint);
                BallGridPoint ballPoint = collidingPoint.GetComponent<BallGridPoint>();
                PitchTile tile = ballPoint.PitchTile;
                AddToReachableTiles(tile);
            }
        }
    }

    public void AddToReachableTiles(PitchTile tile)
    {
        if (ReachedFromTiles == null)
        {
            ReachedFromTiles = new List<PitchTile>();
        }
        
        if (!ReachedFromTiles.Contains(tile))
        {
            ReachedFromTiles.Add(tile);
        } 
    }

    /* #endregion */
    /* ======================================================================================== */
}
