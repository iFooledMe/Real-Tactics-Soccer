using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchManager : SingletonScriptableObject<PitchManager>
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
        MatchManager = MatchManager.Instance;
    }

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== ON ENABLE ================================================================= */
    void OnEnable() 
    {
        getDependencies();
        MatchManager.RefTest("PitchManager");
    }
    /* #endregion */
    /* ======================================================================================== */
}
