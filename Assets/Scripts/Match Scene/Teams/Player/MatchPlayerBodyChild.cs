using UnityEngine;

public class MatchPlayerBodyChild : MonoBehaviour
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */

    /* #region ---- Dependencies -------------------------------------------------------------- */
    private MatchManager MatchManager;
    private MatchPlayer MatchPlayer;

    /* #endregion */
    
    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== GET DEPENDENCIES & COMPONENTS ============================================= */
    private void getDependencies()
    {
        getMatchManager();
        getMatchPlayerParent();
    }

    /* #region ---- Get MatchManager ---------------------------------------------------------- */
    public void getMatchManager()
    {
        MatchManager = MatchManager.Instance;
    }
    /* #endregion */

    /* #region ---- Get MatchManager ---------------------------------------------------------- */
    public void getMatchPlayerParent()
    {
        GameObject MatchPlayerParent = transform.parent.gameObject;
        MatchPlayer = MatchPlayerParent.GetComponent<MatchPlayer>();
    }

    /* #endregion */
    
    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== AWAKE / START ============================================================= */
    void Awake() 
    {
        getDependencies();
    }

    /* #endregion */
    /* ======================================================================================== */
    
    /* #region ==== INTERACTIONS ============================================================== */
    
    /* #region ---- MouseOver / MouseExit ----------------------------------------------------- */
    
    private void OnMouseEnter() 
    {
       MatchManager.MatchPlayerInput.OnPlayerMouseEnter(MatchPlayer);
    }

    private void OnMouseExit()
    {
        MatchManager.MatchPlayerInput.OnPlayerMouseExit(MatchPlayer);
    }

    /* #endregion */

    /* #region ---- Left click ---------------------------------------------------------------- */
    
    private void OnMouseUp() 
    {
        MatchManager.MatchPlayerInput.OnPlayerLeftClick(MatchPlayer);
    }

    /* #endregion */
    
    /* #endregion */
    /* ======================================================================================== */
}
