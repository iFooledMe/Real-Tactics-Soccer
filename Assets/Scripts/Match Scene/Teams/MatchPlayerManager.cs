using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPlayerManager : SingletonMonoBehaviour<MatchPlayerManager>
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */
    
    /* #region ---- Dependencies -------------------------------------------------------------- */
    private MatchManager MatchManager;

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

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
        MatchManager = MatchManager.Instance;
        MatchManager.SetMatchPlayerManager();
    }
    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */


    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== AWAKE / START ============================================================= */
    private void Awake() 
    {
        getDependencies();
        
    }
    
    private void Start()
    {
        MatchManager.PitchGrid.PitchCreated += createPlayers;
    }

    /* #endregion */
    /* ======================================================================================== */

    private void createPlayers()
    {
        Debug.Log("Pitch is fully loaded in Scene. Start load players...");
    }

}
