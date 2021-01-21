using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPointer : SingletonMonoBehaviour<BallPointer>
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */
    
    private BallGridPoint BallGridPoint;
    private Vector3 position;

    /* #region ---- Dependencies -------------------------------------------------------------- */
    private MatchManager MatchManager;

    /* #endregion */
    
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
        this.MatchManager = MatchManager.Instance;
        MatchManager.SetBallPointer();
    }
    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== AWAKE / START ============================================================= */
    void Awake() 
    {
        getDependencies();
        position = this.transform.position;
    }

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== UPDATE ==================================================================== */
    void Update() 
    {
        //setPosition();
    }

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== S E T  P O S I T I O N ==================================================== */    
    public void SetPosition()
    {
        BallGridPoint = MatchManager.BallGrid.CurrentPoint;
        position.x = BallGridPoint.transform.position.x;
        position.z = BallGridPoint.transform.position.z;
        this.transform.position = position;

        Debug.Log(BallGridPoint.transform.position);
    }

    /* #endregion */
    /* ======================================================================================== */
    
}
