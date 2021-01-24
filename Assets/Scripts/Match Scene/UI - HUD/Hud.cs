using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Hud : SingletonMonoBehaviour<Hud>
{
    public Text ApValue {get; private set;}

    /* #region ==== FIELDS & PROPERTIES ======================================================= */
    
    /* #region ---- Players Stats ---------------------------------------------------------------- */
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
        // Player Stats
        apValue =  this.transform.Find("HUD - Action Points/HUD - AP Value").GetComponent<TMP_Text>();
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
    
    public void UpdatePlayerStats(MatchPlayer Player)
    {
        apValue.text = Player.CurrentActionPoints.ToString();
    }

    /* #endregion */
    /* ======================================================================================== */










}
