using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOverlay : SingletonMonoBehaviour<GridOverlay>
{

    /* #region ==== FIELDS & PROPERTIES ======================================================= */
    
    /* #region ---- Grid Overlay -------------------------------------------------------------- */
    private GameObject [,] _gridOverlay;
    private bool _gridOn = false;

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #region ---- Prefabs ------------------------------------------------------------------- */
    [SerializeField] private GameObject gridOverlayPrefab;

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
        getSetMatchManager();
    }

    /* #region ---- Get/Set MatchManager (Get Instance and sets itself as ref on the same ----- */
    public void getSetMatchManager()
    {
        this.MatchManager = MatchManager.Instance;
        MatchManager.SetGridOverLay();
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

    private void Start()
    {
        CreateGridArray();
    }
    
    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== T O G G L E  O V E R L A Y  G R I D ======================================= */
    
    /* #region ---- Create Grid Array --------------------------------------------------------- */
    private void CreateGridArray()
    {
        int pitchWidth = MatchManager.PitchManager.PitchWidth;
        int pitchLength = MatchManager.PitchManager.PitchLength;
        
        _gridOverlay = new GameObject[pitchWidth + 1, pitchLength + 1];    
    }

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */
    
    /* #region ---- Toggle OverlayGrid -------------------------------------------------------- */
    public void ToggleOverlayGrid() 
    {     
        PitchManager PitchManager = MatchManager.PitchManager;
        PitchGrid PitchGrid = MatchManager.PitchGrid;

        if (!_gridOn) 
        {
            _gridOn = true;
        }
        else 
        {
            _gridOn = false;
        }
        
        for (int x = 1; x <= PitchManager.PitchWidth; x++) 
        {
            for (int z = 1; z <= PitchManager.PitchLength; z++) 
            {
                if (_gridOn)
                {
                    GameObject overlayTile = (GameObject)Instantiate(gridOverlayPrefab);
                    overlayTile.transform.position = new Vector3(
                    overlayTile.transform.position.x - PitchGrid.XOffset + x, 
                    overlayTile.transform.position.y,
                    overlayTile.transform.position.z - PitchGrid.ZOffset + z);
                    overlayTile.transform.SetParent(this.transform);
                    _gridOverlay[x,z] = overlayTile;
                }
                else 
                {
                    Destroy(_gridOverlay[x,z]);
                }
            }  
        }      
    }

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */
    
    /* #endregion */
    /* ======================================================================================== */
}
