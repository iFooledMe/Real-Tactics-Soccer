using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void PitchCreatedNote();

public class PitchGrid : SingletonMonoBehaviour<PitchGrid>
{
    
    /* #region ==== FIELDS & PROPERTIES ======================================================= */
    
    /* #region ---- Settings ------------------------------------------------------------------ */
    public PitchSettings PitchSettings;

    /* #endregion */
    
    /* #region ---- Pitch --------------------------------------------------------------------- */
    public GameObject [,] PitchTiles {get; private set;}
    public float XOffset {get; private set;}
    public float ZOffset {get; private set;}

    public event PitchCreatedNote PitchCreated;

    /* #endregion */

    /* #region ---- Move Target Overly -------------------------------------------------------- */
    private GameObject MoveTargetOverlay; 

    /* #endregion */

    /* #region ---- Out Line ------------------------------------------------------------------ */
    [SerializeField]
    private float outLineOffset = 1f; //Offset from outline tile position to define pitch border
    
    public Vector3 outLineNegX {get; private set;} //Bottom Left Corner
    public Vector3 outLinePosX {get; private set;} //Bottom Right Corner
    public Vector3 outLineNegZ {get; private set;} //Top Left Corner
    public Vector3 outLinePosZ {get; private set;} //Top Right Corner

    /* #endregion */
    
    /* #region ---- Prefabs ------------------------------------------------------------------- */
    public GameObject PathLinePrefab; //Accessed by PathFinding class
    public GameObject MoveTargetTileOverlay;
    
    [SerializeField] private GameObject noLines;
    [SerializeField] private GameObject cornerBottomLeft;
    [SerializeField] private GameObject cornerBottomRight;
    [SerializeField] private GameObject cornerTopLeft;
    [SerializeField] private GameObject cornerTopRight;
    [SerializeField] private GameObject sideLineLeft;
    [SerializeField] private GameObject sideLineRight;
    [SerializeField] private GameObject sideLineTop;
    [SerializeField] private GameObject sideLineBottom;

    [SerializeField] private GameObject centerLineBottom;
    [SerializeField] private GameObject centerLineTop;
    [SerializeField] private GameObject centerLineBottomLeft;
    [SerializeField] private GameObject centerLineBottomRight;
    [SerializeField] private GameObject centerLineTopLeft;
    [SerializeField] private GameObject centerLineTopRight;

    [SerializeField] private GameObject circle_bottom_left_1_1;
    [SerializeField] private GameObject circle_bottom_left_1_2;
    [SerializeField] private GameObject circle_bottom_left_1_3;
    [SerializeField] private GameObject circle_bottom_left_2_1;
    [SerializeField] private GameObject circle_bottom_left_2_2;
    [SerializeField] private GameObject circle_bottom_left_2_3;
    [SerializeField] private GameObject circle_bottom_left_3_1;
    [SerializeField] private GameObject circle_bottom_right_1_1;
    [SerializeField] private GameObject circle_bottom_right_1_2;
    [SerializeField] private GameObject circle_bottom_right_1_3;
    [SerializeField] private GameObject circle_bottom_right_2_1;
    [SerializeField] private GameObject circle_bottom_right_2_2;
    [SerializeField] private GameObject circle_bottom_right_2_3;
    [SerializeField] private GameObject circle_bottom_right_3_1;
    
    [SerializeField] private GameObject circle_top_left_1_1;
    [SerializeField] private GameObject circle_top_left_1_2;
    [SerializeField] private GameObject circle_top_left_1_3;
    [SerializeField] private GameObject circle_top_left_2_1;
    [SerializeField] private GameObject circle_top_left_2_2;
    [SerializeField] private GameObject circle_top_left_2_3;
    [SerializeField] private GameObject circle_top_left_3_1;

    [SerializeField] private GameObject circle_top_right_1_1;
    [SerializeField] private GameObject circle_top_right_1_2;
    [SerializeField] private GameObject circle_top_right_1_3;
    [SerializeField] private GameObject circle_top_right_2_1;
    [SerializeField] private GameObject circle_top_right_2_2;
    [SerializeField] private GameObject circle_top_right_2_3;
    [SerializeField] private GameObject circle_top_right_3_1;

