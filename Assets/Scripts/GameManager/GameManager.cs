using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonScriptableObject<GameManager>
{
	/* #region ==== FIELDS & PROPERTIES ======================================================= */
	
    /* #region ---- Misc Fields --------------------------------------------------------------- */
	private bool _firstTimeInit = true;
    
    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

	//TODO: List<Team> Teams is temporary here. Move to a separate class to set up a game
	/* #region ---- Create Team --------------------------------------------------------------- */
	public List<Team> Teams {get; private set;}
    
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
		
		//TODO: createTeam() is temporary here. Move to a separate class to set up a game
		createTeam("Aik");
	}

	/* #endregion */
    /* ======================================================================================== */

	/* #region ==== CREATE TEAM =============================================================== */
	private void createTeam(string teamName) 
	{
		bool teamNameExist = false;
		
		if (Teams == null)
		{
			Teams = new List<Team>();
		}

		foreach(var team in Teams)
		{
			if (team.Name == teamName)
			{
				Debug.Log($"The Team name {teamName} already exist");
				teamNameExist = true;
				return;
			}
		}

		if(!teamNameExist)
		{
			Team newTeam = new Team(teamName);
			Teams.Add(newTeam);
		}
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

}
