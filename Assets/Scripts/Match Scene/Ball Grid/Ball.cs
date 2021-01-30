using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : SingletonMonoBehaviour<Ball>
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
        getSetMatchManager();
    }

    /* #region ---- Get/Set MatchManager (Get Instance and sets itself as ref on the same ----- */
    public void getSetMatchManager()
    {
        this.MatchManager = MatchManager.Instance;
        MatchManager.SetBall();
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

    /* #region ==== B A L L  P O S I T I O N ================================================== */
    
    /* #region ---- Set Ball Position --------------------------------------------------------- */
    public void SetBallPossetion(BallGridPoint ballPoint) 
    {
        if (ballPoint != null)
        {
            Vector3 newPos = new Vector3(
                ballPoint.transform.position.x,
                this.transform.position.y,
                ballPoint.transform.position.z );

            this.transform.position = newPos;
        }
        else
        {
            Debug.Log("The ballPoint is not set (null)", this);
        }
    }

    /* #endregion */
    
    /* #endregion */
    /* ======================================================================================== */

}