    [SerializeField] private GameObject finishYard_s_1_1;
    [SerializeField] private GameObject finishYard_s_1_2;
    [SerializeField] private GameObject finishYard_s_2_1;
    [SerializeField] private GameObject finishYard_s_2_2;
    [SerializeField] private GameObject finishYard_s_2_3;
    [SerializeField] private GameObject finishYard_s_2_4;
    [SerializeField] private GameObject finishYard_s_2_5;
    [SerializeField] private GameObject finishYard_s_2_6;
    [SerializeField] private GameObject finishYard_n_1_1;
    [SerializeField] private GameObject finishYard_n_1_2;
    [SerializeField] private GameObject finishYard_n_2_1;
    [SerializeField] private GameObject finishYard_n_2_2;
    [SerializeField] private GameObject finishYard_n_2_3;
    [SerializeField] private GameObject finishYard_n_2_4;
    [SerializeField] private GameObject finishYard_n_2_5;
    [SerializeField] private GameObject finishYard_n_2_6;

    [SerializeField] private GameObject penaltyBox_s_1_1;
    [SerializeField] private GameObject penaltyBox_s_1_2;
    [SerializeField] private GameObject penaltyBox_s_2_1;
    [SerializeField] private GameObject penaltyBox_s_2_2;
    [SerializeField] private GameObject penaltyBox_s_3_1;
    [SerializeField] private GameObject penaltyBox_s_3_2;
    [SerializeField] private GameObject penaltyBox_s_4_1;
    [SerializeField] private GameObject penaltyBox_s_4_2__and_4_9;
    [SerializeField] private GameObject penaltyBox_s_4_3__and_4_8;
    [SerializeField] private GameObject penaltyBox_s_4_4;
    [SerializeField] private GameObject penaltyBox_s_4_5__and_4_6;
    [SerializeField] private GameObject penaltyBox_s_4_7;
    [SerializeField] private GameObject penaltyBox_s_4_10; 
    [SerializeField] private GameObject penaltyBox_s_5_1;
    [SerializeField] private GameObject penaltyBox_s_5_2;
    [SerializeField] private GameObject penaltyBox_s_5_3;
    [SerializeField] private GameObject penaltyBox_s_5_4;

    [SerializeField] private GameObject penaltyBox_n_1_1;
    [SerializeField] private GameObject penaltyBox_n_1_2;
    [SerializeField] private GameObject penaltyBox_n_2_1;
    [SerializeField] private GameObject penaltyBox_n_2_2;
    [SerializeField] private GameObject penaltyBox_n_3_1;
    [SerializeField] private GameObject penaltyBox_n_3_2;
    [SerializeField] private GameObject penaltyBox_n_4_1;
    [SerializeField] private GameObject penaltyBox_n_4_2__and_4_9;
    [SerializeField] private GameObject penaltyBox_n_4_3__and_4_8;
    [SerializeField] private GameObject penaltyBox_n_4_4;
    [SerializeField] private GameObject penaltyBox_n_4_5__and_4_6;
    [SerializeField] private GameObject penaltyBox_n_4_7;
    [SerializeField] private GameObject penaltyBox_n_4_10; 
    [SerializeField] private GameObject penaltyBox_n_5_1;
    [SerializeField] private GameObject penaltyBox_n_5_2;
    [SerializeField] private GameObject penaltyBox_n_5_3;
    [SerializeField] private GameObject penaltyBox_n_5_4;
    /* #endregion */

