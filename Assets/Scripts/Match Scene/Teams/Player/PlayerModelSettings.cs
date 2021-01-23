using UnityEngine;

[CreateAssetMenu(fileName = "Player Model Settings", menuName = "ScriptableObjects/MatchScene/PlayerModelSettings")]
public class PlayerModelSettings : SingletonScriptableObject<ActionsApCostSettings>
{
	[Header("PLAYER MODEL SETTINGS")]
	[Range(2,4)] public float ModelMoveSpeed = 3f;

}
