using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG_Data;

public class CombatArea : MonoBehaviour
{
    [SerializeField, Header ("Player")] private Transform playerArea;
	[SerializeField] private Vector3 playerPosDefault, playerPosOffset;

    [SerializeField, Header ("Enemy")] private Transform enemyArea;
	[SerializeField] private Vector3 enemyPosDefault, enemyPosOffset;

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
		CombatCharacterManager.OnNewModelAdded += SetPosition;
	}

	private void OnDestroy()
	{
		CombatCharacterManager.OnNewModelAdded -= SetPosition;
	}

	public void SetPosition(CharacterModel _char, RPG_Party _type)
	{
		Transform _model = _char.Model.transform;

		print("Invoke");
		switch (_type)
		{
			case RPG_Party.Ally:
				_model.SetParent(playerArea);
				_model.position = playerPosDefault + (playerPosOffset * _model.GetSiblingIndex());
				break;
			case RPG_Party.Enemy:
				_model.SetParent(enemyArea);
				_model.position = enemyPosDefault + (enemyPosOffset * _model.GetSiblingIndex());
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
