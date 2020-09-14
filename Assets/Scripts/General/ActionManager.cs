using RPG_Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CombatAction))]
public class ActionManager : MonoBehaviour
{
	public static ActionManager Instance { get; private set; }

    [SerializeField] private CombatAction action_Combat;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
			return;
		}

        action_Combat = GetComponent<CombatAction>();
	}

	public void RunAction<Action>(Combat_Action _actionType) where Action : CombatAction
	{
		print("Yo run");
		action_Combat.RunCombatAction(_actionType);
	}
}
