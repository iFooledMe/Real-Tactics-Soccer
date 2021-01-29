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

    public Vector3 VectorPosition {get; private set;}

    /* #endregion */

    /* #region ---- Occupation state ---------------------------------------------------------- */
    public bool IsOccupied {get; private set;}
    public MatchPlayer OccupiedByPlayer {get; private set;}


    /* #endregion */

    /* #region ---- Movement/Pathfinding Fields ----------------------------------------------- */
    [SerializeField]
    public List<PitchTile> NeighbourTiles { get; set; }
    
    public int CostToEnter {get; set;}
    
    private bool _isViableTarget = false;
    public bool IsViableTarget { get => _isViableTarget; set => _isViableTarget = value; }

    /* #endregion */

    /* #region ---- Movement/Pathfinding Fields ----------------------------------------------- */
    public GameObject RotateGridOverlay {get; private set;}

    /* #endregion */

    /* #region ---- Dependencies -------------------------------------------------------------- */
    private MatchManager MatchManager;

    /* #endregion */

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

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== AWAKE / START ============================================================= */
    void Awake() 
    {
        getDependencies();
    }
    
    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== INTERACTIONS ============================================================== */
    
    /* #region ---- MouseOver / MouseExit ----------------------------------------------------- */
    void OnMouseEnter() 
    {
        MatchManager.MatchPlayerInput.OnPitchTileMouseEnter(this);
    }

    /* #endregion */

    /* #region ---- Left click ---------------------------------------------------------------- */
    
    private void OnMouseUp() 
    {
        MatchManager.MatchPlayerInput.OnPitchTileLeftClick(this);
    }

    /* #endregion */
    
    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== GENERAL HELPER FUNCTIONS ================================================== */
    
    /* #region ---- Set tile coordinates and Vector position fields --------------------------- */
    public void SetCoodinates(int coordX, int coordZ) 
    {
        this.CoordX = coordX;
        this.CoordZ = coordZ;

        this.VectorPosition = new Vector3 (
            transform.position.x, 
            transform.position.y, 
            transform.position.z );
    }
    /* #endregion */

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

    /* #region ---- Set tile occupied/unoccupied ---------------------------------------------- */
    public void setOccupied(MatchPlayer player)
    {
        IsOccupied = true;
        OccupiedByPlayer = player;
        CostToEnter = MatchManager.MatchPlayerManager.ActionsApCostSettings.CostEnterTileOtherPlayer;
    }

    public void setUnOccupied()
    {
        this.IsOccupied = false;
        this.OccupiedByPlayer = null;
        CostToEnter = MatchManager.MatchPlayerManager.ActionsApCostSettings.CostEnterTileEmpty;
    }

    /* #endregion */

    /* #region ---- Set Rotate Grid Overlay --------------------------------------------------- */
    public void InitRotateGridOverlayObjects()
    {
        RotateGridOverlay = (GameObject)Instantiate(MatchManager.PitchGrid.RotateGridOverlay);
        
        Vector3 position = new Vector3(
            this.transform.position.x,
            RotateGridOverlay.transform.position.y,
            this.transform.position.z);

        RotateGridOverlay.transform.position = position;
        
        RotateGridOverlay.SetActive(false);
    }

    public void ActivateRotateGridOverlay(bool activate)
    {
        if (activate)
        {
            RotateGridOverlay.SetActive(true);
        }
        else if (!activate)
        {
            RotateGridOverlay.SetActive(false);
        }
    }

    /* #endregion */


    /* #endregion */
    /* ======================================================================================== */

}
