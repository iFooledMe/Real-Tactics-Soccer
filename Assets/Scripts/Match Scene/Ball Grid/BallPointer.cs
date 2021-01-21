using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPointer : MonoBehaviour
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */
    
    private BallGridPoint BallGridPoint;
    private Vector3 position;

    /* #region ---- Dependencies -------------------------------------------------------------- */
    private MatchManager MatchManager;

    /* #endregion */
    
    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== GET DEPENDENCIES & COMPONENTS ============================================= */
    private void getDependencies()
    {
        getMatchManager();
    }

    /* #region ---- Get MatchManager ---------------------------------------------------------- */
    public void getMatchManager()
    {
        MatchManager = MatchManager.Instance;
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
        setPosition();
    }

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== SET POSITION ============================================================== */    
    private void setPosition()
    {
        BallGridPoint = MatchManager.BallGrid.CurrentPoint;
        //Debug.Log(BallGridPoint.transform.position);

        position.x = BallGridPoint.transform.position.x;

        position.z = BallGridPoint.transform.position.z;

        this.transform.position = position;
    }

    /* #endregion */
    /* ======================================================================================== */
    
}
