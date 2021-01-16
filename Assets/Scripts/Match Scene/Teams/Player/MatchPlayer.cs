using System;
using UnityEngine;

public class MatchPlayer : MonoBehaviour
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */
    
    /* #region ---- Player Info --------------------------------------------------------------- */
    public Player Player {get; private set;}

    private string playerName;

    public int CoordX {get; private set;}
    public int CoordZ {get; private set;}

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #region ---- Player Stats -------------------------------------------------------------- */
    private int actionPoints;

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

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */
    
    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== GET DEPENDENCIES & COMPONENTS ============================================= */
    private void getDependencies()
    {
        getMatchManager();
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
        playerName = Player.Name;
        actionPoints = Player.Stats.ActionPoints;
    }

    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */

    /* #region ---- Set player Active / Inactive ---------------------------------------------- */
    public void SetPlayerActive()
    {
        IsActive = true;
        _renderer.material.color = activeColor;
        MatchManager.MatchPlayerManager.SetOtherPlayersInactive(this);
    }

    public void SetPlayerInactive()
    {
        IsActive = false;
        _renderer.material.color = defaultColor;
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

}
