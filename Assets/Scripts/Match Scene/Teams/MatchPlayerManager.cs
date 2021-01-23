using System;
using System.Collections.Generic;
using UnityEngine;

public class MatchPlayerManager : SingletonMonoBehaviour<MatchPlayerManager>
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */
    
    /* #region ---- Settings ------------------------------------------------------------------ */
    public float OnScreenPlayerMoveSpeed = 3f;
    public ActionsApCostSettings ActionsApCostSettings;
    
    /* #endregion */

    /* #region ---- Players ------------------------------------------------------------------- */
    private List<MatchPlayer> matchPlayersList = new List<MatchPlayer>();
    public MatchPlayer CurrentActivePlayer = null;
    public bool PlayInAction { get; private set; }

    /* #endregion */
    
    /* #region ---- Prefabs ------------------------------------------------------------------- */
    [SerializeField] private GameObject playerPrefab;

    /* #endregion ------------------------------------------------------------------------------*/

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
        MatchManager.SetMatchPlayerManager();
    }
    /* #endregion */

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
        List<Player> playersList = MatchManager.MatchTeamManager.PlayerTeam.Players;
        Team team = MatchManager.MatchTeamManager.PlayerTeam;
        
        if (playersList.Count < 1 || playersList == null)
        {
            Debug.Log("The team has no players", this);
        }
        else
        {
            int count = 0;
            foreach(var player in playersList)
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

                PitchTile pitchTile = MatchManager.PitchManager.GetPitchTile(coordX, coordZ);
                GameObject playerObj = (GameObject)Instantiate(playerPrefab);
                MatchPlayer matchPlayer = playerObj.GetComponent<MatchPlayer>();
                setPlayerInfo(playerObj, matchPlayer, player, team, count, coordX, coordZ);
                setPlayerActiveState(playerObj, player);
                setVectorPosition(pitchTile, playerObj);
                setTileOccupied(pitchTile, matchPlayer);
                matchPlayersList.Add(matchPlayer);
            }    
        }
    }
    
    /* #region ---- Set player info ----------------------------------------------------------- */
    private void setPlayerInfo(GameObject playerObj, MatchPlayer matchPlayer, Player player, Team team, int count, int coordX, int coordZ)
    {
        playerObj.name = $"{team.Name} - Player {count} - {player.Name}";
        playerObj.transform.SetParent(this.transform);
        
        matchPlayer.SetPlayerInfo(player, coordX, coordZ);
    }

    /* #endregion */

    /* #region ---- Set player Active status -------------------------------------------------- */
    private void setPlayerActiveState(GameObject playerObj, Player player)
    {
        MatchPlayer matchPlayer = playerObj.GetComponent<MatchPlayer>();

        if (player.startActive)
        {
            matchPlayer.SetPlayerActive();
        }
        else
        {
            matchPlayer.SetPlayerInactive();
        }
    }

    /* #endregion */

    /* #region ---- Set player Vector position ------------------------------------------------ */
    private void setVectorPosition(PitchTile pitchTile, GameObject playerObj)
    {
        GameObject pitchTileObj = pitchTile.gameObject;

        Vector3 position = new Vector3 (
            pitchTileObj.transform.position.x, 
            playerObj.transform.position.y, 
            pitchTileObj.transform.position.z);
        
        playerObj.transform.position = position;
    }
    
    /* #endregion */

    /* #region ---- Set pitchTile Occupied by player ------------------------------------------ */
    private void setTileOccupied(PitchTile pitchTile, MatchPlayer playerObj)
    {
        MatchManager.PitchManager.setPitchTileOccupied(pitchTile, playerObj);
    }
    
    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== G E N E R A L  H E L P E R S ============================================== */
    
    /* #region ---- Set Player in Action State ------------------------------------------------ */
    public void setPlayerInActionState(bool playerInAction)
    {
        if(playerInAction)
        {
            this.PlayInAction = true; 
        }

        if(!playerInAction)
        {
            this.PlayInAction = false;
        }
    }

    /* #endregion */
    
    /* #region ---- Set other players inactive (get a player and set all others inactive ------ */
    public void SetOtherPlayersInactive(MatchPlayer activePlayer)
    {
        foreach(MatchPlayer player in matchPlayersList)
        {
            if (player != activePlayer)
            {
                player.SetPlayerInactive();
            }
        }
    }

    /* #endregion */

    /* #region ---- Get Active Player --------------------------------------------------------- */
    public MatchPlayer GetActivePlayer()
    {
        MatchPlayer activePlayer = null;

        foreach(var player in matchPlayersList)
        {
            if (player.IsActive)
            {
                activePlayer = player;
                break;
            }
        }

        if (activePlayer == null)
        {
            Debug.Log("No player is set to active", this);
        }

        return activePlayer;
    }

    /* #endregion */
        
    /* #endregion */
    /* ======================================================================================== */
    
}
