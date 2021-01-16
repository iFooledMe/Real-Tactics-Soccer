using System;
using UnityEngine;

public class PlayerInput : SingletonScriptableObject<PlayerInput>
{
    public string test = "Wtest";
    
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
    private void OnEnable() 
    {
        getDependencies();
    }
    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== E V E N T  / C O N T R O L  H A N D L E R S =============================== */
    
    /* #region ---- Player MouseEnter / MuseExit ---------------------------------------------- */
    public void OnPlayerMouseEnter(MatchPlayer player)
    {
        player.PlayerHighlightOn();
    }

    public void OnPlayerMouseExit(MatchPlayer player)
    {
        player.PlayerHighlightOff();
    }

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #endregion */
    /* ======================================================================================== */



}
