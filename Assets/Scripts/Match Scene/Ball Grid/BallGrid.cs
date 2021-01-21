using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGrid : SingletonMonoBehaviour<BallGrid>
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */
    
    /* #region ---- Settings ------------------------------------------------------------------ */
    public float OnScreenBallMoveSpeed = 10f;
    
    /* #endregion */

    /* #region ---- BallGrid ----------------------------------------------------------------- */
    private List<BallGridPoint> ballGridPoints = new List<BallGridPoint>();
    public List<BallGridPoint> BallGridPoints {get => ballGridPoints; }
    public GameObject PointerPlateObj {get; private set;}
    public GameObject PointerObj {get; private set;}
    public BallGridPoint CurrentPoint {get; private set;}

    /* #endregion */
    
    /* #region ---- Prefabs ------------------------------------------------------------------- */
    [SerializeField] private GameObject BallGridPointPrefab;
    [SerializeField] private GameObject BallPointerPlatePrefab;
    [SerializeField] private GameObject BallPointerPrefab;

    /* #endregion ------------------------------------------------------------------------------*/

    /* #region ---- Dependencies -------------------------------------------------------------- */
    private MatchManager MatchManager;

    /* #endregion */

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
        MatchManager.SetBallGrid();
    }
    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== AWAKE / START ============================================================= */
    private void Awake() 
    {
        getDependencies();
    }
    
    private void Start()
    {
        MatchManager.PitchGrid.PitchCreated += createBallGrid;
    }

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== C R E A T E  B A L L  G R I D  ============================================ */
    
    /* #region ---- CREATE BALL GRID ---------------------------------------------------------- */
    private void createBallGrid()
    {
        int pitchWidth = MatchManager.PitchManager.PitchWidth;
        int pitchLength = MatchManager.PitchManager.PitchLength;

        for (int x = 1; x <= pitchWidth; x++) 
        {
            for (int z = 1; z <= pitchLength; z++) 
            {     
                createBallGridPoints(MatchManager.PitchManager.GetPitchTile(x,z));
            }
        }

        createBallPointer();
        DeactivateBallGrid();
    }

    /* #endregion */

    /* #region ---- Create Ball Grid points --------------------------------------------------- */
    private void createBallGridPoints(PitchTile pitchTile)
    {        
        for (int x = 1; x <= 9; x++) 
        {
            GameObject ballGridPointObj = MatchManager.InstantiateGameObject(BallGridPointPrefab);
            BallGridPoint ballGridPoint = ballGridPointObj.GetComponent<BallGridPoint>();
            ballGridPoint.PitchTile = pitchTile;
            ballGridPoint.AddToReachableTiles(pitchTile);
            setVectorPosition(pitchTile, ballGridPoint, x);
            ballGridPointObj.transform.SetParent(this.transform);
            ballGridPoints.Add(ballGridPoint);
        }
    }

    /* #endregion */

    /* #region ---- Set player Vector position ------------------------------------------------ */
    private void setVectorPosition(PitchTile pitchTile, BallGridPoint ballGridPoint, int iteration)
    {
        float offsetX = 0f;
        float offsetZ = 0f;

        
        if (iteration == 1)
        {
            // 1 : 1
            offsetX = -0.5f;
            offsetZ = -0.5f;
            ballGridPoint.gameObject.name = $"Tile - {pitchTile.CoordX} : {pitchTile.CoordZ} | BallPoint 1:1";    

        }
        else if (iteration == 2)
        {
            // 1 : 2
            offsetX = 0f;
            offsetZ = -0.5f;
            ballGridPoint.gameObject.name = $"Tile - {pitchTile.CoordX} : {pitchTile.CoordZ} | BallPoint 1:2";    
        }
        else if (iteration == 3)
        {
            // 1 : 3
            offsetX = 0.5f;
            offsetZ = -0.5f; 
            ballGridPoint.gameObject.name = $"Tile - {pitchTile.CoordX} : {pitchTile.CoordZ} | BallPoint 1:3";   
        }
        else if (iteration == 4)
        {
            // 2 : 1
            offsetX = -0.5f;
            offsetZ = 0f; 
            ballGridPoint.gameObject.name = $"Tile - {pitchTile.CoordX} : {pitchTile.CoordZ} | BallPoint 2:1";   
        }
        else if (iteration == 5)
        {
            // 2 : 2 - Center
            offsetX = 0f;
            offsetZ = 0f; 
            ballGridPoint.gameObject.name = $"Tile - {pitchTile.CoordX} : {pitchTile.CoordZ} | BallPoint 2:2";   
        }
        else if (iteration == 6)
        {
            // 2 : 3
            offsetX = 0.5f;
            offsetZ = 0f; 
            ballGridPoint.gameObject.name = $"Tile - {pitchTile.CoordX} : {pitchTile.CoordZ} | BallPoint 2:3";   
        }
        else if (iteration == 7)
        {
            // 3 : 1
            offsetX = -0.5f;
            offsetZ = 0.5f; 
            ballGridPoint.gameObject.name = $"Tile - {pitchTile.CoordX} : {pitchTile.CoordZ} | BallPoint 3:1"; 
        }
        else if (iteration == 8)
        {
            // 3 : 2
            offsetX = 0f;
            offsetZ = 0.5f; 
            ballGridPoint.gameObject.name = $"Tile - {pitchTile.CoordX} : {pitchTile.CoordZ} | BallPoint 3:2"; 
        }
        else if (iteration == 9)
        {
            // 3 : 3
            offsetX = 0.5f;
            offsetZ = 0.5f; 
            ballGridPoint.gameObject.name = $"Tile - {pitchTile.CoordX} : {pitchTile.CoordZ} | BallPoint 3:3"; 
        }
        
        Vector3 position = new Vector3 (
            pitchTile.transform.position.x + offsetX, 
            ballGridPoint.transform.position.y, 
            pitchTile.transform.position.z + offsetZ);
        
        ballGridPoint.transform.position = position;
    }
    
    /* #endregion */

    /* #region ---- Create Ball Pointer (Follows Mouse on the Pitch in Pass Mode) ------------- */
    private void createBallPointer()
    {
        createPointerPlate();
        createPointer();
        BallPointerSetActive(false);
    }

    private void createPointerPlate()
    {
        PointerPlateObj = MatchManager.InstantiateGameObject(BallPointerPlatePrefab);
        PointerPlateObj.transform.SetParent(this.transform);
        PointerPlateObj.transform.position = new Vector3 (0,0,0);
        PointerPlateObj.GetComponent<MeshRenderer>().enabled = false;
    }

    private void createPointer()
    {
        PointerObj = MatchManager.InstantiateGameObject(BallPointerPrefab);
        PointerObj.transform.SetParent(this.transform);
        PointerObj.transform.position = new Vector3 (0,2,0);
    }




    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== A C T I V A T E  /  D E A C T I V A T E  B A L L G R I D ================== */

    /* #region ---- Activate / Deactivate Ball Grid ------------------------------------------- */
    public void ActivateBallGrid()
    {
        foreach (var ballPoint in BallGridPoints)
        {
            ballPoint.gameObject.SetActive(true);
        }
        BallPointerSetActive(true);
    }

    public void DeactivateBallGrid()
    {
        foreach (var ballPoint in BallGridPoints)
        {
            ballPoint.gameObject.SetActive(false);
        }
        BallPointerSetActive(false);
    }

    /* #endregion */

    /* #region ---- Activate / Deactivate Ball Pointer ---------------------------------------- */
    public void BallPointerSetActive(bool setActive)
    {
        switch(setActive) 
        {
            case true:
                PointerPlateObj.SetActive(true);
                PointerObj.SetActive(true);
                break; 
            case false:
                PointerPlateObj.SetActive(false);
                PointerObj.SetActive(false);
                break; 
        }
    }

    /* #endregion */

    /* #region ---- Activate / Deactivate Ball Pointer ---------------------------------------- */
    public void SetCurrentPoint(BallGridPoint ballPoint)
    {
        this.CurrentPoint = ballPoint;
    }

    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */



}
