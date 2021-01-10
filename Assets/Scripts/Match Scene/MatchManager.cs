using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : SingletonScriptableObject<MatchManager>
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */ 
    
    /* #region ---- Dependencies -------------------------------------------------------------- */
    public GameManager GameManager {get; private set;}

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
        //loadMatch();
    }
    /* #endregion */
    /* ======================================================================================== */

}
