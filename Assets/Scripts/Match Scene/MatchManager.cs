using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchManager : SingletonScriptableObject<MatchManager>
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */ 
    
    /* #region ---- Dependencies -------------------------------------------------------------- */
    public GameManager GameManager {get; private set;}
    public PitchManager PitchManager {get; private set;}

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== GET DEPENDENCIES ========================================================== */
    private void getDependenciesBeforeLoadMatch()
    {
        GameManager = GameManager.Instance;
    }

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== ON ENABLE ================================================================= */
    void OnEnable()
    {
        getDependenciesBeforeLoadMatch();
        GameManager.RefTest("MatchManager");
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
