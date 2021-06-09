using RPG_Data;
using UnityEngine;

public class CombatArea : MonoBehaviour
{
	private static CombatArea instance;
    [SerializeField, Header ("Player")] private Transform playerArea;
	[SerializeField] private Vector3 playerPosDefault, playerPosOffset;
	[SerializeField] private Vector3 playerRotationDefault;

    [SerializeField, Header ("Enemy")] private Transform enemyArea;
	[SerializeField] private Vector3 enemyPosDefault, enemyPosOffset;
	[SerializeField] private Vector3 enemyRotationDefault;

#if UNITY_EDITOR
	[SerializeField, Header ("Gizmos")] private bool drawGizmos = true;
	[SerializeField] private Vector3 gizmosSize;

	private void OnDrawGizmos()
	{
		if (drawGizmos)
		{
			if (playerArea)
			{
				Gizmos.color = Color.blue;
				Gizmos.DrawWireCube(playerArea.position + Vector3.up, gizmosSize);
			}

			if (enemyArea)
			{
				Gizmos.color = Color.red;
				Gizmos.DrawWireCube(enemyArea.position + Vector3.up, gizmosSize);
			}
		}
	}
#endif

	private void Awake()
	{
		instance = this;
		//CombatCharacterManager.OnNewCharacterAdded += SetPositionAndRotation;
	}

	private void OnDestroy()
	{
		//CombatCharacterManager.OnNewCharacterAdded -= SetPositionAndRotation;
	}

	public static void SetPositionAndRotation(Character _char, RPG_Party _type)
	{
		if (instance == null)
			return;

		Transform _model = _char.Model.transform;

		_model.position = GetPositionAndRotation(_type, out Quaternion newRot);
		_model.rotation = newRot;
		_model.SetParent(instance.GetParent(_type));
	}

	public static Vector3 GetPositionAndRotation(RPG_Party _type, out Quaternion _rot)
	{
		_rot = Quaternion.identity;

		if (instance == null)
			return Vector3.zero;

		switch (_type)
		{
			case RPG_Party.Ally:
				_rot = Quaternion.Euler(instance.playerRotationDefault);
				return instance.playerPosDefault + (instance.playerPosOffset * instance.playerArea.childCount);
			case RPG_Party.Enemy:
				_rot = Quaternion.Euler(instance.enemyRotationDefault);
				return instance.enemyPosDefault + (instance.enemyPosOffset * instance.enemyArea.childCount);
			default:
				break;
		}

		return Vector3.zero;
	}

	public Transform GetParent(RPG_Party _type)
	{
		switch (_type)
		{
			case RPG_Party.Ally:
				return playerArea;
			case RPG_Party.Enemy:
				return enemyArea;
		}

		return null;
	}

	[ContextMenu ("Adjust")]
	private void PositionOffset()
	{
		foreach (Transform child in playerArea)
		{
			child.position = playerPosDefault + (playerPosOffset * child.GetSiblingIndex());
		}

		foreach (Transform child in enemyArea)
		{
			child.position = enemyPosDefault + (enemyPosOffset * child.GetSiblingIndex());
		}
	}
}
