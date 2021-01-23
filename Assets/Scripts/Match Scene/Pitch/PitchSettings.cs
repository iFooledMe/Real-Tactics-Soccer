using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pitch Settings", menuName = "ScriptableObjects/MatchScene/PitchSettings")]
public class PitchSettings : SingletonScriptableObject<PitchSettings>
{
    //TODO: Fix issue with pitch being created skewed when PitchWidth is set to uneven number
    [Header("PITCH SIZE")]
    [Range(20,40)] public int PitchWidth = 20;
    [Range(40,80)] public int PitchLength = 40;
}
