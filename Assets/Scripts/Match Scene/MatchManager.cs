using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchManager : SingletonScriptableObject<MatchManager>
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */ 
    
    /* #region ---- Match states ------------------------------------------------------------- */
    private bool _playInAction = false;
    public bool PlayInAction { get => _playInAction; set => _playInAction = value; }

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */
    
    /* #region ---- Dependencies -------------------------------------------------------------- */
    public GameManager GameManager {get; private set;}
    
    public PitchManager PitchManager {get; private set;}
    public PitchGrid PitchGrid {get; private set;}

    public CameraManager CameraManager {get; private set;}
    public MainCameraControler MainCameraControler {get; private set;}

    public MatchTeamManager MatchTeamManager {get; private set;}
    public MatchPlayerManager MatchPlayerManager {get; private set;}

    public PlayerInput PlayerInput {get; private set;}

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== GET DEPENDENCIES ========================================================== */
    private void preSceneLoads()
    {
        GameManager = GameManager.Instance;
    }

    /* #region ---- Classes call theese functions to set themselfs as ref here on Instantiation */
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

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

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

    /* #region ==== LOAD MATCH ================================================================ */
    void loadMatchScene() 
    {
        this.PitchManager = PitchManager.Instance;
        this.MatchTeamManager = MatchTeamManager.Instance;
        this.PlayerInput = PlayerInput.Instance;
        MatchTeamManager.SetupTeams ("Aik", "None");
        SceneManager.LoadScene("Match", LoadSceneMode.Single);
    }
    /* #endregion */
    /* ======================================================================================== */

}
