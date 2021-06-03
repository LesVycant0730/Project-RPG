using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatArea : MonoBehaviour
{
    private static CombatArea instance;

    [SerializeField, Header ("Player")] private Transform playerArea;
	[SerializeField] private Vector3 playerPosDefault, playerPosOffset;

    [SerializeField, Header ("Enemy")] private Transform enemyArea;
	[SerializeField] private Vector3 enemyPosDefault, enemyPosOffset;
	

	private void Awake()
	{
		instance = this;
	}

#if UNITY_EDITOR
	[SerializeField, Header ("Gizmos")] private bool drawGizmos = true;
	[SerializeField] private Vector3 gizmosSize;
	private void OnValidate()
	{
		//PositionOffset();
	}

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

	public static void SetPosition(RPGParty.PartyType _type, Transform _model)
	{
		if (instance)
		{
			switch (_type)
			{
				case RPGParty.PartyType.Ally:
					_model.SetParent(instance.playerArea);
					_model.position = instance.playerPosDefault + (instance.playerPosOffset * _model.GetSiblingIndex());
					break;
				case RPGParty.PartyType.Enemy:
					_model.SetParent(instance.enemyArea);
					_model.position = instance.enemyPosDefault + (instance.enemyPosOffset * _model.GetSiblingIndex());
					break;
				default:
					break;
			}
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
