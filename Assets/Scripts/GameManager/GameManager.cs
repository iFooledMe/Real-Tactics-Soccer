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
			LoadStartMenu();
			_firstTimeInit = false;
		}
	}
	/* #endregion */
	/* ---------------------------------------------------------------------------------------- */

	/* #region ---- Load Start Menu Scene ----------------------------------------------------- */
	public void LoadStartMenu()
	{
		//SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
	}
	/* #endregion */
	/* ----------------------------------------------------------------------------------------- */
	
	/* #region ---- Load Match Scene ---------------------------------------------------------- */
	public void LoadMatchManager()
	{
		//MatchManager _matchManager = MatchManager.Instance;
	}
	/* #endregion */
	/* ----------------------------------------------------------------------------------------- */

    /* #endregion */
    /* ======================================================================================== */
}
