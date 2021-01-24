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

    }

    /* #endregion */
    /* ======================================================================================== */
    
    /* #region ==== P L A Y E R  S T A T S ==================================================== */
    
    public void UpdatePlayerInfo(MatchPlayer Player)
    {
        // Player General Info
        playerName.text = Player.Player.Name;

        // Player Stats
        apValue.text = Player.CurrentActionPoints.ToString();
    }

    /* #endregion */
    /* ======================================================================================== */










}
