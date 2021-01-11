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

    /* #region ---- Public Properties --------------------------------------------------------- */
    

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */
    
    /* #region ---- Dependencies -------------------------------------------------------------- */
    public GameManager GameManager {get; private set;}
    public PitchManager PitchManager {get; private set;}
    public PitchGrid PitchGrid {get; private set;}

    public CameraManager CameraManager {get; private set;}
    public MainCameraControler MainCameraControler {get; private set;}

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== GET DEPENDENCIES ========================================================== */
    private void preSceneLoads()
    {
        GameManager = GameManager.Instance;
        PitchManager = PitchManager.Instance;
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
        SceneManager.LoadScene("Match", LoadSceneMode.Single);
    }
    /* #endregion */
    /* ======================================================================================== */
    
    
    public void RefTest(string fromClass)
    {
        Debug.Log("MatchManager reference from " + fromClass + " - OK!");
    }

}
