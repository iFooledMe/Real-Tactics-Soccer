using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Hud : SingletonMonoBehaviour<Hud>
{
    public Text ApValue {get; private set;}

    /* #region ==== FIELDS & PROPERTIES ======================================================= */
    
    /* #region ---- Players General Info ------------------------------------------------------ */
    TMP_Text playerName;

    /* #endregion */
    
    /* #region ---- Players Stats ------------------------------------------------------------- */
    TMP_Text apValue;
    TMP_Text apCost;

    /* #endregion */

    /* #region ---- General Game Message ------------------------------------------------------ */
    TMP_Text gameMessage;

    /* #endregion */

    /* #region ---- Message Text Settings ----------------------------------------------------- */
    public Messages Messages;

    /* #endregion */

    /* #region ---- Dependencies -------------------------------------------------------------- */
    private MatchManager MatchManager;

    /* #endregion */
    
    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== GET DEPENDENCIES & COMPONENTS ============================================= */
    private void getDependencies()
    {
        getSetMatchManager();
        getComponents();
    }

    /* #region ---- GetSet MatchManager ------------------------------------------------------- */
    public void getSetMatchManager()
    {
        MatchManager = MatchManager.Instance;
        MatchManager.SetHud();
    }
    /* #endregion */

    /* #region ---- Get Components ------------------------------------------------------------ */
    public void getComponents()
    {
        // Player General Info
        playerName =  this.transform.Find("HUD - Player Name/Name Value").GetComponent<TMP_Text>();
        
        // Player Stats
        apValue =  this.transform.Find("HUD - Action Points/AP Value").GetComponent<TMP_Text>();

        // Temporary States
        apCost = this.transform.Find("HUD - Action Points/AP Cost").GetComponent<TMP_Text>();

        //General Game Message
        gameMessage = this.transform.Find("HUD - General Info").GetComponent<TMP_Text>();
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
    
    /* #region ==== U P D A T E  P L A Y E R  I N F O ========================================= */
    
    /* #region ---- Player info --------------------------------------------------------------- */
    public void UpdatePlayerInfo(MatchPlayer Player)
    {
        // Player General Info
        playerName.text = Player.Player.Name;

        // Player Stats
        apValue.text = Player.CurrentActionPoints.ToString();

    }

    /* #endregion */

    /* #region ---- Accumulated AP-Cost (Calculation before action) --------------------------- */
    public void UpdateAccAPCost(int accCost)
    {
        MatchPlayer activePlayer = MatchManager.MatchPlayerManager.CurrentActivePlayer;
        DrawPathLine drawPathLine = MatchManager.PitchGrid.PathFinding.DrawPathLine;

        if (drawPathLine.AccCostReset)
        {
            apCost.text = "";
        }
        else
        {
            if(activePlayer.PlayerMode == PlayerMode.Move)
            {
                //TODO: Why accCost != 0 when calculating AP-Cost in Move Mode???
                if (accCost <= activePlayer.CurrentActionPoints && accCost != 0)
                {
                    apCost.text = $"-{accCost.ToString()}";
                }
                else
                {
                    apCost.text = "Not enough AP!";
                }
            }
            else
            {
                if (accCost <= activePlayer.CurrentActionPoints)
                {
                    apCost.text = $"-{accCost.ToString()}";
                }
                else
                {
                    apCost.text = "Not enough AP!";
                }
            }
        }
    }

    /* #endregion */

    /* #region ---- Update apCost --------------------------------------------------------------- */
    public void UpdateApCostToString(string message)
    {
        apCost.text = message;
    }

    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== U P D A T E  G E N E R A L  I N F O ======================================= */
    
    /* #region ---- Game Message -------------------------------------------------------------- */
    public void UpdateGameMessage(string message)
    {
        gameMessage.text = message;
    }

    public void ResetGameMessage()
    {
        gameMessage.text = "";
    }
    /* #endregion */

    /* #endregion */
    /* ======================================================================================== */










}
