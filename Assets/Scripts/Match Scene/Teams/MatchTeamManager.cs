using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchTeamManager : SingletonScriptableObject<MatchTeamManager>
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */
    
    /* #region ---- Teams --------------------------------------------------------------------- */
    public Team PlayerTeam {get; private set;}


    /* #endregion */

    /* #region ---- Dependencies -------------------------------------------------------------- */
    private MatchManager MatchManager;

    /* #endregion */
    
    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== GET DEPENDENCIES ========================================================== */
    private void getDependencies()
    {
        MatchManager = MatchManager.Instance;
    }

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== ON ENABLE ================================================================= */
    private void OnEnable() 
    {
        getDependencies();
    }
    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== S E T U P   T E A M S ===================================================== */
    public void SetupTeams (string playerTeamName, string AITeamName) 
    {
        string AITeam = AITeamName; 
        PlayerTeam = getTeam(playerTeamName);
        
    }

    private Team getTeam(string teamName)
    {
        List<Team> teamsList = MatchManager.GameManager.Teams;
        Team returnTeam = null;

        foreach(Team team in teamsList)
        {
            if (team.Name == teamName)
            {
                returnTeam = team;
                break;
            }
        }

        if (returnTeam == null)
        {
            Debug.Log($"No team with name {teamName} exist!");
        }

        return returnTeam;
    }

    /* #endregion */
    /* ======================================================================================== */

}
