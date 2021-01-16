using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchTile : MonoBehaviour
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */
    
    /* #region ---- Coordinates / Position ---------------------------------------------------- */
    [SerializeField]
    public int CoordX { get; private set; }
    
    [SerializeField]
    public int CoordZ { get; private set; }

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #region ---- Occupation state ---------------------------------------------------------- */
    public bool IsOccupied {get; private set;}
    public GameObject OccupiedByPlayer {get; private set;}


    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #region ---- Movement/Pathfinding Fields ----------------------------------------------- */
    [SerializeField]
    public List<PitchTile> NeighbourTiles { get; set; }
    
    
    int _costToEnter = PitchManager.EnterCostDefault;
    
    private bool _isViableTarget = false;
    public bool IsViableTarget { get => _isViableTarget; set => _isViableTarget = value; }

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #region ---- Dependencies -------------------------------------------------------------- */
    private MatchManager MatchManager;

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== GET DEPENDENCIES ========================================================== */
    private void getDependencies()
    {
        getMatchManager();
    }

    /* #region ---- Get MatchManager ---------------------------------------------------------- */
    public void getMatchManager()
    {
        MatchManager = MatchManager.Instance;
    }
    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== AWAKE / START ============================================================= */
    void Awake() 
    {
        getDependencies();
    }
    
    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== GENERAL HELPER FUNCTIONS ================================================== */
    
    /* #region ---- Helper - Set this tiles coordinates in the grid --------------------------- */
    public void SetCoodinates(int coordX, int coordZ) 
    {
        this.CoordX = coordX;
        this.CoordZ = coordZ;
    }
    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #region ---- Creates/Add to a new list of all neighbouring tiles ----------------------- */
    public void CreateNeighbourTilesList()
    {
        NeighbourTiles = new List<PitchTile>();
    }

    public void AddTileToList(PitchTile pitchTile)
    {
        NeighbourTiles.Add(pitchTile);
    }

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #region ---- Set tile occupied/unoccupied ---------------------------------------------- */
    public void setOccupied(GameObject player)
    {
        this.IsOccupied = true;
        this.OccupiedByPlayer = player;
    }

    public void setUnOccupied()
    {
        this.IsOccupied = false;
        this.OccupiedByPlayer = null;
    }

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #endregion */
    /* ======================================================================================== */

}