    /* #region ---- Dependencies -------------------------------------------------------------- */
    private MatchManager MatchManager;
    private CreateGraph CreateGraph;
    public PathFinding PathFinding {get; private set;}

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
        MatchManager.SetPitchGrid();
    }
    /* #endregion */

    /* #region ---- Set pathfinding ----------------------------------------------------------- */
    void createPathFinding() 
    {
        CreateGraph = new CreateGraph(MatchManager);
        PathFinding = new PathFinding();
        CreateGraph.AddNeighbourTiles();
    }
    /* #endregion */

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
        setMoveTargetTileOverlay();
        OnPitchCreated();
        createPathFinding();
    }

    protected virtual void OnPitchCreated()
    {
        PitchCreated?.Invoke();
    }

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== C R E A T E  P I T C H ==================================================== */
    
    /* #region ---- Set Positional offset (grid created with 0,0,0 as center) ----------------- */
    private void setPosOffset(int pitchWidth, int PitchLength) 
    {
        XOffset = (pitchWidth/2) + 0.5f;
        ZOffset = (PitchLength/2) + 0.5f;
    }
    /* #endregion */
    
    /* #region ---- CreatePitch --------------------------------------------------------------- */
    private void createPitch() 
    {     
        int pitchWidth = MatchManager.PitchGrid.PitchSettings.PitchWidth;
        int pitchLength = MatchManager.PitchGrid.PitchSettings.PitchLength;
        setPosOffset(pitchWidth, pitchLength);
        PitchTiles = new GameObject [pitchWidth + 1, pitchLength + 1];

        for (int x = 1; x <= pitchWidth; x++) 
        {
            for (int z = 1; z <= pitchLength; z++) 
            {    
                GameObject pitchTileObj = MatchManager.InstantiateGameObject(returnPitchTilePrefab(x, z));
                pitchTileObj.transform.position = new Vector3(
                pitchTileObj.transform.position.x - XOffset + x, 
                pitchTileObj.transform.position.y,
                pitchTileObj.transform.position.z - ZOffset + z);
                pitchTileObj.name = "Pitch tile - " + x + ":" + z;
                pitchTileObj.transform.SetParent(this.transform);
                
                PitchTile pitchTile = pitchTileObj.GetComponent<PitchTile>();   
                pitchTile.SetCoodinates(x, z);
                pitchTile.setUnOccupied();
                PitchTiles[x,z] = pitchTileObj;

                getOutLineTiles(pitchTile, x, z);
            }
        }
    }
    /* #endregion */

    /* #region ---- Return a tile prefab for position ----------------------------------------- */
    GameObject returnPitchTilePrefab (int x, int z) 
    {
        int pitchWidth = MatchManager.PitchGrid.PitchSettings.PitchWidth;
        int pitchLength = MatchManager.PitchGrid.PitchSettings.PitchLength;
        
        /* #region --- Row - PenaltyBox 1 South-------------------- */
        if (z == 1) {
            //Corner tiles
            if (x == 1) 
            {
                return cornerBottomLeft;
            }
            else if (x == pitchWidth) 
            {
                return cornerBottomRight;
            }
            //Penalty box tiles
            else if (x == (pitchWidth/2) - 4) 
            {
                return penaltyBox_s_1_1;
            }
            else if (x == (pitchWidth/2) + 5)
            {
                return penaltyBox_s_1_2;
            }
            //Finish yard tiles
            else if (x == (pitchWidth/2) - 2) 
            {
                return finishYard_s_1_1;
            }
            else if (x == (pitchWidth/2) + 3) 
            {
                return finishYard_s_1_2;
            }            
            //No Lines
            else 
            {
                return sideLineBottom;
            }
        }
        /* #endregion */
        /* #region --- Row - PenaltyBox 2 South-------------------- */
        else if (z == 2) 
        {
            //Sidelines
            if (x == 1) 
            {
                return sideLineLeft;
            }
            else if (x == pitchWidth) 
            {
                return sideLineRight;
            }
            //Penalty box tiles
            else if (x == (pitchWidth/2) - 4) 
            {
                return penaltyBox_s_2_1;
            }
            else if (x == (pitchWidth/2) + 5)
            {
                return penaltyBox_s_2_2;
            }
            //Finish yard tiles
            else if (x == (pitchWidth/2) - 2) 
            {
                return finishYard_s_2_1;
            }
            else if (x == (pitchWidth/2) - 1) 
            {
                return finishYard_s_2_2;
            }
            else if (x == (pitchWidth/2)) 
            {
                return finishYard_s_2_3;
            }
            else if (x == (pitchWidth/2) + 1) 
            {
                return finishYard_s_2_4;
            }
            else if (x == (pitchWidth/2) + 2) 
            {
                return finishYard_s_2_5;
            }
            else if (x == (pitchWidth/2) + 3) 
            {
                return finishYard_s_2_6;
            }
            //No Lines       
            else  
            {
                return noLines;
            }
        }
        /* #endregion */
        /* #region --- Row - PenaltyBox 3 South-------------------- */
        else if (z == 3) 
        {
            //Sidelines
            if (x == 1) 
            {
                return sideLineLeft;
            }
            else if (x == pitchWidth) 
            {
                return sideLineRight;
            }
            //Penalty box tiles
            else if (x == (pitchWidth/2) - 4)
            {
                return penaltyBox_s_3_1;
            }
            else if (x == (pitchWidth/2) + 5)
            {
                return penaltyBox_s_3_2;
            }
            //No Lines
            else  
            {
                return noLines;
            }
        }
        /* #endregion */
        /* #region --- Row - PenaltyBox 4 South-------------------- */
        else if (z == 4) 
        {
            //Sidelines
            if (x == 1) 
            {
                return sideLineLeft;
            }
            else if (x == pitchWidth) 
            {
                return sideLineRight;
            }
            //Penalty box tiles
            else if (x == (pitchWidth/2) - 4)
            {
                return penaltyBox_s_4_1;
            }
            else if (x == (pitchWidth/2) - 3 || x == (pitchWidth/2) + 4)
            {
                return penaltyBox_s_4_2__and_4_9;
            }
            else if (x == (pitchWidth/2) - 2 || x == (pitchWidth/2) + 3)
            {
                return penaltyBox_s_4_3__and_4_8;
            }
            else if (x == (pitchWidth/2) - 1)
            {
                return penaltyBox_s_4_4;
            }
            else if (x == (pitchWidth/2) || x == (pitchWidth/2) + 1)
            {
                return penaltyBox_s_4_5__and_4_6;
            }
            else if (x == (pitchWidth/2) + 2)
            {
                return penaltyBox_s_4_7;
            }
            else if (x == (pitchWidth/2) + 5)
            {
                return penaltyBox_s_4_10;
            }
            //No Lines
            else  
            {
                return noLines;
            }
        }
        /* #endregion */
        /* #region --- Row - PenaltyBox 5 South-------------------- */
        else if (z == 5) 
        {
            //Sidelines
            if (x == 1) 
            {
                return sideLineLeft;
            }
            else if (x == pitchWidth) 
            {
                return sideLineRight;
            }
            //Penalty box tiles
            else if (x == (pitchWidth/2) - 1)
            {
                return penaltyBox_s_5_1;
            }
            else if (x == (pitchWidth/2))
            {
                return penaltyBox_s_5_2;
            }
            else if (x == (pitchWidth/2) + 1)
            {
                return penaltyBox_s_5_3;
            }
            else if (x == (pitchWidth/2) + 2)
            {
                return penaltyBox_s_5_4;
            }
            //No Lines
            else  
            {
                return noLines;
            }
        }
        /* #endregion */
        /* #region --- Row - PenaltyBox to Circle) South ---------- */
        else if (z > 5 && z <= (pitchLength/2)-3) 
        {
            //Sidelines
            if (x == 1) 
            {
                return sideLineLeft;
            }
            else if (x == pitchWidth) 
            {
                return sideLineRight;
            }
            //No Lines
            else  
            {
                return noLines;
            }
        }
        /* #endregion */
        /* #region --- Row - Circle South 1 ----------------------- */
        else if (z == (pitchLength/2)-2) 
        {
            //Sidelines
            if (x == 1) 
            {
                return sideLineLeft;
            }
            else if (x == pitchWidth) 
            {
                return sideLineRight;
            }
            // Middle Circle 
            else if (x == pitchWidth/2) 
            {
                return circle_bottom_left_3_1;
            }
            else if (x == (pitchWidth/2) + 1) 
            {
                return circle_bottom_right_3_1;
            }
            //No Lines 
            else 
            {
                return noLines;
            }
        }
        /* #endregion */
        /* #region --- Row - Circle South 2 ----------------------- */
        else if (z == (pitchLength/2)-1) 
        {
            //Sidelines
            if (x == 1) 
            {
                return sideLineLeft;
            }
            else if (x == pitchWidth) 
            {
                return sideLineRight;
            }
            // Middle Circle 
            else if (x == pitchWidth/2) 
            {
                return circle_bottom_left_2_1;
            }
            else if (x == (pitchWidth/2) - 1) 
            {
                return circle_bottom_left_2_2;
            }
            else if (x == (pitchWidth/2) - 2) 
            {
                return circle_bottom_left_2_3;
            }
            else if (x == (pitchWidth/2) +1) 
            {
                return circle_bottom_right_2_1;
            }
            else if (x == (pitchWidth/2) + 2) 
            {
                return circle_bottom_right_2_2;
            }
            else if (x == (pitchWidth/2) + 3) 
            {
                return circle_bottom_right_2_3;
            }
            //No Lines 
            else 
            {
                return noLines;
            }
        }
        /* #endregion */
        /* #region --- Row - Circle South 3 (Center Line South) --- */
        else if (z == (pitchLength/2)) 
        {
            //Centerline sides
            if (x == 1) 
            {
                return centerLineBottomLeft;
            }
            else if (x == pitchWidth) 
            {
                return centerLineBottomRight;
            }
            // Middle Circle 
            else if (x == pitchWidth/2) 
            {
                return circle_bottom_left_1_1;
            }
            else if (x == (pitchWidth/2) - 1) 
            {
                return circle_bottom_left_1_2;
            }
            else if (x == (pitchWidth/2) - 2) 
            {
                return circle_bottom_left_1_3;
            }
            else if (x == (pitchWidth/2) +1) 
            {
                return circle_bottom_right_1_1;
            }
            else if (x == (pitchWidth/2) + 2) 
            {
                return circle_bottom_right_1_2;
            }
            else if (x == (pitchWidth/2) + 3) 
            {
                return circle_bottom_right_1_3;
            }
            //Center line south 
            else 
            {
                return centerLineBottom;
            } 
        }
        /* #endregion */
        /* #region --- Row - Circle North 3 (Center Line North) --- */
        else if (z == (pitchLength/2) + 1) 
        {
            //Centerline sides
            if (x == 1) 
            {
                return centerLineTopLeft;
            }
            else if (x == pitchWidth) 
            {
                return centerLineTopRight;
            }
            // Middle Circle 
            else if (x == pitchWidth/2) 
            {
                return circle_top_left_1_1;
            }
            else if (x == (pitchWidth/2) - 1) 
            {
                return circle_top_left_1_2;
            }
            else if (x == (pitchWidth/2) - 2) 
            {
                return circle_top_left_1_3;
            }
            else if (x == (pitchWidth/2) +1) 
            {
                return circle_top_right_1_1;
            }
            else if (x == (pitchWidth/2) + 2) 
            {
                return circle_top_right_1_2;
            }
            else if (x == (pitchWidth/2) + 3) 
            {
                return circle_top_right_1_3;
            }
            //Center line south 
            else 
            {
                return centerLineTop;
            } 
        }
        /* #endregion */
        /* #region --- Row - Circle North 2 ----------------------- */
        else if (z == (pitchLength/2) + 2) 
        {
            //Sidelines
            if (x == 1) 
            {
                return sideLineLeft;
            }
            else if (x == pitchWidth) 
            {
                return sideLineRight;
            }
            // Middle Circle 
            else if (x == pitchWidth/2) 
            {
                return circle_top_left_2_1;
            }
            else if (x == (pitchWidth/2) - 1) 
            {
                return circle_top_left_2_2;
            }
            else if (x == (pitchWidth/2) - 2) 
            {
                return circle_top_left_2_3;
            }
            else if (x == (pitchWidth/2) +1) 
            {
                return circle_top_right_2_1;
            }
            else if (x == (pitchWidth/2) + 2) 
            {
                return circle_top_right_2_2;
            }
            else if (x == (pitchWidth/2) + 3) 
            {
                return circle_top_right_2_3;
            }
            //No Lines 
            else 
            {
                return noLines;
            }
        }
        /* #endregion */
        /* #region --- Row - Circle North 1 ----------------------- */
        else if (z == (pitchLength/2) + 3) 
        {
            //Sidelines
            if (x == 1) 
            {
                return sideLineLeft;
            }
            else if (x == pitchWidth) 
            {
                return sideLineRight;
            }
            // Middle Circle 
            else if (x == pitchWidth/2) 
            {
                return circle_top_left_3_1;
            }
            else if (x == (pitchWidth/2) + 1) 
            {
                return circle_top_right_3_1;
            }
            //No Lines 
            else 
            {
                return noLines;
            }
        }
        /* #endregion */
        /* #region --- Row - PenaltyBox to Circle) North ---------- */ 
        else if (z >= (pitchLength/2) + 4 && z <= pitchLength - 5) 
        {
            //Sidelines
            if (x == 1) 
            {
                return sideLineLeft;
            }
            else if (x == pitchWidth) 
            {
                return sideLineRight;
            }
            //No Lines
            else  
            {
                return noLines;
            }
        }
        /* #endregion */
        /* #region --- Row - PenaltyBox 5 North ------------------- */
        else if (z == pitchLength - 4) 
        {
            //Sidelines
            if (x == 1) 
            {
                return sideLineLeft;
            }
            else if (x == pitchWidth) 
            {
                return sideLineRight;
            }
            //Penalty box tiles
            else if (x == (pitchWidth/2) - 1)
            {
                return penaltyBox_n_5_1;
            }
            else if (x == (pitchWidth/2))
            {
                return penaltyBox_n_5_2;
            }
            else if (x == (pitchWidth/2) + 1)
            {
                return penaltyBox_n_5_3;
            }
            else if (x == (pitchWidth/2) + 2)
            {
                return penaltyBox_n_5_4;
            }
            //No Lines
            else  
            {
                return noLines;
            }
        }
        /* #endregion */
        /* #region --- Row - PenaltyBox 4 North ------------------- */
        else if (z == pitchLength - 3) 
        {
            //Sidelines
            if (x == 1) 
            {
                return sideLineLeft;
            }
            else if (x == pitchWidth) 
            {
                return sideLineRight;
            }
            //Penalty box tiles
            else if (x == (pitchWidth/2) - 4)
            {
                return penaltyBox_n_4_1;
            }
            else if (x == (pitchWidth/2) - 3 || x == (pitchWidth/2) + 4)
            {
                return penaltyBox_n_4_2__and_4_9;
            }
            else if (x == (pitchWidth/2) - 2 || x == (pitchWidth/2) + 3)
            {
                return penaltyBox_n_4_3__and_4_8;
            }
            else if (x == (pitchWidth/2) - 1)
            {
                return penaltyBox_n_4_4;
            }
            else if (x == (pitchWidth/2) || x == (pitchWidth/2) + 1)
            {
                return penaltyBox_n_4_5__and_4_6;
            }
            else if (x == (pitchWidth/2) + 2)
            {
                return penaltyBox_n_4_7;
            }
            else if (x == (pitchWidth/2) + 5)
            {
                return penaltyBox_n_4_10;
            }
            //No Lines
            else  
            {
                return noLines;
            }
        }
        /* #endregion */
        /* #region --- Row - PenaltyBox 3 North ------------------- */
        else if (z == pitchLength - 2) 
        {
            //Sidelines
            if (x == 1) 
            {
                return sideLineLeft;
            }
            else if (x == pitchWidth) 
            {
                return sideLineRight;
            }
            //Penalty box tiles
            else if (x == (pitchWidth/2) - 4)
            {
                return penaltyBox_n_3_1;
            }
            else if (x == (pitchWidth/2) + 5)
            {
                return penaltyBox_n_3_2;
            }
            //No Lines
            else  
            {
                return noLines;
            }
        }
        /* #endregion */
        /* #region --- Row - PenaltyBox 2 North-------------------- */
        else if (z == pitchLength - 1)
        {
            //Sidelines
            if (x == 1) 
            {
                return sideLineLeft;
            }
            else if (x == pitchWidth) 
            {
                return sideLineRight;
            }
            //Penalty box tiles
            else if (x == (pitchWidth/2) - 4) 
            {
                return penaltyBox_n_2_1;
            }
            else if (x == (pitchWidth/2) + 5)
            {
                return penaltyBox_n_2_2;
            }
            //Finish yard tiles
            else if (x == (pitchWidth/2) - 2) 
            {
                return finishYard_n_2_1;
            }
            else if (x == (pitchWidth/2) - 1) 
            {
                return finishYard_n_2_2;
            }
            else if (x == (pitchWidth/2)) 
            {
                return finishYard_n_2_3;
            }
            else if (x == (pitchWidth/2) + 1) 
            {
                return finishYard_n_2_4;
            }
            else if (x == (pitchWidth/2) + 2) 
            {
                return finishYard_n_2_5;
            }
            else if (x == (pitchWidth/2) + 3) 
            {
                return finishYard_n_2_6;
            }
            //No Lines       
            else  
            {
                return noLines;
            }
        }
        /* #endregion */
        /* #region --- Row - PenaltyBox 1 North-------------------- */
        if (z == pitchLength) {
            //Corner tiles
            if (x == 1) 
            {
                return cornerTopLeft;
            }
            else if (x == pitchWidth) 
            {
                return cornerTopRight;
            }
            //Penalty box tiles
            else if (x == (pitchWidth/2) - 4) 
            {
                return penaltyBox_n_1_1;
            }
            else if (x == (pitchWidth/2) + 5)
            {
                return penaltyBox_n_1_2;
            }
            //Finish yard tiles
            else if (x == (pitchWidth/2) - 2) 
            {
                return finishYard_n_1_1;
            }
            else if (x == (pitchWidth/2) + 3) 
            {
                return finishYard_n_1_2;
            }            
            //No Lines
            else 
            {
                return sideLineTop;
            }
        }
        /* #endregion */
        /* #region --- Else - No Lines ---------------------------- */
        else  
        {
            return noLines;
        }
        /* #endregion */
    }               
    /* #endregion */

    /* #region ---- Set Outline tiles --------------------------------------------------------- */
    private void getOutLineTiles(PitchTile tile, int x, int z)
    {
        int pitchWidth = MatchManager.PitchGrid.PitchSettings.PitchWidth;
        int pitchLength = MatchManager.PitchGrid.PitchSettings.PitchLength;

        Vector3 tilePosition = tile.transform.position;

        if (x == 1 && z == pitchLength/2)
        {
            tilePosition.x -= outLineOffset;
            outLineNegX = tilePosition;
        }
        
        if (x == pitchWidth && z == pitchLength/2)
        {
            tilePosition.x += outLineOffset;
            outLinePosX = tilePosition;
        } 
        
        if (z == 1 && x == pitchWidth/2)
        {
            tilePosition.z -= outLineOffset;
            outLineNegZ = tilePosition;
        }
        
        if (z == pitchLength && x == pitchWidth/2)
        {
            tilePosition.z += outLineOffset;
            outLinePosZ = tilePosition;
        } 
    }

    /* #endregion */

    /* #region ---- Set Move Target Overlay --------------------------------------------------- */
    private void setMoveTargetTileOverlay()
    {
        MoveTargetOverlay = (GameObject)Instantiate(MoveTargetTileOverlay);
        MoveTargetOverlay.SetActive(false);
    }

    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== GENERAL HELPERS =========================================================== */
    
    /* #region ---- Activate/Deactivate Move Target Overlay ----------------------------------- */
    //TODO: Should Activation of Move target overlay be in PitchGrid?
    public void ActivateMoveTargetOverlay(Vector3 targetPos)
    {
        MoveTargetOverlay.SetActive(true);
        float posX = targetPos.x;
        float posY = MoveTargetOverlay.transform.position.y;
        float posZ = targetPos.z;
        
        Vector3 newPosition = new Vector3(posX, posY, posZ);

        MoveTargetOverlay.transform.position = newPosition;
    }

    public void DeactivateMoveTargetOverlay()
    {
        MoveTargetOverlay.SetActive(false);
    }




    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */

}
