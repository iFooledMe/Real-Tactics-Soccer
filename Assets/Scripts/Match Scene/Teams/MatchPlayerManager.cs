using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPlayerManager : SingletonMonoBehaviour<MatchPlayerManager>
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */
    
    /* #region ---- Prefabs ------------------------------------------------------------------- */
    [SerializeField] private GameObject playerPrefab;

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
        getSetMatchManager();
    }

    /* #region ---- Get/Set MatchManager (Get Instance and sets itself as ref on the same ----- */
    public void getSetMatchManager()
    {
        this.MatchManager = MatchManager.Instance;
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

    /* #region ==== C R E A T E  P L A Y E R S ================================================ */
    private void createPlayers()
    {
        List<Player> players = MatchManager.MatchTeamManager.PlayerTeam.Players;
        
        foreach(var player in players)
        {
            //Get players start coordinates on the pitch
            int coordX = 0;
            int coordZ = 0;
            foreach(KeyValuePair<Player.StartPos,int> pos in player.startPosition)
            {
                if(pos.Key == Player.StartPos.X)
                {
                    coordX = pos.Value;
                }
                if (pos.Key == Player.StartPos.Z)
                {
                    coordZ = pos.Value;
                }
            }

            PitchTile pitchTile = MatchManager.PitchManager.GetPitchTile(coordX, coordZ);
            GameObject playerObj = (GameObject)Instantiate(playerPrefab);
            playerObj.transform.position = getPosition(pitchTile, playerObj);

        }        
    }

    private Vector3 getPosition(PitchTile pitchTile, GameObject playerObj)
    {
        GameObject pitchTileObj = pitchTile.gameObject;

        Vector3 returnPosition = new Vector3(
            pitchTileObj.transform.position.x, 
            playerObj.transform.position.y, 
            pitchTileObj.transform.position.z);

        return returnPosition;
    }





    
}
