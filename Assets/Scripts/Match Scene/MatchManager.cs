﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchManager : SingletonScriptableObject<MatchManager>
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */ 
        
    /* #region ---- Match states -------------------------------------------------------------- */
    public int Score = 0;

    /* #endregion */

    /* #region ---- Dependencies -------------------------------------------------------------- */
    public GameManager GameManager {get; private set;}
    
    public PitchManager PitchManager {get; private set;}
    public PitchGrid PitchGrid {get; private set;}
    public GridOverlay GridOverlay {get; private set;}

    public CameraManager CameraManager {get; private set;}
    public MainCameraControler MainCameraControler {get; private set;}

    public MatchTeamManager MatchTeamManager {get; private set;}
    public MatchPlayerManager MatchPlayerManager {get; private set;}

    public MatchPlayerInput MatchPlayerInput {get; private set;}

    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== GET DEPENDENCIES ========================================================== */
    private void preSceneLoads()
    {
        GameManager = GameManager.Instance;
    }

    /* #region ---- Classes call theese functions to set themselfs as ref here on Instantiation */
    //TODO: Set up observer pattern with events instead
    public void SetPitchGrid()
    {
        this.PitchGrid = PitchGrid.Instance;
    }

    public void SetCameraManager()
    {
        this.CameraManager = CameraManager.Instance;
    }

    public void SetMainCameraController()
    {
        this.MainCameraControler = MainCameraControler.Instance;
    }

    public void SetMatchPlayerManager()
    {
        this.MatchPlayerManager = MatchPlayerManager.Instance;
    }

    public void SetGridOverLay()
    {
        this.GridOverlay = GridOverlay.Instance;
    }

    public void SetMatchPlayerInput()
    {
        this.MatchPlayerInput = MatchPlayerInput.Instance;
    }

    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== ON ENABLE ================================================================= */
    void OnEnable()
    {
        preSceneLoads();
        loadMatchScene();    
    }
    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== L O A D  M A T C H ======================================================== */
    void loadMatchScene() 
    {
        loadPitchManager();
        loadMatchTeamManager();
        
        MatchTeamManager.SetupTeams ("Aik", "None");
        SceneManager.LoadScene("Match", LoadSceneMode.Single);
    }

    /* #region ---- Load PitchManager -------------------------------------------------------- */
    private void loadPitchManager()
    {
        if(PitchManager != null)
		{
			PitchManager.ResetInstance();
		}
		else
		{
			PitchManager = ScriptableObject.CreateInstance<PitchManager>();
		}
    }
    /* #Endregion */

    /* #region ---- Load MatchTeamManager ---------------------------------------------------- */
    private void loadMatchTeamManager()
    {
        if(MatchTeamManager != null)
		{
			MatchTeamManager.ResetInstance();
		}
		else
		{
			MatchTeamManager = ScriptableObject.CreateInstance<MatchTeamManager>();
		}
    }
    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== G E N E R A L  H E L P E R S ============================================== */
    
    /* #region ---- Destroy GameObject -------------------------------------------------------- */
    public void DestroyObjectsByTag(string tag) 
    {
        Destroy(GameObject.FindGameObjectWithTag(tag));
    }
    /* #endregion */

    /* #region ---- Instantiate GameObject ---------------------------------------------------- */
    public GameObject InstantiateGameObject(GameObject prefab) 
    {
        GameObject _pitchTileObject = (GameObject)Instantiate(prefab);
        return _pitchTileObject;
    }

    /* #endregion */
    /* ======================================================================================== */

}
