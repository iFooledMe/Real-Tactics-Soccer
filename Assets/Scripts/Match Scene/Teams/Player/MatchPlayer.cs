using System;
using System.Collections.Generic;
using UnityEngine;

public class MatchPlayer : MonoBehaviour
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */
    
    /* #region ---- Player Info --------------------------------------------------------------- */
    public Player Player {get; private set;}

    public string Name {get; private set;}

    public int CoordX {get; private set;}
    public int CoordZ {get; private set;}

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #region ---- Player Stats -------------------------------------------------------------- */
    public int ActionPoints {get; private set;}

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #region ---- Player States ------------------------------------------------------------- */
    public bool IsActive {get; private set;}

    private Color defaultColor = Color.green;
    private Color highLightColor = Color.blue;
    private Color activeColor = Color.red;

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */
    
    /* #region ---- Components ---------------------------------------------------------------- */
    private Renderer _renderer;

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #region ---- Dependencies -------------------------------------------------------------- */
    private MatchManager MatchManager;
    public PlayerActions PlayerActions {get; private set;}

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */
    
    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== GET DEPENDENCIES & COMPONENTS ============================================= */
    private void getDependencies()
    {
        getMatchManager();
        setPlayerActions();
    }

    private void getComponents()
    {
        getRenderer();
    }

    /* #region ---- Get MatchManager ---------------------------------------------------------- */
    public void getMatchManager()
    {
        MatchManager = MatchManager.Instance;
    }
    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #region ---- Get Renderer Component ---------------------------------------------------- */
    public void getRenderer()
    {
        _renderer = this.GetComponent<Renderer>();
    }
    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #region ---- Set PlayerActions --------------------------------------------------------- */
    public void setPlayerActions()
    {
        this.PlayerActions = new PlayerActions(this);
    }

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== AWAKE / START ============================================================= */
    void Awake() 
    {
        getDependencies();
        getComponents();
    }

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== UPDATE ==================================================================== */
    void Update() 
    {
        PlayerActions.ActionSelector(PlayerActions.CurrentAction);
    }

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== INTERACTIONS ============================================================== */
    
    /* #region ---- MouseOver / MouseExit ----------------------------------------------------- */
    
    private void OnMouseEnter() 
    {
       MatchManager.MatchPlayerInput.OnPlayerMouseEnter(this);
    }

    private void OnMouseExit()
    {
        MatchManager.MatchPlayerInput.OnPlayerMouseExit(this);
    }

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #region ---- Left click ---------------------------------------------------------------- */
    
    private void OnMouseUp() 
    {
        MatchManager.MatchPlayerInput.OnPlayerLeftClick(this);
    }

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */
    
    /* #endregion */
    /* ======================================================================================== */
    
    /* #region ==== M A I N  M E T H O D S ==================================================== */
    
    /* #region ---- Set player info on Instantiation ------------------------------------------ */
    public void SetPlayerInfo(Player player, int coordX, int coordZ)
    {
        CoordX = coordX;
        CoordZ = coordZ;
        Player = player;
        Name = Player.Name;
        ActionPoints = Player.Stats.ActionPoints;
    }

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #region ---- Set player Active / Inactive ---------------------------------------------- */
    public void SetPlayerActive()
    {
        IsActive = true;
        _renderer.material.color = activeColor;
        MatchManager.MatchPlayerManager.SetOtherPlayersInactive(this);
        MatchManager.MatchPlayerManager.CurrentActivePlayer = this;
    }

    public void SetPlayerInactive()
    {
        IsActive = false;
        _renderer.material.color = defaultColor;
    }

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #region ---- Set player Coordinates by PitchTile --------------------------------------- */
    public void SetPlayerCoordinatesByTile(PitchTile tile)
    {
        this.CoordX = tile.CoordX;
        this.CoordZ = tile.CoordZ;
    }

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #region ---- Player Highlight ---------------------------------------------------------- */
    public void PlayerHighlightOn()
    {
        _renderer.material.color = highLightColor;
    }

    public void PlayerHighlightOff()
    {
        if (IsActive)
        {
            _renderer.material.color = activeColor;
        }

        else
        {
            _renderer.material.color = defaultColor;
        } 
    }

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #endregion */
    /* ======================================================================================== */
    int _currentWaypoint = 0;
    float _onScreenMoveSpeed = 3f;

    public void MovePlayer(Vector3 _nextPos, List<PitchTile> _waypoints)
    {
        _nextPos = new Vector3(_waypoints[
        _currentWaypoint].transform.position.x, 
        transform.position.y, 
        _waypoints[_currentWaypoint].transform.position.z);

        
        
        float distance = Vector3.Distance(_nextPos, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, _nextPos, _onScreenMoveSpeed * Time.deltaTime);

        Debug.Log(distance);
        
        if (distance <= 0.005)
        {
            transform.position = _nextPos;
            _currentWaypoint++;
        }

        if (_currentWaypoint >= _waypoints.Count)
        {
            _waypoints[_waypoints.Count - 1].CostToEnter = 0;
            _waypoints = null;
            _currentWaypoint = 0;
            MatchManager.MatchPlayerManager.setPlayerInActionState(false);
            PlayerActions.CurrentAction = PlayerAction.Idle;
        }
    }



}
