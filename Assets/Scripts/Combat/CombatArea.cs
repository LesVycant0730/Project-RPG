using RPG_Data;
using UnityEngine;

public class CombatArea : MonoBehaviour
{
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
		CombatCharacterManager.OnNewCharacterAdded += SetPosition;
	}

	private void OnDestroy()
	{
		CombatCharacterManager.OnNewCharacterAdded -= SetPosition;
	}

	public void SetPosition(Character _char, RPG_Party _type)
	{
		Transform _model = _char.Model.transform;

		switch (_type)
		{
			case RPG_Party.Ally:
				_model.SetParent(playerArea);
				_model.localPosition = playerPosDefault + (playerPosOffset * _model.GetSiblingIndex());
				_model.localRotation = Quaternion.Euler(playerRotationDefault);
				break;
			case RPG_Party.Enemy:
				_model.SetParent(enemyArea);
				_model.localPosition = enemyPosDefault + (enemyPosOffset * _model.GetSiblingIndex());
				_model.localRotation = Quaternion.Euler(enemyRotationDefault);
				break;
			default:
				break;
		}
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
