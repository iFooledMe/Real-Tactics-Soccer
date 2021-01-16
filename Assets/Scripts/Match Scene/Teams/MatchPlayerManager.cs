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
        MatchManager.PitchGrid.PitchCreated += addPlayersToPitch;
    }



    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== C R E A T E  P L A Y E R S ================================================ */
    private void addPlayersToPitch()
    {
        List<Player> players = MatchManager.MatchTeamManager.PlayerTeam.Players;
        Team team = MatchManager.MatchTeamManager.PlayerTeam;
        
        if (players.Count < 1 || players == null)
        {
            Debug.Log("The team has no players", this);
        }
        else
        {
            int count = 0;
            foreach(var player in players)
            {
                count++;
                
                /* #region ---- Get start position -------------------------------------------- */
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
                /* #endregion */
                /* ---------------------------------------------------------------------------- */

                PitchTile pitchTile = MatchManager.PitchManager.GetPitchTile(coordX, coordZ);
                GameObject playerObj = (GameObject)Instantiate(playerPrefab);
                setPlayerInfo(playerObj, player, team, count, coordX, coordZ);
                setPosition(pitchTile, playerObj);
                setTileOccupied(pitchTile, playerObj);
            }    
        }
    }
    
    /* #region ---- Set player info ----------------------------------------------------------- */
    private void setPlayerInfo(GameObject playerObj, Player player, Team team, int count, int coordX, int coordZ)
    {
        playerObj.name = $"{team.Name} - Player {count} - {player.Name}";
        playerObj.transform.SetParent(this.transform);

        MatchPlayer matchPlayer = playerObj.GetComponent<MatchPlayer>();
        matchPlayer.SetPlayerInfo(player, coordX, coordZ);
    }

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #region ---- Set player position ------------------------------------------------------- */
    private void setPosition(PitchTile pitchTile, GameObject playerObj)
    {
        GameObject pitchTileObj = pitchTile.gameObject;

        Vector3 position = new Vector3 (
            pitchTileObj.transform.position.x, 
            playerObj.transform.position.y, 
            pitchTileObj.transform.position.z);
        
        playerObj.transform.position = position;
    }
    
    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #region ---- Set pitchTile Occupied ---------------------------------------------------- */
    private void setTileOccupied(PitchTile pitchTile, GameObject playerObj)
    {
        MatchManager.PitchManager.setPitchTileOccupied(pitchTile, playerObj);
    }
    
    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #endregion */
    /* ======================================================================================== */





    
}
