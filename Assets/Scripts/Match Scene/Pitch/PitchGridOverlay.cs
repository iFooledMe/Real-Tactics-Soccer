using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchGridOverlay : MonoBehaviour
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */
    
    /* #region ---- Grid Overlay -------------------------------------------------------------- */
    private GameObject [,] _gridOverlay = new GameObject[200, 200];
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

    /* #region ==== TOGGLE OVERLAY GRID ======================================================= */
    
    /* #region ---- Toggle OverlayGrid -------------------------------------------------------- */
    public void ToggleOverlayGrid() 
    {          
        int pitchWidth = MatchManager.PitchManager.PitchWidth;
        int pitchLength = MatchManager.PitchManager.PitchLength;
        float xOffset = MatchManager.PitchGrid.XOffset;
        float zOffset = MatchManager.PitchGrid.ZOffset;
        
        if (!_gridOn) 
        {
            _gridOn = true;
        }
        else 
        {
            _gridOn = false;
        }
        
        for (int x = 1; x <= pitchWidth; x++) 
        {
            for (int z = 1; z <= pitchLength; z++) 
            {
                if (_gridOn)
                {
                    GameObject overlayTile = (GameObject)Instantiate(gridOverlayPrefab);
                    overlayTile.transform.position = new Vector3(
                    overlayTile.transform.position.x - xOffset + x, 
                    overlayTile.transform.position.y,
                    overlayTile.transform.position.z - zOffset + z);
                    overlayTile.transform.SetParent(this.transform);
                    _gridOverlay[x,z] = overlayTile;
                }
                else 
                {
                    Destroy(_gridOverlay[x,z]);
                    //TODO: GRID OVERLAY: Change to enable/disable instead of destroy.
                }
            }  
        }      
    }

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */
    
    /* #endregion */
    /* ======================================================================================== */
}
