using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchManager : SingletonScriptableObject<PitchManager>
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */

    /* #region ---- Pitch Settings ------------------------------------------------------------ */
    private int _pitchWidth = 20; 
    private int _pitchLength = 40;
    public int PitchWidth { get => _pitchWidth; private set => _pitchLength = value; }
    public int PitchLength { get => _pitchLength; private set => _pitchLength = value; }


    //TODO: Instantiate _pitchTileObjects with pitchWidth and PitchLenght. Trying to do so now cause the pitch to not be created propperly.
    
    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */
    
    
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
    }
    /* #endregion */
    /* ======================================================================================== */

    public void RefTest(string fromClass)
    {
        Debug.Log("PitchManager reference from " + fromClass + " - OK!");
    }

}
