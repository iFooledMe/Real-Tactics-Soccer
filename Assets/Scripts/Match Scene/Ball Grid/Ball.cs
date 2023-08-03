using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : SingletonMonoBehaviour<Ball>
{

    /* #region ==== FIELDS & PROPERTIES ======================================================= */

    public MatchPlayer BallHolder {get; private set;}

    private bool showErrorMessage = true;

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

    /* #region ==== C H E C K  F O R  B A L L  P O S S I T I O N  U P D A T E ================= */
    public void CheckForBallPosUpdate()
    {
        MatchPlayer ballHolder = null;
        
        // Extra Security if a player is set as BallHolder but not set on MatchPlayerManager
        if (MatchManager.MatchPlayerManager.CurrentBallHolder is null)
        {
            ballHolder = GetBallHolder();
            if (ballHolder is null)
            {
                return;
            }
            else
            {
                MatchManager.MatchPlayerManager.SetCurrentBallHolder(ballHolder);
            }
        }
        // If MatchManager is holding a CurrentBallHolder
        else
        {
            ballHolder = MatchManager.MatchPlayerManager.CurrentBallHolder;
            setBallPositionInPossession(ballHolder);
        }
    }

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== S E T  P L A Y E R  B A L L  P O S S E S S I O N ========================= */
    
    /* #region ---- SET BALL HOLDER ---------------------------------------------------------- */
    public void SetBallHolder(MatchPlayer player)
    {
        BallHolder = player;

        if (BallHolder is null)
        {
            Debug.Log("No Ball Holder", this);
        }
        else
        {
            setBallPositionInPossession(BallHolder);
        }
    }

    /* #endregion */

    /* #region ---- Set Ball position at the BallHolder (depending on BallHolders curren angle)*/
    private void setBallPositionInPossession(MatchPlayer Player)
    {
        BallGridPoint ballPoint = null;
        
        switch(Player.currentAngle) 
        {
            case 0:
                ballPoint = Player.CurrentTile.BallGridPoints[7];
                break;
            case 45:
                ballPoint = Player.CurrentTile.BallGridPoints[8];
                break;
            case 90:
                ballPoint = Player.CurrentTile.BallGridPoints[5];
                break;
            case 135:
                ballPoint = Player.CurrentTile.BallGridPoints[2];
                break;
            case 180:
                ballPoint = Player.CurrentTile.BallGridPoints[1];
                break;          
            case 225:
                ballPoint = Player.CurrentTile.BallGridPoints[0];
                break;  
            case 270:
                ballPoint = Player.CurrentTile.BallGridPoints[3];
                break;  
            case 315:
                ballPoint = Player.CurrentTile.BallGridPoints[6];
                break;  
        }

        //MatchManager.Ball.SetBallPositionOld(ballPoint);

        setBallPosition(Player.transform);
    }

    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */
    
    
    /* #region ==== S E T  B A L L  P O S I T I O N (to a specified BallGridPoint) ============ */
    
    public void setBallPosition(Transform player)
    {
        float distance = 0.5f;

        if (player != null)
        {
            Vector3 ballPosition = player.position + player.forward * distance;
            this.transform.position = ballPosition;
        }
    }

    public void SetBallPositionOld(BallGridPoint ballPoint) 
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
            if(showErrorMessage)
            {
                Debug.Log("The ballPoint is not set (null)", this);
                showErrorMessage = false;
            }
        }
    }
    
    /* #endregion */
    /* ======================================================================================== */

    
    /* #region ==== GENERAL HELPERS =========================================================== */
    
    /* #region ---- Get Currrent Ball Holder -------------------------------------------------- */
    public MatchPlayer GetBallHolder()
    {
        MatchPlayer ballHolder = null;

        foreach(var player in MatchManager.MatchPlayerManager.MatchPlayersList)
        {
            if (player.IsBallHolder)
            {
                ballHolder = player;
                break;
            }
        }

        return ballHolder;
    }

    /* #endregion */

    /* #region ---- Set No BallHolder (All players are set to IsBallHolder = false) ----------- */
    public void SetNoBallHolder()
    {
        foreach(var player in MatchManager.MatchPlayerManager.MatchPlayersList)
        {
            player.setAsBallHolder(false);
            MatchManager.MatchPlayerManager.CurrentBallHolder = null;
        }   
    }

    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */

}
