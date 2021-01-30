using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Messages", menuName = "ScriptableObjects/MatchScene/GameMessages")]
public class Messages : SingletonScriptableObject<Messages>
{
    /* #region ==== G E N E R A L  G A M E  M A S S A G E S =================================== */
    [Header("General Game Messages")]
    
    /* #region ---- No Action Points Message -------------------------------------------------- */
    [TextArea(3,8)]
    public string NoActionPoints = $"The player has no more Action Points left to use!";

    /* #endregion */


    /* #region ---- No Action Points Message -------------------------------------------------- */
    [TextArea(3,8)]
    public string NoMoreRotations = $"Only 1 rotation per turn permited!";

    /* #endregion */

    
    /* #endregion */
    /* ======================================================================================== */



}
