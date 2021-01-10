using UnityEngine;

public class MainCameraControler : SingletonMonoBehaviour<MainCameraControler>
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */
    [SerializeField] float distanceFromEdgeShortSide = 7; 
    [SerializeField] float distanceFromEdgeLongtSide = 25; 
    [SerializeField] float fieldOfViewShortSide = 50; 
    [SerializeField] float fieldOfViewLongSide = 30; 
    GameObject pitchGridInstance;
    int pitchWidth;
    int pitchLength;

    Vector3 originalPos;

    MatchManager MatchManager;

    /* #endregion */  
    /* ======================================================================================== */
    
    /* #region ==== GET REFERENCES ============================================================ */
    void getReferencesOnAwake() 
    {
        getSetMatchManager();
    }

    /* #region ---- Get/Set MatchManager (Get Instance and sets itself as ref on the same ----- */
    public void getSetMatchManager()
    {
        MatchManager = MatchManager.Instance;
        MatchManager.SetMainCameraController();
    }
    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */
    
    /* #endregion */
    /* ======================================================================================== */
    
    /* #region ==== AWAKE / START ============================================================= */
    void Awake() 
    {
        getReferencesOnAwake();
    }

    void Start()
    {
        getPitchSize();
        setStartPos();
    }

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== SET CAMERA START POSITION ================================================= */
	private void setStartPos()
	{
        resetCameraPos();

        //Apply positional offset
        this.transform.position = new Vector3(
            this.transform.position.x, 
            this.transform.position.y,
            this.transform.position.z - pitchLength/2 - distanceFromEdgeShortSide);
            Camera.main.fieldOfView = fieldOfViewShortSide;
	}
    /* #endregion */  
    /* ======================================================================================== */

    /* #region ==== UPDATE CAMERA POSITION ==================================================== */
    
    /* #region ---- Get/Set new camera position ----------------------------------------------- */
    public void UpdateCamera(CameraManager.CameraPosition camPos) 
    {
        float xOffset; // On Width - Long sides
        float yOffset; // Height
        float zOffset; // On Length - Short sides
        
        switch (camPos) 
        {
        
        // South
        case CameraManager.CameraPosition.South:
            setStartPos();
            break;
        
        // East
        case CameraManager.CameraPosition.East:
            xOffset = pitchWidth/2 + distanceFromEdgeLongtSide;
            yOffset = 0;
            zOffset = 0;
            
            TransformNewPosition(xOffset, yOffset, zOffset);
            Camera.main.fieldOfView = fieldOfViewLongSide;
            break;
        

        // West
        case CameraManager.CameraPosition.West:
            xOffset = -pitchWidth/2 - distanceFromEdgeLongtSide;
            yOffset = 0;
            zOffset = 0;
            
            TransformNewPosition(xOffset, yOffset, zOffset);
            Camera.main.fieldOfView = fieldOfViewLongSide;
            break;
        

        // North
        case CameraManager.CameraPosition.North:
            xOffset = 0;
            yOffset = 0;
            zOffset = pitchLength/2 + distanceFromEdgeShortSide;
            
            TransformNewPosition(xOffset, yOffset, zOffset);
            Camera.main.fieldOfView = fieldOfViewShortSide;
            break;
        }
    }
    /* #endregion */  
    /* ---------------------------------------------------------------------------------------- */

    /* #region ---- Transform New Position ---------------------------------------------------- */
    private void TransformNewPosition(float xOffset, float yOffset, float zOffset) 
    {
        resetCameraPos();

        //Apply positional offset
        this.transform.position = new Vector3(
            transform.position.x + xOffset, 
            transform.position.y + yOffset,
            transform.position.z + zOffset);
    }
    /* #endregion */  
    /* ---------------------------------------------------------------------------------------- */
    
    /* #endregion */  
    /* ======================================================================================== */

    /* #region ==== GENERAL HELPER FUNCTIONS ================================================== */
    
    /* #region ---- Reset Camera Position ----------------------------------------------------- */
    private void resetCameraPos() 
    {
        //Reset Camera position to center
        this.transform.position = new Vector3(0,17,0);
    }
    /* #endregion */  
    /* ---------------------------------------------------------------------------------------- */

    /* #region ---- Get pitch size (Width & Length) ------------------------------------------- */
    void getPitchSize() 
    {
        pitchWidth = MatchManager.PitchManager.PitchWidth;
        pitchLength = MatchManager.PitchManager.PitchLength;
    }
    /* #endregion */  
    /* ---------------------------------------------------------------------------------------- */

    /* #endregion */
    /* ======================================================================================== */

}
