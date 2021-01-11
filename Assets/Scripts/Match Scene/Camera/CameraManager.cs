using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : SingletonMonoBehaviour<CameraManager>
{
    /* #region ==== FIELDS & PROPERTIES ======================================================= */
    
    public CameraPosition CamPos = CameraPosition.South;
    Vector3 originalPos;

    public enum RotationDir 
    {
        Left,
        Right
    }

    public enum CameraPosition 
    {
        North,
        West,
        South,
        East
    }

    MatchManager MatchManager;

    /* #endregion */  
    /* ======================================================================================== */

    /* #region ==== AWAKE / START ============================================================= */
    void Awake() 
    {
        getReferencesOnAwake();
    }
    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== GET REFERENCES ============================================================ */
    void getReferencesOnAwake() 
    {
        getSetMatchManager();
    }

    /* #region ---- Get/Set MatchManager (Get Instance and sets itself as ref on the same ----- */
    public void getSetMatchManager()
    {
        MatchManager = MatchManager.Instance;
        MatchManager.SetCameraManager();
    }
    /* #endregion */
    /* ---------------------------------------------------------------------------------------- */
    
    /* #endregion */
    /* ======================================================================================== */

    /* #region ==== START (button trigger) - ROTATE CAMERA TRIGGERS =========================== */

    public void RotateLeft() 
    {
        rotate(RotationDir.Left);
    }

    public void RotateRight() 
    {
        rotate(RotationDir.Right);
    }
    /* #endregion */  
    /* ======================================================================================== */

    /* #region ==== R O T A T E  M A I N  C A M E R A ========================================= */
    private void rotate(RotationDir direction) 
    {   
        if (direction == RotationDir.Left) 
        {
            getCameraPosition(RotationDir.Left);  
            transform.Rotate(Vector3.up, 90, Space.Self);
            MatchManager.MainCameraControler.UpdateCamera(CamPos); 
        }
        else if (direction == RotationDir.Right)
        {
            getCameraPosition(RotationDir.Right);
            transform.Rotate(Vector3.up, -90, Space.Self);
            MatchManager.MainCameraControler.UpdateCamera(CamPos); 
        }
        else 
        {
            Debug.Log("No direction set!");
        }
    }
    /* #endregion */  
    /* ======================================================================================== */

    /* #region ==== G E T  C U R R E N T  C A M E R A  P O S I T I O N ======================== */
    private void getCameraPosition(RotationDir direction) 
    {
        if (CamPos == CameraPosition.South) 
        {
            if (direction == RotationDir.Left) 
            {
                CamPos = CameraPosition.West;
            }
            else 
            {
                CamPos = CameraPosition.East;
            } 
        }
        else if (CamPos == CameraPosition.East) 
        {
            if (direction == RotationDir.Left) 
            {
                CamPos = CameraPosition.South;
            }
            else 
            {
                CamPos = CameraPosition.North;
            } 
        }
        else if (CamPos == CameraPosition.North) 
        {
            if (direction == RotationDir.Left) 
            {
                CamPos = CameraPosition.East;
            }
            else 
            {
                CamPos = CameraPosition.West;
            } 
        }
        else if (CamPos == CameraPosition.West) 
        {
            if (direction == RotationDir.Left) 
            {
                CamPos = CameraPosition.North;
            }
            else 
            {
                CamPos = CameraPosition.South;
            } 
        }
        else 
        {
            Debug.Log("Something bad happened! No side!", this.gameObject);
        }
    }
    /* #endregion */  
    /* ======================================================================================== */

}