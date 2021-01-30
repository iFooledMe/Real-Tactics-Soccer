using UnityEngine;

[CreateAssetMenu(fileName = "Actions AP-Cost Settings", menuName = "ScriptableObjects/MatchScene/ActionsApCostSettings")]
public class ActionsApCostSettings : SingletonScriptableObject<ActionsApCostSettings>
{
    [Header("Move to tiles")]
    [Range(1,5)] public int CostEnterTileEmpty = 1;
    [Range(1,5)] public int CostEnterTileOtherPlayer = 2;

    [Header("Rotation")]
    [Range(1,5)] public int CostRotateHalf = 1;
    [Range(1,5)] public int CostRotateFull = 2;
    [Range(0,2)] public int MaxRotationsPerTurn = 1;
}
