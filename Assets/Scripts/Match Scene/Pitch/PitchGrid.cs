using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchGrid : SingletonMonoBehaviour<PitchGrid>
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */
    
    /* #region ---- Pitch --------------------------------------------------------------------- */
    private GameObject [,] _pitchTiles = new GameObject[200, 200];
    public float XOffset {get; private set;}
    public float ZOffset {get; private set;}

    //TODO: Instantiate _pitchTileObjects with pitchWidth and PitchLenght. Trying to do so now cause the pitch to not be created propperly.
    
    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */
    
    /* #region ---- Prefabs ------------------------------------------------------------------- */  
    [SerializeField] private GameObject noLines;

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
        MatchManager = MatchManager.Instance;
        MatchManager.SetPitchGrid();
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
    
    void Start()
    {
        createPitch();
    }

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== CREATE PITCH ============================================================== */
    
    /* #region ---- Set Positional offset (grid created with 0,0,0 as center) ----------------- */
    private void setPosOffset(int pitchWidth, int PitchLength) 
    {
        XOffset = (pitchWidth/2) + 0.5f;
        ZOffset = (PitchLength/2) + 0.5f;
    }
    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */
    
    /* #region ---- CreatePitch --------------------------------------------------------------- */
    private void createPitch() 
    {     

        int pitchWidth = MatchManager.PitchManager.PitchWidth;
        int pitchLength = MatchManager.PitchManager.PitchLength;
        setPosOffset(pitchWidth, pitchLength);

        Debug.Log($"Create pitch --- Width: {pitchWidth} - Length: {pitchLength}");

        for (int x = 1; x <= pitchWidth; x++) {
            for (int z = 1; z <= pitchLength; z++) 
            {    
                GameObject _pitchTileObject = InstantiateGameObject(noLines);
                //PitchTile _pitchTile = _pitchTileObject.GetComponent<PitchTile>();   
                //_pitchTile.PitchGrid = this;
                //_pitchTile.CoordX = x;
                //_pitchTile.CoordZ = z;
                _pitchTileObject.transform.position = new Vector3(
                _pitchTileObject.transform.position.x - XOffset + x, 
                _pitchTileObject.transform.position.y,
                _pitchTileObject.transform.position.z - ZOffset + z);
                //_pitchTile.setVectorPositions();
                _pitchTileObject.name = "Pitch tile - " + x + ":" + z;
                _pitchTileObject.transform.SetParent(this.transform);
                //pitchTiles[x,z] = _pitchTileObject;
            }
        }
    }
    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== GENERAL HELPERS =========================================================== */

    /* #region ---- Instantiate GameObject ---------------------------------------------------- */
    public GameObject InstantiateGameObject(GameObject prefab) 
    {
        GameObject _pitchTileObject = (GameObject)Instantiate(prefab);
        return _pitchTileObject;
    }
    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #endregion */
    /* ======================================================================================== */



}
