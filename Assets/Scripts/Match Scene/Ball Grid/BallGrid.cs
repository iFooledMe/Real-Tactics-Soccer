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
    public List<BallGridPoint> BallGridPointList;

    /* #endregion */
    
    /* #region ---- Prefabs ------------------------------------------------------------------- */
    [SerializeField] private GameObject BallPrefab;
    [SerializeField] private GameObject BallGridPointPrefab;

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

    /* #region ==== C R E A T E  B A L L  G R I D ============================================= */
    private void createBallGrid()
    {
        int iterations = 0;
        string maxRightTop = "none";

        int pitchWidth = MatchManager.PitchManager.PitchWidth;
        int pitchLength = MatchManager.PitchManager.PitchLength;
        
        for (int x = 1; x <= pitchWidth; x++) {
            for (int z = 1; z <= pitchLength; z++) 
            {  
                if (x == pitchWidth && z == pitchLength)
                {
                    iterations = 9;
                }
                else if (x == pitchWidth)
                {
                    iterations = 6;
                    maxRightTop = "maxRight";

                }
                else if (z == pitchLength)
                {
                    iterations = 6;
                    maxRightTop = "maxTop";

                }
                else
                {
                    iterations = 4;
                }





                PitchTile pitchTile = MatchManager.PitchManager.GetPitchTile(x, z);
                createBallGridPoints(pitchTile, iterations, maxRightTop);
                
  
            }
        }
    }

    /* #region ---- Set tempList -------------------------------------------------------------- */
    private void createBallGridPoints(PitchTile pitchTile, int iterations, string maxRightTop)
    {
        for (int x = 1; x <= iterations; x++) 
        {
            GameObject ballGridPoint = MatchManager.InstantiateGameObject(BallGridPointPrefab);
            setVectorPosition(pitchTile, ballGridPoint, x, maxRightTop);
        }

    }

    /* #endregion */

    /* #region ---- Set player Vector position ------------------------------------------------ */
    private void setVectorPosition(PitchTile pitchTile, GameObject ballGridPoint, int iteration, string maxRightTop)
    {
        float offsetX = 0f;
        float offsetZ = 0f;

        
        if (iteration == 1)
        {
            // --- Left Bottom
            offsetX = -0.5f;
            offsetZ = -0.5f;    

        }
        else if (iteration == 2)
        {
            // --- Middle Bottom
            offsetX = 0f;
            offsetZ = -0.5f; 
        }
        else if (iteration == 3)
        {
            // --- Left Center
            offsetX = -0.5f;
            offsetZ = 0f;
        }
        else if (iteration == 4)
        {
            // --- Center Center
            offsetX = 0f;
            offsetZ = 0f;
        }
        else if (iteration == 5)
        {
            if (maxRightTop == "maxRight")
            {
                // --- Right Bottom
                offsetX = 0.5f;
                offsetZ = -0.5f;
            }
            else if (maxRightTop == "maxTop")
            {
                // --- Left Top
                offsetX = -0.5f;
                offsetZ = 0.5f;
            }

        }
        else if (iteration == 6)
        {
            if (maxRightTop == "maxRight")
            {
                // --- Right Center
                offsetX = 0.5f;
                offsetZ = 0f;
            }
            else if (maxRightTop == "maxTop")
            {
                // --- Center Top
                offsetX = 0f;
                offsetZ = 0.5f;
            }
        }
        else if (iteration == 7)
        {
            // --- TopRightCorner - Left Top
            offsetX = -0.5f;
            offsetZ = 0.5f;
        }
        else if (iteration == 8)
        {
            // --- TopRightCorner - Center Top
            offsetX = 0f;
            offsetZ = 0.5f;
        }
        else if (iteration == 9)
        {
            // --- TopRightCorner - Right Top
            offsetX = 0.5f;
            offsetZ = 0.5f;
        }
        

        
        GameObject pitchTileObj = pitchTile.gameObject;

        Vector3 position = new Vector3 (
            pitchTileObj.transform.position.x + offsetX, 
            ballGridPoint.transform.position.y, 
            pitchTileObj.transform.position.z + offsetZ);
        
        ballGridPoint.transform.position = position;
    }
    
    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */
}
