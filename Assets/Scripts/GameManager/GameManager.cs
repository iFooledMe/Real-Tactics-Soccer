using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonScriptableObject<GameManager>
{
	/* #region ==== FIELDS & PROPERTIES ======================================================= */
	
    /* #region ---- Misc Fields --------------------------------------------------------------- */
	private bool _firstTimeInit = true;
    
    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */
	
	/* #endregion */
    /* ======================================================================================== */

    /* #region ==== GET DEPENDENCIES ========================================================== */
    


	/* #endregion */
    /* ======================================================================================== */

	/* #region ==== ON ENABLE ================================================================= */
	private void OnEnable() 
	{
		checkFirstTimeInit();
	}

	/* #endregion */
    /* ======================================================================================== */

	/* #region ==== LOAD SCENES =============================================================== */
    
	/* #region ---- Check for first time init ------------------------------------------------- */
	private void checkFirstTimeInit()
	{
		if(_firstTimeInit)
		{
			LoadMainMenu();
			_firstTimeInit = false;
		}
	}
	/* #endregion */
	/* ---------------------------------------------------------------------------------------- */

	/* #region ---- Load Start Menu Scene ----------------------------------------------------- */
	public void LoadMainMenu()
	{
		SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
	}
	/* #endregion */
	/* ----------------------------------------------------------------------------------------- */
	
	/* #region ---- Load Match Scene ---------------------------------------------------------- */
	public void LoadMatchManager()
	{
		MatchManager _matchManager = MatchManager.Instance;
	}
	/* #endregion */
	/* ---------------------------------------------------------------------------------------- */

    /* #endregion */
    /* ======================================================================================== */

    public void RefTest(string fromClass)
    {
        Debug.Log("GameManager reference from " + fromClass + " - OK!");
    }
}
