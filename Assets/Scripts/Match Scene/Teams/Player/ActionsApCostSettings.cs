using UnityEngine;

[CreateAssetMenu(fileName = "ActionsApCostSettings", menuName = "ScriptableObjects/MatchScene/ActionsApCostSettings", order = 1)]
public class ActionsApCostSettings : SingletonScriptableObject<ActionsApCostSettings>
{
    [Header("Move to tiles")]
    [Range(1,5)] public int CostEnterTileEmpty = 1;
    [Range(1,5)] public int CostEnterTileOtherPlayer = 2;

    [Header("Rotation")]
    [Range(1,5)] public int CostRotateHalf = 1;
    [Range(1,5)] public int CostRotateFull = 2;
}
