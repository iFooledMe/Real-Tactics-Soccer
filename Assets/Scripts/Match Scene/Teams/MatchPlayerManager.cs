using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerInit
{
    Yes
}

public class MatchPlayerManager : SingletonMonoBehaviour<MatchPlayerManager>
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */
    
    /* #region ---- Settings ------------------------------------------------------------------ */
    public ActionsApCostSettings ActionsApCostSettings;
    public PlayerModelSettings PlayerModelSettings;
    
    /* #endregion */

    /* #region ---- Players ------------------------------------------------------------------- */
    public List<MatchPlayer> MatchPlayersList {get; private set;}
    public MatchPlayer CurrentActivePlayer = null;
    public bool PlayInAction { get; private set; }
    public MatchPlayer CurrentBallHolder {get; set;}

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

    /* #region ==== AWAKE / START / EVENTS ==================================================== */
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
        MatchPlayersList = new List<MatchPlayer>();
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
                setPlayerInfo(playerObj, matchPlayer, player, team, pitchTile, count, coordX, coordZ);
                setPlayerActiveState(playerObj, player);
                setVectorPosition(pitchTile, playerObj);
                setTileOccupied(pitchTile, matchPlayer);
                MatchPlayersList.Add(matchPlayer);
                SetCurrentBallHolder(matchPlayer);
            }    
        }
    }
    
    /* #region ---- Set player info ----------------------------------------------------------- */
    private void setPlayerInfo(GameObject playerObj, MatchPlayer matchPlayer, Player player, Team team, PitchTile pitchTile, int count, int coordX, int coordZ)
    {
        playerObj.name = $"{team.Name} - Player {count} - {player.Name}";
        playerObj.transform.SetParent(this.transform);
        
        matchPlayer.SetPlayerInfoOnInstantiation(player, pitchTile, coordX, coordZ);
    }

    /* #endregion */

    /* #region ---- Set player Active status -------------------------------------------------- */
    private void setPlayerActiveState(GameObject playerObj, Player player)
    {
        MatchPlayer matchPlayer = playerObj.GetComponent<MatchPlayer>();

        if (player.startActive)
        {
            matchPlayer.SetPlayerActive(PlayerInit.Yes);
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

    /* #region ==== S E T  A  P L A Y E R  A S  B A L L  H O L D E R ========================== */
    public void SetCurrentBallHolder(MatchPlayer Player)
    {
        if (Player.IsBallHolder)
        {
            MatchManager.Ball.SetNoBallHolder();
            this.CurrentBallHolder = Player;
            Player.setAsBallHolder(true);
            MatchManager.Ball.SetBallHolder(CurrentBallHolder);
        }
    }

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
        foreach(MatchPlayer player in MatchPlayersList)
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

        foreach(var player in MatchPlayersList)
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
