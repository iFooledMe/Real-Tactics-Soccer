using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGridPoint : MonoBehaviour
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */
       
    /* #region ---- Dependencies -------------------------------------------------------------- */
    private MatchManager MatchManager;

    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== GET DEPENDENCIES ========================================================== */
    private void getDependencies()
    {
        getMatchManager();
    }

    /* #region ---- Get/Set MatchManager (Get Instance and sets itself as ref on the same ----- */
    public void getMatchManager()
    {
        this.MatchManager = MatchManager.Instance;
    }
    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== AWAKE / START ============================================================= */
    private void Awake() 
    {
        getDependencies();
    }
    
    /* #endregion */
    /* ======================================================================================== */
}
